using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BacktestCointegration
{
    public class MegaOptimization
    {
        #region Local Variables
        private string[] Basket;
        private double[,] Price;
        private double[,] Bid;
        private double[,] Ask;

        private bool[] InversionDirection;
        private string commonCurrencyInBasket; //Common currency in the given basket
        private double[] resolution;
        private double[,] pipCostSeries;
        private int n_data = 0;              //Number of total bars       
        public int Window { set; get; }
        public int MaxOpenPositions { set; get; }
        public string Timeframe { set; get; }
        public double TransactionCost { set; get; }
        public double InitialDeposit { set; get; }
        public int Leverage { set; get; }

        #endregion

        public MegaOptimization(string[] Basket_, string TimeFrame_)
        {
            #region Initialize data
            Basket = Basket_;
            Timeframe = TimeFrame_;
            n_data = Global.Instance.TotalBars[Timeframe];
            commonCurrencyInBasket = Global.Instance.CommonCurrency(Basket);
            InversionDirection = Global.Instance.InversionCurrencyPair(Basket, commonCurrencyInBasket);
            Price = new double[Basket.Length, n_data];
            Bid = new double[Basket.Length, n_data];
            Ask = new double[Basket.Length, n_data];
            resolution = new double[Basket.Length];

            #endregion

            #region load data
            for (int i = 0; i < Basket.Length; i++)
            {
                resolution[i] = (Basket[i].Contains("JPY")) ? 0.01 : 0.0001;
                using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + Timeframe + "\\" + Basket[i] + ".csv"))
                {
                    string line = null;
                    int j = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] ext = line.Split(',');
                        double bid = double.Parse(ext[1]);
                        double ask = double.Parse(ext[2]);
                        Price[i, j] = (bid + ask) / 2;
                        Bid[i, j] = bid;
                        Ask[i, j] = ask;
                        if (InversionDirection[i])
                        {
                            Price[i, j] = 1 / Price[i, j]; //Inversion
                        }

                        j++;
                    }
                    reader.Close();
                }
            }
            #endregion

            #region Work on pip costs

            pipCostSeries = new double[Basket.Length, n_data];


            for (int i = 0; i < Basket.Length; i++)
            {
                string countercurrency = Basket[i].Substring(3, 3);
                if (countercurrency == "USD") //For USD countercurrency, pip cost is always 1
                {
                    for (int j = 0; j < n_data; j++)
                    {
                        pipCostSeries[i, j] = 1;
                    }
                }
                else
                {
                    if (File.Exists(Global.Instance.data_folder_path + "\\" + Timeframe + "\\" + countercurrency + "USD" + ".csv"))  //If the currency is XXXUSD
                    {
                        using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + Timeframe + "\\" + countercurrency + "USD" + ".csv"))
                        {
                            string line = null;
                            int j = 0;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] ext = line.Split(',');
                                pipCostSeries[i, j] = Math.Round(double.Parse(ext[2]), 2);
                                j++;
                            }
                            reader.Close();
                        }
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + Timeframe + "\\USD" + countercurrency + ".csv"))
                        {
                            string line = null;
                            int j = 0;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] ext = line.Split(',');
                                if (countercurrency == "JPY") //For XXXJPY based
                                {
                                    pipCostSeries[i, j] = Math.Round(100 / double.Parse(ext[2]), 2);
                                }
                                else
                                {
                                    pipCostSeries[i, j] = Math.Round(1 / double.Parse(ext[2]), 2);
                                }
                                j++;
                            }
                            reader.Close();
                        }

                    }
                }

            }

            #endregion
        }

        public StrategyTesterResult[,] Run()
        {
            #region Initialize variables
            StrategyTesterResult[,] resultset = new StrategyTesterResult[Basket.Length,19];
            List<TradeInfo>[,] TRADES = new List<TradeInfo>[Basket.Length, 19];
            int[,] OpenTradePositionIndex = new int[Basket.Length, 19];

            #endregion

            #region Fill up initial results
            for (int i = 0; i < Basket.Length; i++)
            {
                for (int j=0; j<19; j++)
                {
                    resultset[i, j] = new StrategyTesterResult();
                    resultset[i, j].Basket = Basket;
                    resultset[i, j].window = Window;                   
                    resultset[i, j].maximum_open_positions = MaxOpenPositions;
                    resultset[i, j].final_balance = InitialDeposit;
                    resultset[i, j].balance_history = new List<double>();
                    resultset[i, j].balance_history.Add(InitialDeposit);
                    resultset[i, j].transactionCost = TransactionCost;
                    resultset[i, j].initialDeposit = InitialDeposit;
                    resultset[i, j].leverage = Leverage;
                    TRADES[i, j] = new List<TradeInfo>();
                }
            }
            #endregion


            for (int i = Window - 1; i < n_data; i++)
            {
                #region Regression
                double[,] xy = new double[Window, Basket.Length];
                for (int j = 0; j < Window; j++)
                {
                    for (int k = 0; k < Basket.Length; k++)
                    {
                        xy[j, k] = Price[k, i - j];
                    }
                }
                double[] x = new double[Basket.Length];               
                for (int j = 0; j < Basket.Length; j++)
                {                  
                    x[j] = Price[j, i];
                }

                double[][] W = new double[Basket.Length][];
                for (int j = 0; j < Basket.Length; j++)
                {
                    W[j] = LinearRegression.Build(xy, j);
                }
                #endregion

                for (int j = 0; j < Basket.Length; j++)
                {
                    double[] w = W[j];

                    #region Calculate SD
                    double sum = 0;
                    double sd = 0;
                    double mean = 0;
                    double upperBand, lowerBand, deviation;

                    for (int k = 1; k < Window; k++)
                    {
                        sum += LinearRegression.Predict(Price, i - k, w);
                    }
                    mean = sum / Window;
                    sum = 0;
                    for (int k = 1; k < Window; k++)
                    {
                        double val = LinearRegression.Predict(Price, i - k, w);
                        sum += ((val - mean) * (val - mean));
                    }
                    sd = Math.Sqrt(sum / Window);
                    #endregion

                    for (int k = 0; k < 19; k++)
                    {
                        List<TradeInfo> Trades = TRADES[j, k];
                        StrategyTesterResult result = resultset[j, k];
                        result.bandwidth = (k + 1) * 0.25;
                       
                        #region Calculate bands
                        deviation = (k + 1) * 0.25 * sd;
                        upperBand = mean + deviation;
                        lowerBand = mean - deviation;                        
                        #endregion
                        
                        #region Locate current and previous positions
                        int current_position = 0;
                        int previous_position = 0;
                        double current_val = LinearRegression.Predict(Price, i, w);
                        double previous_val = LinearRegression.Predict(Price, i - 1, w);

                        if (current_val >= upperBand)
                        {
                            current_position = 1;
                        }
                        else if (current_val < upperBand && current_val > mean)
                        {
                            current_position = 2;
                        }
                        else if (current_val <= mean && current_val > lowerBand)
                        {
                            current_position = 3;
                        }
                        else
                        {
                            current_position = 4;
                        }

                        if (previous_val >= upperBand)
                        {
                            previous_position = 1;
                        }
                        else if (previous_val < upperBand && previous_val > mean)
                        {
                            previous_position = 2;
                        }
                        else if (previous_val <= mean && previous_val > lowerBand)
                        {
                            previous_position = 3;
                        }
                        else
                        {
                            previous_position = 4;
                        }
                        #endregion

                        #region Trades
                        //######################### Close open trades #####################################################

                        foreach (TradeInfo trade in Trades)
                        {
                            double v = LinearRegression.Predict(Price, i, trade.Coefficients);
                            if (trade.Action == 1)
                            {
                                #region Check Buy Order
                                if (v >= trade.TP)
                                {
                                    //TP is hit. Winner :)

                                    trade.IsClosed = true;
                                    trade.close_index = i;
                                    result.tradedurations.Add(i - trade.open_index);
                                    double estimatepips = 0;
                                    double profit = GetProfit(trade, out estimatepips);
                                    updateResults(result, 1, profit, estimatepips);

                                    if (profit < 0)
                                    {
                                        result.total_TP_mismatch++;
                                    }

                                }
                                else if (v <= trade.SL)
                                {
                                    //SL is hit. Loser :(

                                    trade.IsClosed = true;
                                    trade.close_index = i;
                                    result.tradedurations.Add(i - trade.open_index);

                                    double estimatepips = 0;
                                    double profit = GetProfit(trade, out estimatepips);
                                    updateResults(result, 1, profit, estimatepips);

                                    if (profit > 0)
                                    {
                                        result.total_SL_mismatch++;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region Check Sell Order
                                if (v <= trade.TP)
                                {
                                    //TP is hit. Winner :)

                                    trade.IsClosed = true;
                                    trade.close_index = i;
                                    result.tradedurations.Add(i - trade.open_index);

                                    double estimatepips = 0;
                                    double profit = GetProfit(trade, out estimatepips);
                                    updateResults(result, 1, profit, estimatepips);

                                    if (profit < 0)
                                    {
                                        result.total_TP_mismatch++;
                                    }
                                }
                                else if (v >= trade.SL)
                                {
                                    //SL is hit. Loser :(

                                    trade.IsClosed = true;
                                    trade.close_index = i;
                                    result.tradedurations.Add(i - trade.open_index);

                                    double estimatepips = 0;
                                    double profit = GetProfit(trade, out estimatepips);
                                    updateResults(result, 1, profit, estimatepips);

                                    if (profit > 0)
                                    {
                                        result.total_SL_mismatch++;
                                    }
                                }
                                #endregion
                            }
                        }
                        Trades.RemoveAll(trade => trade.IsClosed);


                        //################################## Open trade signals ######################################
                        if (Trades.Count < MaxOpenPositions)
                        {
                            if (previous_position == 1 && current_position == 2)
                            {
                                //Sell order
                                #region Execute sell order
                                double[] c = LinearRegression.NormalizeWeights(w, x, InversionDirection);
                                int[] lotsizes = RiskManager.getTradeSizes(c, result.final_balance, Leverage);
                                if (lotsizes != null)
                                {
                                    TradeInfo trade = new TradeInfo();
                                    trade.Action = -1;
                                    trade.Coefficients = w;
                                    trade.IsClosed = false;
                                    trade.TP = mean;
                                    trade.SL = mean + 2 * deviation;
                                    trade.TradeSizes = lotsizes;
                                    trade.open_index = i;
                                    trade.IsClosed = false;
                                    Trades.Add(trade);

                                    result.tradesizes.Add(LinearRegression.TotalNormalizedWeights(w, x, InversionDirection));
                                }
                                else
                                {
                                    result.total_reject_trades++;
                                }
                                #endregion

                            }
                            else if (previous_position == 4 & current_position == 3)
                            {
                                //Buy order
                                #region Execute buy order
                                double[] c = LinearRegression.NormalizeWeights(w, x, InversionDirection);
                                int[] lotsizes = RiskManager.getTradeSizes(c, result.final_balance, Leverage);
                                if (lotsizes != null)
                                {
                                    TradeInfo trade = new TradeInfo();
                                    trade.Action = -1;
                                    trade.Coefficients = w;
                                    trade.IsClosed = false;
                                    trade.TP = mean;
                                    trade.SL = mean - 2 * deviation;
                                    trade.TradeSizes = lotsizes;
                                    trade.open_index = i;
                                    trade.IsClosed = false;
                                    Trades.Add(trade);

                                    result.tradesizes.Add(LinearRegression.TotalNormalizedWeights(w, x, InversionDirection));                                   
                                }
                                else
                                {
                                    result.total_reject_trades++;
                                }
                                #endregion
                            }
                        }
                        //#####################################################################################
                        #endregion
                    }
                }
            }

            #region Post Mortem
            for (int i = 0; i < Basket.Length; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    resultset[i, j].postMortem();
                    resultset[i, j].balance_history = null;
                    resultset[i, j].tradesizes = null;
                    resultset[i, j].tradedurations = null;
                }
            }

            #endregion

            return resultset;
        }



        private double price_to_pip(double price1, double price2, int whichpair)
        {
            double net = price1 - price2;
            net /= resolution[whichpair];
            return Math.Round(net, 2);
        }
        private void updateResults(StrategyTesterResult result, int Action, double profit, double profitpip = 0)
        {
            result.total_trades++;
            if (profit > 0)
            {
                result.total_profit_trades++;
                result.TradeResults.Add(1);
                result.pips_won += profitpip;
                if (profitpip > result.pips_won_max)
                {
                    result.pips_won_max = profitpip;
                }
                if (profitpip < result.pips_won_min)
                {
                    result.pips_won_min = profitpip;
                }

                if (Action == 1)
                {
                    result.total_long_trades++;
                    result.total_long_trades_profitable++;
                }
                else
                {
                    result.total_short_trades++;
                    result.total_short_trades_profitable++;
                }
            }
            else
            {
                result.total_loss_trades++;
                result.TradeResults.Add(-1);
                result.pips_loss += profitpip;
                if (profitpip < result.pips_loss_max)
                {
                    result.pips_loss_max = profitpip;
                }
                if (profitpip > result.pips_loss_min)
                {
                    result.pips_loss_min = profitpip;
                }

                if (Action == 1)
                {
                    result.total_long_trades++;
                    result.total_long_trades_loss++;
                }
                else
                {
                    result.total_short_trades++;
                    result.total_short_trades_loss++;
                }
            }

            result.final_balance += profit;
            result.balance_history.Add(result.final_balance);
        }


        private double GetProfit(TradeInfo trade, out double estimate_pips)
        {
            double net = 0;
            int trade_size_total = 0;
            for (int i = 0; i < Basket.Length; i++)
            {
                double pipcost = pipCostSeries[i, trade.close_index] / 10;  //pip cost per 1K contract
                double pips = 0;
                if ((trade.Action == -1 && trade.TradeSizes[i] > 0) || (trade.Action == 1 && trade.TradeSizes[i] < 0))
                {
                    //Sell
                    pips = price_to_pip(Bid[i, trade.open_index], Ask[i, trade.close_index], i);
                }
                else if ((trade.Action == 1 && trade.TradeSizes[i] > 0) || (trade.Action == -1 && trade.TradeSizes[i] < 0))
                {
                    //Buy
                    pips = price_to_pip(Bid[i, trade.close_index], Ask[i, trade.open_index], i);
                }
                pips -= TransactionCost;
                net += pips * pipcost * Math.Abs(trade.TradeSizes[i]);
                trade_size_total += Math.Abs(trade.TradeSizes[i]);
            }
            if (trade_size_total != 0)
            {
                estimate_pips = (10 / (double)trade_size_total) * net;
            }
            else
            {
                estimate_pips = 0;
            }

            return net;
        }


    }
}
