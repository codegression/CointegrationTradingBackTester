using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BacktestCointegration
{
    class Global
    {
        private static volatile Global m_instance;
        private static object m_syncRoot = new Object();

        public string data_folder_path = @"";

        public static Global Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new Global();
                        }
                    }
                }

                return m_instance;
            }
        }


        public List<string> counterCurrencies = new List<string>();
        


        public string[] Symbols = new string[]{
            "AUDCAD",
            "AUDCHF",
            "AUDJPY",
            "AUDNZD",
            "AUDUSD",
            "CADCHF",
            "CADJPY",
            "CHFJPY",
            "EURAUD",
            "EURCAD",
            "EURCHF",
            "EURGBP",
            "EURJPY",
            "EURNZD",
            "GBPAUD",
            "GBPCAD",
            "GBPCHF",
            "GBPJPY",
            "GBPNZD",
            "GBPUSD",
            "NZDCAD",
            "NZDCHF",
            "NZDJPY",
            "NZDUSD",
            "USDCAD",
            "USDCHF",
            "USDJPY",
            "EURUSD"
            };

        public Dictionary<string, int> DecimalPlaces = new Dictionary<string, int>();
        public Dictionary<string, int> TotalBars = new Dictionary<string, int>();

     
                


        public Global()
        {

            TotalBars.Add("m5", 75738);
            TotalBars.Add("m15", 25255);
            TotalBars.Add("m30", 12634);
            TotalBars.Add("H1", 6327);
            TotalBars.Add("H2", 3177);
            TotalBars.Add("H3", 2118);
            TotalBars.Add("H4", 1602);
            TotalBars.Add("H6", 1086);
            TotalBars.Add("H8", 829);
            TotalBars.Add("D1", 311);

            //FXCM 2   (2010 to 2011)
            //TotalBars.Add("m5", 74501);
            //TotalBars.Add("m15", 24865);
            //TotalBars.Add("m30", 12458);
            //TotalBars.Add("H1", 6254);
            //TotalBars.Add("H2", 3128);
            //TotalBars.Add("H3", 2086);
            //TotalBars.Add("H4", 1564);
            //TotalBars.Add("H6", 1044);
            //TotalBars.Add("H8", 783);
            //TotalBars.Add("D1", 261);

            counterCurrencies.Add("USD");
            counterCurrencies.Add("AUD");
            counterCurrencies.Add("EUR");
            counterCurrencies.Add("GBP");
            counterCurrencies.Add("NZD");
            counterCurrencies.Add("CAD");
            counterCurrencies.Add("CHF");
            counterCurrencies.Add("JPY");
          
        }

        public void DoNothing()
        {
          
        }

        /*
         * This function returns the common currency in a basket of currency pairs
         * e.g.  If basket = {GBPUSD, USDJPY, USDCHF}, this function will return "USD". It is assumed that pairs in the basket has a common currency
         */
        public string CommonCurrency(string[] basket)
        {
            if (basket[0].Substring(0, 3)==basket[1].Substring(0, 3))
            {
                return basket[0].Substring(0, 3);
            }
            else if (basket[0].Substring(0, 3) == basket[1].Substring(3, 3))
            {
                return basket[0].Substring(0, 3);
            }
            else if (basket[0].Substring(3, 3) == basket[1].Substring(0, 3))
            {
                return basket[0].Substring(3, 3);
            }
            else
            {
                return basket[0].Substring(3, 3);
            }
        }

        public bool[] InversionCurrencyPair(string[] basket, string commoncurrency)
        {
            bool[] result = new bool[basket.Length];
            for (int i=0; i<basket.Length;i++)
            {
                result[i] = (basket[i].Substring(0, 3) == commoncurrency) ? true : false;
            }
            return result;
        }

        public double MaxDrawnDownPert(double[] equity, out int drawdown_position)
        {
            double MDD = 0;
            double peak = double.NegativeInfinity;
            double[] DD = new double[equity.Length];
            drawdown_position = 0;
            for (int i = 0; i < equity.Length; i++)
            {
                if (equity[i] > peak)
                {
                    peak = equity[i];
                }
                else
                {
                    DD[i] = 100.0 * (peak - equity[i]) / peak;
                    if (DD[i] > MDD)
                    {
                        MDD = DD[i];
                        drawdown_position = i;
                    }
                }
            }

            return MDD;

        }

        public double MaxDrawnDown(double[] equity, out int drawdown_position)
        {
            double MDD = 0;
            double peak = double.NegativeInfinity;
            double[] DD = new double[equity.Length];
            drawdown_position = 0;
            for (int i = 0; i < equity.Length; i++)
            {
                if (equity[i] > peak)
                {
                    peak = equity[i];
                }
                else
                {
                    DD[i] = peak - equity[i];
                    if (DD[i] > MDD)
                    {
                        MDD = DD[i];
                        drawdown_position = i;
                    }
                }
            }

            return MDD;

        }



        public double PositionSize(double equity, double leverage=1)
        {
            double size;
            
            size = Math.Floor((equity * leverage) / 1000);
            if (size < 1)
            {
                size = 1;
            }
           
            return size/10;
        }
        public void MaximumWinsLosses(int[] results, out int maxwins, out int maxlosses)
        {
            int wins = 0, losses = 0;
            maxwins = 0;
            maxlosses = 0;


            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] <= 0)
                {
                    wins = 0;                    
                    losses++;                   
                    if (losses > maxlosses)
                    {
                        maxlosses = losses;
                    }                  
                }
                else
                {
                    losses = 0;                   
                    wins++;                  
                    if (wins > maxwins)
                    {
                        maxwins = wins;
                    }                   
                }
            }

        }
        public void MaximumWinsLosses(double[] results, out int maxwins, out int maxlosses, out double max_win_pips, out double max_loss_pips)
        {
            int wins = 0, losses = 0;
            maxwins = 0;
            maxlosses = 0;

            max_win_pips = double.NegativeInfinity;
            max_loss_pips = double.PositiveInfinity;
            double winpips = 0;
            double losspips = 0;

            for (int i=0; i<results.Length; i++)
            {
                if (results[i] <= 0)
                {
                    wins = 0;
                    winpips = 0;
                    losses++;
                    losspips += results[i];
                    if (losses > maxlosses)
                    {
                        maxlosses = losses;
                    }
                    if (losspips < max_loss_pips)
                    {
                        max_loss_pips = losspips;
                    }
                }
                else
                {
                    losses = 0;
                    losspips = 0;
                    wins++;
                    winpips += results[i];
                    if (wins > maxwins)
                    {
                        maxwins = wins;
                    }
                    if (winpips > max_win_pips)
                    {
                        max_win_pips = winpips;
                    }
                }               
            }
            
        }
    }
}
