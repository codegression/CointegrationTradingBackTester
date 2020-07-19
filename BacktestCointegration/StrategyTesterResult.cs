using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BacktestCointegration
{
    [Serializable]
    public class StrategyTesterResult
    {
         public int total_trades;        
         public double accuracy;
         public double expected_profit;
         public double final_balance;
         public List<double> balance_history;
         public double maximum_drawdown;
         public double estimate_monthly_profit;

         public int total_short_trades;
         public int total_long_trades;
         public int total_profit_trades;
         public int total_loss_trades;
         public int total_reject_trades;
         public int total_short_trades_profitable;
         public int total_short_trades_loss;
         public int total_long_trades_profitable;
         public int total_long_trades_loss;
         public int total_SL_mismatch;
         public int total_TP_mismatch;
         public int maximum_consecutive_wins;
         public int maximum_consecutive_losses;
         public double maximum_consecutive_losses_factor;
         public string title;
        
         //Trades
         public List<int> TradeResults = new List<int>();

         //Percentage
         public double percentage_short_trades;
         public double percentage_long_trades;
         public double percentage_profit_trades;
         public double percentage_loss_trades;
         public double percentage_short_trades_profitable;
         public double percentage_short_trades_loss;
         public double percentage_long_trades_profitable;
         public double percentage_long_trades_loss;

         //Parameters 
          //These are just to display on Strategy Tester Report. 
         public string[] Basket;
         public int window = 40;
         public double bandwidth = 2;
         public double transactionCost = 0;
         public double initialDeposit = 0;
         public double leverage = 10;
         public int maximum_open_positions;

         //Trade sizes
        public List<double> tradesizes = new List<double>();
        public double tradesize_max;
        public double tradesize_min;
        public double tradesize_mean;
        public double tradesize_median;
        public double tradesize_sd;

        //Tradedurations
        public List<int> tradedurations = new List<int>();
        public double tradeduration_max;
        public double tradeduration_min;
        public double tradeduration_mean;
        public double tradeduration_median;
        public double tradeduration_sd;

        //Pips
        public double pips_won;
        public double pips_won_min = double.PositiveInfinity;
        public double pips_won_max = double.NegativeInfinity;
        public double pips_won_avg;
        public double pips_loss;
        public double pips_loss_min = double.NegativeInfinity;
        public double pips_loss_max = double.PositiveInfinity;
        public double pips_loss_avg;
        public double pips_net;
      

         public void postMortem()
         {
             pips_won_avg = Math.Round(pips_won / total_profit_trades, 1);
             pips_loss_avg = Math.Round(pips_loss / total_loss_trades, 1);
             pips_won = Math.Round(pips_won, 1);
             pips_won_min = Math.Round(pips_won_min, 1);
             pips_won_max = Math.Round(pips_won_max, 1);
             pips_loss = Math.Round(pips_loss, 1);
             pips_loss_min = Math.Round(pips_loss_min, 1);
             pips_loss_max = Math.Round(pips_loss_max, 1);
             final_balance = Math.Round(final_balance, 2);
             pips_net = Math.Round(pips_won + pips_loss, 2);
             estimate_monthly_profit = Math.Round(pips_net/12, 2);


             accuracy = (total_trades != 0) ? Math.Round((double)((total_profit_trades * 100) / total_trades), 1) : 0;
             percentage_short_trades = ((total_trades != 0) ? Math.Round((double)(total_short_trades * 100) / total_trades, 1) : 0);
             percentage_long_trades = ((total_trades != 0) ? Math.Round((double)(total_long_trades * 100) / total_trades,1) : 0);
             percentage_profit_trades = ((total_trades != 0) ? Math.Round((double)(total_profit_trades * 100) / total_trades, 1) : 0);
             percentage_loss_trades = ((total_trades != 0) ? Math.Round((double)(total_loss_trades * 100) / total_trades, 1) : 0);
             percentage_short_trades_profitable = ((total_short_trades != 0) ? Math.Round((double)(total_short_trades_profitable * 100) / total_short_trades, 1) : 0);
             percentage_short_trades_loss = ((total_short_trades != 0) ? Math.Round((double)(total_short_trades_loss * 100) / total_short_trades, 1) : 0);
             percentage_long_trades_profitable = ((total_long_trades != 0) ? Math.Round((double)(total_long_trades_profitable * 100) / total_long_trades, 1) : 0);
             percentage_long_trades_loss = ((total_long_trades != 0) ? Math.Round((double)(total_long_trades_loss * 100) / total_long_trades, 1) : 0);
             Global.Instance.MaximumWinsLosses(TradeResults.ToArray(), out maximum_consecutive_wins, out maximum_consecutive_losses);
             TradeResults.Clear();
             TradeResults = null;
             maximum_consecutive_losses_factor = maximum_consecutive_losses * bandwidth;
             expected_profit = (total_profit_trades - total_loss_trades) * bandwidth;
             
             //Handle trade sizes
             if (tradesizes.Count > 0)
             {
                 double sum = 0;
                 tradesize_max = double.NegativeInfinity;
                 tradesize_min = double.PositiveInfinity;
                 for (int i = 0; i < tradesizes.Count; i++)
                 {
                     sum += tradesizes[i];

                     if (tradesizes[i] > tradesize_max)
                     {
                         tradesize_max = tradesizes[i];
                     }
                     if (tradesizes[i] < tradesize_min)
                     {
                         tradesize_min = tradesizes[i];
                     }
                 }
                 tradesize_mean = sum / tradesizes.Count;
                 sum = 0;
                 for (int i = 0; i < tradesizes.Count; i++)
                 {
                     sum += ((tradesizes[i] - tradesize_mean) * (tradesizes[i] - tradesize_mean));
                 }
                 tradesize_sd = Math.Round(Math.Sqrt(sum / tradesizes.Count), 2);
                 tradesize_mean = Math.Round(tradesize_mean, 2);
                 tradesizes.Sort();
                 tradesize_median = tradesizes[(int)Math.Floor((double)(tradesizes.Count / 2))];
                 tradesizes.Clear();
                 tradesizes = null;
             }

             //Handle trade durations
             if (tradedurations.Count > 0)
             {
                 double sum = 0;
                 tradeduration_max = double.NegativeInfinity;
                 tradeduration_min = double.PositiveInfinity;
                 for (int i = 0; i < tradedurations.Count; i++)
                 {
                     sum += tradedurations[i];

                     if (tradedurations[i] > tradeduration_max)
                     {
                         tradeduration_max = tradedurations[i];
                     }
                     if (tradedurations[i] < tradeduration_min)
                     {
                         tradeduration_min = tradedurations[i];
                     }
                 }
                 tradeduration_mean = sum / tradedurations.Count;
                 sum = 0;
                 for (int i = 0; i < tradedurations.Count; i++)
                 {
                     sum += ((tradedurations[i] - tradeduration_mean) * (tradedurations[i] - tradeduration_mean));
                 }
                 tradeduration_sd = Math.Round(Math.Sqrt(sum / tradedurations.Count), 2);
                 tradeduration_mean = Math.Round(tradeduration_mean, 2);
                 tradedurations.Sort();
                 tradeduration_median = tradedurations[(int)Math.Floor((double)(tradedurations.Count / 2))];
                 tradedurations.Clear();
                 tradedurations = null;
             }
         }
    }

}
