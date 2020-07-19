using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BacktestCointegration
{
    class RiskManager
    {
        public static int[] getTradeSizes(double[] Coefficients, double equity, double leverage)
        {
            /*
             * This function return an array of trade sizes (in K) given a list of coefficients.
             * 
             */
            //try
            {
                double allowance = 0;
                if (leverage > 0)
                {
                    allowance = Math.Floor((equity * leverage) / 1000);
                }
                else
                {
                    allowance = leverage * -1;
                }

                int[] tradesizes = new int[Coefficients.Length];
                double total = 0;

                for (int i = 0; i < Coefficients.Length; i++)
                {
                    total += Math.Abs(Coefficients[i]);
                }
                if (equity < 0)
                {
                    return null; 
                }
                if (total > allowance || total > 50000 || total == 0)
                {
                    return null;
                }
                for (int i = 0; i < Coefficients.Length; i++)
                {
                    tradesizes[i] = (int)Math.Round((allowance / total) * Coefficients[i]);
                }
                return tradesizes;

            }
            //catch
            //{
            //    return null;
            //}
        }
    }
}
