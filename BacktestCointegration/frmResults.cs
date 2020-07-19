using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace BacktestCointegration
{
    public partial class frmResults : Form
    {
       
        public frmResults(StrategyTesterResult result)
        {
            InitializeComponent();
          

            string body;
            using (StreamReader sr = new StreamReader(@"..\..\StrategyTester.htm"))
            {
                body = sr.ReadToEnd();
            }
          


            // set currency format
            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            System.Globalization.NumberFormatInfo currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;
            currencyFormat.CurrencyNegativePattern = 1;




            body = body.Replace("{1}", result.initialDeposit.ToString("C", currencyFormat));
            body = body.Replace("{11}", Math.Round(result.final_balance, 1).ToString("C", currencyFormat));
            body = body.Replace("{12}", result.expected_profit.ToString());
            body = body.Replace("{14}", result.total_trades.ToString());
            body = body.Replace("{15}", result.pips_net.ToString());
            body = body.Replace("{16}", result.estimate_monthly_profit.ToString());
            body = body.Replace("{17}", result.maximum_open_positions.ToString());
            body = body.Replace("{17}", result.maximum_drawdown.ToString());
            body = body.Replace("{20}", result.accuracy.ToString());
            body = body.Replace("{21}", result.total_short_trades.ToString());
            body = body.Replace("{22}", result.total_long_trades.ToString());
            body = body.Replace("{23}", result.total_profit_trades.ToString());
            body = body.Replace("{24}", result.total_loss_trades.ToString());
            body = body.Replace("{25}", result.total_short_trades_profitable.ToString());
            body = body.Replace("{26}", result.total_short_trades_loss.ToString());
            body = body.Replace("{27}", result.total_long_trades_profitable.ToString());
            body = body.Replace("{28}", result.total_long_trades_loss.ToString());
            body = body.Replace("{29}", result.maximum_consecutive_wins.ToString());
            body = body.Replace("{30}", result.maximum_consecutive_losses.ToString());
            body = body.Replace("{31}", result.maximum_consecutive_losses_factor.ToString());
            body = body.Replace("{33}", result.percentage_short_trades.ToString());
            body = body.Replace("{34}", result.percentage_long_trades.ToString());
            body = body.Replace("{35}", result.percentage_profit_trades.ToString());
            body = body.Replace("{36}", result.percentage_loss_trades.ToString());
            body = body.Replace("{37}", result.percentage_short_trades_profitable.ToString());
            body = body.Replace("{38}", result.percentage_short_trades_loss.ToString());
            body = body.Replace("{39}", result.percentage_long_trades_profitable.ToString());
            body = body.Replace("{40}", result.percentage_long_trades_loss.ToString());

            body = body.Replace("{50}", result.tradesize_max.ToString());
            body = body.Replace("{51}", result.tradesize_min.ToString());
            body = body.Replace("{52}", result.tradesize_mean.ToString());
            body = body.Replace("{53}", result.tradesize_median.ToString());
            body = body.Replace("{54}", result.tradesize_sd.ToString());

            body = body.Replace("{55}", result.tradeduration_max.ToString());
            body = body.Replace("{56}", result.tradeduration_min.ToString());
            body = body.Replace("{57}", result.tradeduration_mean.ToString());
            body = body.Replace("{58}", result.tradeduration_median.ToString());
            body = body.Replace("{59}", result.tradeduration_sd.ToString());
            body = body.Replace("{60}", result.total_SL_mismatch.ToString());
            body = body.Replace("{61}", result.total_TP_mismatch.ToString());
            body = body.Replace("{2}", result.total_reject_trades.ToString());

            body = body.Replace("{70}", result.pips_won.ToString());
            body = body.Replace("{71}", result.pips_won_min.ToString());
            body = body.Replace("{72}", result.pips_won_max.ToString());
            body = body.Replace("{73}", result.pips_won_avg.ToString());
            body = body.Replace("{74}", result.pips_loss.ToString());
            body = body.Replace("{75}", result.pips_loss_min.ToString());
            body = body.Replace("{76}", result.pips_loss_max.ToString());
            body = body.Replace("{77}", result.pips_loss_avg.ToString());


            if (string.IsNullOrEmpty(result.title))
            {
                body = body.Replace("{41}", string.Join(",", result.Basket));
            }
            else
            {
                body = body.Replace("{41}", result.title);
            }
            body = body.Replace("{42}", result.window.ToString());
            body = body.Replace("{43}", DateTime.Now.ToString());
            body = body.Replace("{44}", string.Format("window{0}, bandwidth={1}, transactioncost={2}, leverage={3}", result.window, result.bandwidth, result.transactionCost, result.leverage));
          
            webBrowser1.DocumentText = body;
            

            frmPlot plot = new frmPlot();
            plot.Show();
            plot.draw(result.balance_history.ToArray(), "Equity curve", "Time", "Equity");

        }

      


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savef = new SaveFileDialog();
            savef.Filter = "HTML file (*.html) | *.html";
            savef.FileName = "hello";

            if (savef.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(savef.FileName))
                {
                    sw.Write(webBrowser1.DocumentText);
                    sw.Close();
                }
                
            }
        }

      
    }
}
