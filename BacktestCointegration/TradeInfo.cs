using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BacktestCointegration
{
    public class TradeInfo
    {
        public int Action { get; set; }              //Buy = 1, Sell = -1
        public double TP { get; set; }               //Take profit limit
        public double SL { get; set; }               //Stop loss
        public double[] Coefficients { get; set; }   //Regression coefficients that were used to open this trade
        public int[] TradeSizes { get; set; }        //Trade sizes used to open this trade
        public int open_index { get; set; }          //Position/time at which this trade was opened
        public int close_index { get; set; }         //Position/time at which this trade was closed
        public bool IsClosed { get; set; }           //Whether this trade has been closed
    }
}
