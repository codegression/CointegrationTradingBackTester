using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BacktestCointegration
{
    public class Optimization
    {
        #region Local Variables
        private string Symbol1, Symbol2;                        //e.g.  Symbol1 = EURUSD, Symbol2=GBPUSD
        private int Symbol1CounterIndex, Symbol2CounterIndex;   //Counter currency of Symbol1 and Symbol2.  e.g.  if Symbol1 = EURUSD, Countercurrency = USD, therefore Symbol1CounterIndex=0,  (USD corresponds to 0 as defined in Global.counterCurrencies list
        private double[] Pair1, Pair2, Ratio;                   //Price series for symbol1, symbol 2  and the ratio of two series                
        private double[,] pipCostSeries;                        //Pip cost series. 
        private double[] resolution;                            //Smallest possible change to a currency pair.. JPY based currency = 0.01, others = 0.0001.. Resolution[0] = resolution of pair1, Resolution[1] = resolution of pair2
        private int n_data = 0;                                 //Number of total bars
        private DateTime[] Date1, Date2;                        //Datetime series of Symbol1 and Symbol2
        private int period = 40;
        private double transactionCost = 0;
        private double initialDeposit = 0;
        private int leverage = 10;
        private int correlationDirection = 1;
        private string timeframe = "m5";


        #endregion




        public Optimization(string[] basket, string TimeFrame)
        {
            #region Initialize data
            Symbol1 = basket[0];
            Symbol2 = basket[0];
            timeframe = TimeFrame;
            n_data = Global.Instance.TotalBars[timeframe];

            Date1 = new DateTime[n_data];
            Date2 = new DateTime[n_data];
            Pair1 = new double[n_data];
            Pair2 = new double[n_data];
            Ratio = new double[n_data];

            resolution = new double[2];
            resolution[0] = (Symbol1.Contains("JPY")) ? 0.01 : 0.0001;
            resolution[1] = (Symbol2.Contains("JPY")) ? 0.01 : 0.0001;

         
            #endregion
            #region load data
            using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + timeframe + "\\" + Symbol1 + ".csv"))
            {
                string line = null;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] ext = line.Split(',');
                    Date1[i] = DateTime.Parse(ext[0] + " " + ext[1]);
                    Pair1[i] = double.Parse(ext[2]);
                    i++;
                }
                reader.Close();
            }
            using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + timeframe + "\\" + Symbol2 + ".csv"))
            {
                string line = null;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] ext = line.Split(',');
                    Date2[i] = DateTime.Parse(ext[0] + " " + ext[1]);
                    Pair2[i] = double.Parse(ext[2]);
                    i++;
                }
                reader.Close();
            }
            for (int i = 0; i < n_data; i++)
            {
                Ratio[i] = Pair1[i] / Pair2[i];
            }
            #endregion
            #region Work on pip costs

            pipCostSeries = new double[Global.Instance.counterCurrencies.Count, n_data];

            Symbol1CounterIndex = Global.Instance.counterCurrencies.IndexOf(Symbol1.Substring(3, 3));
            Symbol2CounterIndex = Global.Instance.counterCurrencies.IndexOf(Symbol2.Substring(3, 3));


            for (int i = 0; i < Global.Instance.counterCurrencies.Count; i++)
            {

                if (Global.Instance.counterCurrencies[i] == "USD") //For USD countercurrency, pip cost is always 1
                {
                    for (int j = 0; j < n_data; j++)
                    {
                        pipCostSeries[i, j] = 1;
                    }
                }
                else
                {
                    if (File.Exists(Global.Instance.data_folder_path + "\\" + timeframe + "\\" + Global.Instance.counterCurrencies[i] + "USD" + ".csv"))  //If the currency is XXXUSD
                    {
                        using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + timeframe + "\\" + Global.Instance.counterCurrencies[i] + "USD" + ".csv"))
                        {
                            string line = null;
                            int j = 0;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] ext = line.Split(',');
                                pipCostSeries[i, j] = Math.Round(double.Parse(ext[2]), 1);
                                j++;
                            }
                            reader.Close();
                        }
                    }
                    else
                    {
                        using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + timeframe + "\\USD" + Global.Instance.counterCurrencies[i] + ".csv"))
                        {
                            string line = null;
                            int j = 0;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] ext = line.Split(',');
                                if (Global.Instance.counterCurrencies[i] == "JPY") //For XXXJPY based
                                {
                                    pipCostSeries[i, j] = Math.Round(100 / double.Parse(ext[2]), 1);
                                }
                                else
                                {
                                    pipCostSeries[i, j] = Math.Round(1 / double.Parse(ext[2]), 1);
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

        public StrategyTesterResult[] Run()
        {  
            StrategyTesterResult[] result = new StrategyTesterResult[40];
            #region Initialize Results
            for (int i = 0; i < 40; i++)
            {
                result[i] = new StrategyTesterResult();
                //result[i].Symbol1 = Symbol1;
                //result[i].Symbol2 = Symbol2;
                result[i].window = period;
                result[i].bandwidth = (i + 1) * 0.125;
                result[i].transactionCost = transactionCost;
                result[i].initialDeposit = initialDeposit;
                result[i].leverage = leverage;
            }
            #endregion
            #region Critial variables for trading system
            int[] current_order = new int[40];
            int[] previous_position = new int[40];
            int[] current_position = new int[40];
            double[] openprice1 = new double[40];
            double[] openprice2 = new double[40];           
            #endregion
                                  

            for (int i = period - 1; i < n_data; i++)
            {
                    double sum = 0;
                    double sd = 0;
                    double mean = 0;
                

                    for (int j = 0; j < period; j++)
                    {
                        sum += Ratio[i - j];
                    }
                    mean = sum / period;
                    sum = 0;
                    for (int j = 0; j < period; j++)
                    {
                        sum += ((Ratio[i - j] - mean) * (Ratio[i - j] - mean));
                    }
                    sd = Math.Sqrt(sum / period);

                    for (int j = 0; j < 40; j++)
                    {
                            #region Main Loop
                            double deviation = (j+1) * 0.125 * sd;
                            double upperBand = mean + deviation;
                            double lowerBand = mean - deviation;
                                        

                            if (Ratio[i] >= upperBand)
                            {
                                current_position[j] = 1;
                            }
                            else if (Ratio[i] < upperBand && Ratio[i] > mean)
                            {
                                current_position[j] = 2;
                            }
                            else if (Ratio[i] <= mean && Ratio[i] > lowerBand)
                            {
                                current_position[j] = 3;
                            }
                            else
                            {
                                current_position[j] = 4;
                            }


                            //######################### Close open trades #####################################################


                            if (current_position[j] >= 3 && current_order[j] == -1)
                            {
                                //Close sell order
                                #region close sell order

                                double pip1 = price_to_pip(openprice1[j], Pair1[i], 0);
                                double pip2 = price_to_pip(Pair2[i], openprice2[j], 1) * correlationDirection;

                                double pipcost1 = pipCostSeries[Symbol1CounterIndex, i];
                                double pipcost2 = pipCostSeries[Symbol2CounterIndex, i];


                                double netprofit = pip1 * pipcost1 + pip2 * pipcost2;
                                netprofit -= (transactionCost * pipcost1 + transactionCost * pipcost2);  //minus transaction cost from the profit. Note that there are two transactions

                                current_order[j] = 0;
                                #region resultupdate
                               
                              
                                result[j].total_trades++;
                                result[j].total_short_trades++;

                                if (netprofit < 0)
                                {
                                    result[j].total_loss_trades++;
                                   
                                    result[j].total_short_trades_loss++;

                               


                                }
                                else
                                {
                                    result[j].total_profit_trades++;
                                   
                                    result[j].total_short_trades_profitable++;

                                 
                                }
                                #endregion
                                #endregion

                            }
                            else if (current_position[j] <= 2 && current_order[j] == 1)
                            {
                                //Close Buy order
                                #region close buy order

                                double pip1 = price_to_pip(Pair1[i], openprice1[j], 0);  //Get profit of this buy order (first currency)
                                double pip2 = price_to_pip(openprice2[j], Pair2[i], 1) * correlationDirection;  //in the case of positively correlated pair, the second currency is a sell..  For negatively correlated pair, it is a buy

                                double pipcost1 = pipCostSeries[Symbol1CounterIndex, i];
                                double pipcost2 = pipCostSeries[Symbol2CounterIndex, i];

                                double netprofit = pip1 * pipcost1 + pip2 * pipcost2;
                                netprofit -= (transactionCost * pipcost1 + transactionCost * pipcost2);  //minus transaction cost from the profit. Note that there are two transactions

                                current_order[j] = 0;
                                #region resultupdate
                                                    
                                result[j].total_trades++;
                                result[j].total_long_trades++;

                                if (netprofit < 0)
                                {
                                    result[j].total_loss_trades++;                                  
                                    result[j].total_long_trades_loss++;

                                   

                                }
                                else
                                {

                                    result[j].total_profit_trades++;                                    
                                    result[j].total_long_trades_profitable++;
                                   
                                }
                                #endregion
                                #endregion

                            }
                            //################################## Open trade signals ######################################



                            if (previous_position[j] == 1 && current_position[j] == 2 && current_order[j] == 0)
                            {
                                //Sell order
                                #region Execute sell order
                                current_order[j] = -1; //Sell corresponds to -1                   
                                openprice1[j] = Pair1[i];
                                openprice2[j] = Pair2[i];

                                
                                #endregion

                            }
                            else if (previous_position[j] == 4 & current_position[j] == 3 && current_order[j] == 0)
                            {
                                //Buy order
                                #region Execute buy order
                                current_order[j] = 1;   //Buy corresponds to +1
                                openprice1[j] = Pair1[i];
                                openprice2[j] = Pair2[i];

                               
                                #endregion
                            }
                            //#####################################################################################


                            previous_position[j] = current_position[j];
                            #endregion

                    }                

            }

            for (int i = 0; i < 40; i++)
            {
                result[i].postMortem();
            }
            return result;
        }


        private double price_to_pip(double price1, double price2, int whichpair)
        {
            double net = price1 - price2;
            net /= resolution[whichpair];
            return Math.Round(net, 1);
        }
        public string Timeframe
        {
            get { return timeframe; }
            set { timeframe = value; }
        }
        public int Period
        {
            get { return period; }
            set { period = value; }
        }
       
        public double TransactionCost
        {
            get { return transactionCost; }
            set { transactionCost = value; }
        }
        public double InitialDeposit
        {
            get { return initialDeposit; }
            set { initialDeposit = value; }
        }
        public int Leverage
        {
            get { return leverage; }
            set { leverage = value; }
        }

    }
}
