using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace BacktestCointegration
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Global.Instance.DoNothing();

            cboTimeFrame.SelectedIndex = 1;
            cboResponseV.SelectedIndex = 0;

            string[] pairs = Global.Instance.Symbols;

           // int a = 0 % 6;

            //double[] e = new double[] {1, -1,1,1,-1,-1,-1,1,1,1,1,1,1,1,-1,1,-1,-1,-1,-1,-1,-1,1,-1};
            //int wins, losses;
            //Global.Instance.MaximumConsecutiveWinsLosses(e, out wins, out losses);
            //MessageBox.Show("wins = " + wins.ToString() + " losses = " + losses.ToString());
            //foreach (string str in pairs)
            //{
            //    if (!str.Contains("USD"))
            //    {
            //        continue;
            //    }
            //    textBox1.Text += str + Environment.NewLine;
            //}
            //int count = 0;

            //using (StreamReader sr = new StreamReader(@"D:\Research\Trading\Programs\BacktestCointegration\BacktestCointegration\bin\Release\Output (second round) 2.csv"))
            //{
            //    string line = null;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        count++;
            //    }
            //}
            //MessageBox.Show(count.ToString());

            //MessageBox.Show(Global.Instance.PositionSize(198000).ToString());

        }

        private void cmdTest_Click(object sender, EventArgs e)
        {


        }



        private void cmdOptimize_Click(object sender, EventArgs e)
        {

        }

        private void txtLoad_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void plot(string pair, string frame)
        {

            double[] y = new double[Global.Instance.TotalBars[frame]];
            string line;
            using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + frame + "\\" + pair + ".csv"))
            {
                string[] ext;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    ext = line.Split(',');
                    y[i] = double.Parse(ext[2]);
                    i++;
                }
                reader.Close();
            }

            frmPlot plot = new frmPlot();
            plot.Show();
            plot.draw(y, pair + " - " + frame);
        }

        private void cmdMegaOptimize_Click(object sender, EventArgs e)
        {

        }

        private void SingleRunrunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int window, leverage, responseVariableIndex, maxOpenPositions;
            double bandwidth, transactionCost, initialDeposit;
            string[] basket;

            if (!int.TryParse(txtWindow.Text, out window))
            {
                MessageBox.Show("Invalid window");
                return;
            }
            if (!int.TryParse(txtLeverage.Text, out leverage))
            {
                MessageBox.Show("Invalid leverage");
                return;
            }
            if (!int.TryParse(txtMaxOpenPositions.Text, out maxOpenPositions))
            {
                MessageBox.Show("Invalid max open positions");
                return;
            }
            if (!double.TryParse(txtBandwidth.Text, out bandwidth))
            {
                MessageBox.Show("Invalid bandwidth");
                return;
            }
            if (!double.TryParse(txtTransactionCost.Text, out transactionCost))
            {
                MessageBox.Show("Invalid transaction cost");
                return;
            }
            if (!double.TryParse(txtInitialDeposit.Text, out initialDeposit))
            {
                MessageBox.Show("Invalid initial deposit");
                return;
            }
            basket = txtBasket.Text.Split(',');
            responseVariableIndex = cboResponseV.SelectedIndex;
            if (basket.Length <= 1)
            {
                MessageBox.Show("Basket must contain a minimum of two symbols separated by comma");
                return;
            }
            if (responseVariableIndex < 0 || responseVariableIndex >= basket.Length)
            {
                MessageBox.Show("Response variable index must be greater than 0 and less than the total number of symbols in the basket");
                return;
            }
            SingleRun test = new SingleRun(basket, cboTimeFrame.Text);
            test.Leverage = leverage;
            test.InitialDeposit = initialDeposit;
            test.Bandwidth = bandwidth;
            test.TransactionCost = transactionCost;
            test.Responsevariableindex = responseVariableIndex;
            test.Window = window;
            test.MaxOpenPositions = maxOpenPositions;
            StrategyTesterResult res = test.Run();

            frmResults frm = new frmResults(res);
            frm.Show();
        }

        private void OptimizationRunToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int period, leverage;
            double transactionCost, initialDeposit;
            string[] basket;

            if (!int.TryParse(txtWindow.Text, out period))
            {
                MessageBox.Show("Invalid period");
                return;
            }
            if (!int.TryParse(txtLeverage.Text, out leverage))
            {
                MessageBox.Show("Invalid leverage");
                return;
            }
            if (!double.TryParse(txtTransactionCost.Text, out transactionCost))
            {
                MessageBox.Show("Invalid transaction cost");
                return;
            }
            if (!double.TryParse(txtInitialDeposit.Text, out initialDeposit))
            {
                MessageBox.Show("Invalid initial deposit");
                return;
            }
            basket = txtBasket.Text.Split(',');
            if (basket.Length <= 1)
            {
                MessageBox.Show("Basket must contain a minimum of two symbols separated by comma");
                return;
            }


            Optimization test = new Optimization(basket, cboTimeFrame.Text);
            test.Leverage = leverage;
            test.InitialDeposit = initialDeposit;
            test.TransactionCost = transactionCost;

            StrategyTesterResult[] resultset = test.Run();


            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Strategy tester resultset files (*.str) | *.str";
            sf.FileName = txtBasket.Text;
            sf.InitialDirectory = Application.StartupPath;
            if (sf.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(sf.FileName, FileMode.Create))
                {
                    bformatter.Serialize(stream, resultset);
                    stream.Close();
                }

            }
        }

        private void OptimizationloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Strategy tester resultset files (*.str) | *.str";
            of.FileName = txtBasket.Text;
            of.InitialDirectory = Application.StartupPath;

            if (of.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StrategyTesterResult[] resultset;
            BinaryFormatter bformatter = new BinaryFormatter();
            using (FileStream stream = File.Open(of.FileName, FileMode.Open))
            {
                resultset = (StrategyTesterResult[])bformatter.Deserialize(stream);
                stream.Close();
            }

            frmLoad frmload = new frmLoad(resultset);
            frmload.Show();
        }

        private void individualChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (string str in Global.Instance.Symbols)
            {
                plot(str, cboTimeFrame.Text);
            }
        }

       

        private void SingleRunloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MegaOptimizationRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int leverage, maxOpenPositions;
            double transactionCost, initialDeposit;           

          
            if (!int.TryParse(txtLeverage.Text, out leverage))
            {
                MessageBox.Show("Invalid leverage");
                return;
            }
            if (!int.TryParse(txtMaxOpenPositions.Text, out maxOpenPositions))
            {
                MessageBox.Show("Invalid max open positions");
                return;
            }           
            if (!double.TryParse(txtTransactionCost.Text, out transactionCost))
            {
                MessageBox.Show("Invalid transaction cost");
                return;
            }
            if (!double.TryParse(txtInitialDeposit.Text, out initialDeposit))
            {
                MessageBox.Show("Invalid initial deposit");
                return;
            }

            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
                       

            List<string[]> ListSymbols = new List<string[]>();
            using (StreamReader reader = new StreamReader("../../ListSymbols.txt"))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    ListSymbols.Add(line.Split(','));
                }
                reader.Close();
            }

            List<int> ListWindows = new List<int>();
            using (StreamReader reader = new StreamReader("../../ListWindows.txt"))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    ListWindows.Add(int.Parse(line));
                }
                reader.Close();
            }


            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.SelectedPath = Application.StartupPath;
            fb.Description = "Please select folder to store the results";

            if (fb.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> kvp in Global.Instance.TotalBars)
            {
                sb.Append(kvp.Key + ",");
            }
            sb.Remove(sb.Length - 1, 1);

            InputBoxResult result = InputBox.Show("Enter time frames:", "Time frames", sb.ToString(), null);
            if (!result.OK)
            {
                return;
            }
            this.Text = result.Text;
            string[] timeframes = result.Text.Split(',');

          
            int t = 0;
            foreach (string timeframe in timeframes)
            {

                foreach (int window in ListWindows)                
                {
                        foreach (string[] basket in ListSymbols)
                        {
                            string basketname = string.Join("+", basket);
                            if (!Directory.Exists(fb.SelectedPath + "\\" + timeframe))
                            {
                                Directory.CreateDirectory(fb.SelectedPath + "\\" + timeframe);
                            }
                            if (!Directory.Exists(fb.SelectedPath + "\\" + timeframe + "\\" + window))
                            {
                                Directory.CreateDirectory(fb.SelectedPath + "\\" + timeframe + "\\" + window);
                            }
                            if (File.Exists(fb.SelectedPath + "\\" + timeframe + "\\" + window + "\\" + basketname + ".str"))
                            {
                                continue;
                            }                          
                            MegaOptimization test = new MegaOptimization(basket, timeframe);
                            test.MaxOpenPositions = maxOpenPositions;
                            test.Window = window;
                            test.InitialDeposit = initialDeposit;
                            test.Leverage = leverage;
                            test.TransactionCost = transactionCost;
                            StrategyTesterResult[,] resultset = test.Run();                  
                            BinaryFormatter bformatter = new BinaryFormatter();
                            using (FileStream stream = new FileStream(fb.SelectedPath + "\\" + timeframe + "\\" + window + "\\" + basketname + ".str", FileMode.Create))
                            {
                                bformatter.Serialize(stream, resultset);
                                stream.Close();
                            }

                        }
                }
                t++;
            }
            MessageBox.Show("Done :)");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("hi");
        }

        private void MegaOptimizationloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.SelectedPath = Application.StartupPath;
            fb.Description = "Please select result folder";

            if (fb.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DirectoryInfo rootdir = new DirectoryInfo(fb.SelectedPath);


            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "CSV files (*.csv) | *.csv";
            sf.InitialDirectory = Application.StartupPath;

            if (sf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            //List<string> InvalidBaskets = new List<string>();
            //using (StreamReader reader = new StreamReader("../../InvalidBaskets.txt"))
            //{
            //    string line = null;
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        InvalidBaskets.Add(line);
            //    }
            //    reader.Close();
            //}


            int count = 0;
            using (StreamWriter sw = new StreamWriter(sf.FileName))
            {                
                #region main loop
                foreach (DirectoryInfo dir2 in rootdir.GetDirectories())
                {
                    foreach (DirectoryInfo dir3 in dir2.GetDirectories())
                    {
                        foreach (FileInfo file in dir3.GetFiles())
                        {
                            #region file looop
                            StrategyTesterResult[,] resultset;
                            BinaryFormatter bformatter = new BinaryFormatter();
                            using (FileStream stream = File.Open(file.FullName, FileMode.Open))
                            {
                                resultset = (StrategyTesterResult[,])bformatter.Deserialize(stream);
                                stream.Close();
                            }
                            count += (resultset.GetLength(0) * resultset.GetLength(1));
                            for (int i = 0; i < resultset.GetLength(0); i++)
                            {
                                for (int j = 0; j < resultset.GetLength(1); j++)
                                {
                                    count++;
                                    StrategyTesterResult result = resultset[i, j];
                                    if (result.final_balance < 2000)
                                    {
                                        continue;
                                    }

                                    StringBuilder sb = new StringBuilder();
                                    string basket = string.Join(",", result.Basket);                                 
                                    sb.Append("\"" + basket + "\"" ); sb.Append(",");
                                    sb.Append(i.ToString()); sb.Append(",");
                                    sb.Append(result.window.ToString()); sb.Append(",");
                                    sb.Append(result.bandwidth.ToString()); sb.Append(",");
                                    sb.Append(dir2.Name); sb.Append(",");
                                    sb.Append(result.final_balance.ToString()); sb.Append(",");
                                    sb.Append(result.total_trades.ToString()); sb.Append(",");  
                                    sb.Append(result.accuracy.ToString()); sb.Append(",");
                                    sb.Append(result.estimate_monthly_profit.ToString()); sb.Append(",");
                                    sb.Append(result.pips_net.ToString()); sb.Append(",");
                                    sb.Append(result.pips_won_max.ToString()); sb.Append(",");
                                    sb.Append(result.pips_won_min.ToString()); sb.Append(",");
                                    sb.Append(result.pips_won_avg.ToString()); sb.Append(",");
                                    sb.Append(result.pips_loss_max.ToString()); sb.Append(",");
                                    sb.Append(result.pips_loss_min.ToString()); sb.Append(",");
                                    sb.Append(result.pips_loss_avg.ToString()); sb.Append(",");                               
                                    sb.Append(result.maximum_consecutive_wins.ToString()); sb.Append(",");
                                    sb.Append(result.maximum_consecutive_losses.ToString()); sb.Append(",");                                
                                    sw.WriteLine(sb.ToString());
                                }
                            }
                            sw.Flush();
                            #endregion                           
                        }
                    }
                }
                #endregion
                sw.Close();
            }
            MessageBox.Show("Done " + count.ToString());
        }

        private void cmdBackTest_Click(object sender, EventArgs e)
        {
            Dictionary<DateTime, int> M5Index = new Dictionary<DateTime, int>();

            Dictionary<string, DateTime[]> reff = new Dictionary<string, DateTime[]>();
            foreach (KeyValuePair<string, int> kvp in Global.Instance.TotalBars)
            {
                reff.Add(kvp.Key, new DateTime[kvp.Value]);
            }


            double transactionCost = 0;
            double leverage;
            double initialDeposit = 0;

            StringBuilder title = new StringBuilder();

            if (!double.TryParse(txtTransactionCost.Text, out transactionCost))
            {
                MessageBox.Show("Invalid transaction cost");
                return;
            }
            if (!double.TryParse(txtLeverage.Text, out leverage))
            {
                MessageBox.Show("Invalid leverage");
                return;
            }
            if (!double.TryParse(txtInitialDeposit.Text, out initialDeposit))
            {
                MessageBox.Show("Invalid initial deposit");
                return;
            }

            using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\m5\\EURUSD.csv"))
            {
                string line = null;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] ext = line.Split(',');
                    M5Index.Add(DateTime.Parse(ext[0]), i);
                    i++;
                }
                reader.Close();

            }

            M5Index.Add(new DateTime(2011, 12, 25, 22, 00, 00), 42311);
            M5Index.Add(new DateTime(2012, 1, 2, 2, 00, 00), 43733);

            M5Index.Add(new DateTime(2011, 2, 6, 17, 00, 00), 51265);
            M5Index.Add(new DateTime(2012, 1, 1, 22, 00, 00), 43732);

            foreach (KeyValuePair<string, int> kvp in Global.Instance.TotalBars)
            {
                if (kvp.Key == "m5")
                {
                    continue;
                }
                using (StreamReader reader = new StreamReader(Global.Instance.data_folder_path + "\\" + kvp.Key + "\\EURUSD.csv"))
                {
                    string line = null;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] ext = line.Split(',');
                        reff[kvp.Key][i] = DateTime.Parse(ext[0]);
                        i++;
                    }
                    reader.Close();
                }
            };

            string[] systems = txtSystems.Text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StrategyTesterResult[] results = new StrategyTesterResult[systems.Length];
            StrategyTesterResult res = new StrategyTesterResult();
            List<int> Actions = new List<int>();

            
            for (int i = 0; i < systems.Length; i++)
            {
                #region main loop
                string[] ext;
                if (systems[i].Contains(','))
                {
                    ext = systems[i].Split(',');
                }
                else
                {
                    ext = systems[i].Split('\t');
                }
                string Symbol1 = ext[0];
                string Symbol2 = ext[1];
                int period = int.Parse(ext[2]);
                double bandwidth = double.Parse(ext[3]);
                string timeframe = ext[4];

                SingleRun singlerun = new SingleRun(new string[1], timeframe);
                singlerun.TransactionCost = transactionCost;
                singlerun.Bandwidth = bandwidth;
                results[i] = singlerun.Run();

             
                res.total_trades += results[i].total_trades;
                res.total_short_trades += results[i].total_short_trades;
                res.total_long_trades += results[i].total_long_trades;
                res.total_short_trades_loss += results[i].total_short_trades_loss;
                res.total_long_trades_loss += results[i].total_long_trades_loss;
                res.total_short_trades_profitable += results[i].total_short_trades_profitable;
                res.total_long_trades_profitable += results[i].total_long_trades_profitable;
                res.total_profit_trades += results[i].total_profit_trades;
                res.total_loss_trades += results[i].total_loss_trades;
              

              
                title.Append(Symbol1 + "-" + Symbol2);
                if (i < systems.Length - 1)
                {
                    title.Append("  &  ");
                }

                #endregion


            }

            Actions.Sort();
            List<int> OpenOrders = new List<int>();
            List<int> CloseOrders = new List<int>();
            List<double> Profit1 = new List<double>();
            List<double> Profit2 = new List<double>();
            List<double> Pipcost1 = new List<double>();
            List<double> Pipcost2 = new List<double>();
            List<int> PositionSize = new List<int>();

            int[] pos = new int[results.Length];
            bool[] state = new bool[results.Length];
           
            int max_open_positions = 0;
            int total_number_of_open_positions = 0;


            for (int i = 0; i < Actions.Count; i++)
            {
                for (int j = 0; j < results.Length; j++)
                {
                    
                }
                int n_open_positions = open_positions(state);
                total_number_of_open_positions += n_open_positions;
                if (n_open_positions > max_open_positions)
                {
                    max_open_positions = n_open_positions;
                }
            }





            res.leverage = leverage;
          
            res.title = title.ToString();          
            res.initialDeposit = initialDeposit;
       
            res.transactionCost = transactionCost;
            
            try
            {
                res.percentage_short_trades = Math.Round((double)(res.total_short_trades * 100 / res.total_trades), 1);
            }
            catch { }
            try
            {
                res.percentage_long_trades = Math.Round((double)(res.total_long_trades * 100 / res.total_trades), 1);
            }
            catch { }
            try
            {
                res.percentage_short_trades_profitable = Math.Round((double)(res.total_short_trades_profitable * 100 / res.total_short_trades), 1);
            }
            catch { }
            try
            {
                res.percentage_short_trades_loss = Math.Round((double)(res.total_short_trades_loss * 100 / res.total_short_trades), 1);
            }
            catch { }
            try
            {
                res.percentage_long_trades_profitable = Math.Round((double)(res.total_long_trades_profitable * 100 / res.total_long_trades), 1);
            }
            catch { }
            try
            {
                res.percentage_long_trades_loss = Math.Round((double)(res.total_long_trades_loss * 100 / res.total_long_trades), 1);
            }
            catch { }
            
            
           
           
           

            if (res.total_trades > 0)
            {
                res.accuracy = Math.Round((double)(res.total_profit_trades * 100) / res.total_trades, 1);
            }
          
            res.maximum_open_positions = max_open_positions;           
            frmResults frm = new frmResults(res);
            frm.Show();

        }

        private int open_positions(bool[] state)
        {
            int total = 0;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i])
                {
                    total++;
                }
            }
            return total;
        }

        private void setDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBoxResult result = InputBox.Show("Enter path:", "Data folder path", "", null);
            if (result.OK)
            {
                Global.Instance.data_folder_path = result.Text;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] basket = txtBasket.Text.Split(',');

            string common = Global.Instance.CommonCurrency(basket);
            bool[] b = Global.Instance.InversionCurrencyPair(basket, common);
        }

        private void source1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            source1ToolStripMenuItem.Checked = true;
            source2ToolStripMenuItem.Checked = false;

            Global.Instance.data_folder_path = @"D:\Research\Trading\Data\FXCM\";
            Global.Instance.TotalBars = new Dictionary<string, int>();
            Global.Instance.TotalBars.Add("m5", 75106);
            Global.Instance.TotalBars.Add("m15", 25056);
            Global.Instance.TotalBars.Add("m30", 12543);
            Global.Instance.TotalBars.Add("H1", 6288);
            Global.Instance.TotalBars.Add("H2", 3148);
            Global.Instance.TotalBars.Add("H3", 2101);
            Global.Instance.TotalBars.Add("H4", 1577);
            Global.Instance.TotalBars.Add("H6", 1054);
            Global.Instance.TotalBars.Add("H8", 792);
            Global.Instance.TotalBars.Add("D1", 267);
        }

        private void source2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FXCM 2   (2010 to 2011)
            source1ToolStripMenuItem.Checked = false;
            source2ToolStripMenuItem.Checked = true;

            Global.Instance.data_folder_path = @"D:\Research\Trading\Data\FXCM 2\";
            Global.Instance.TotalBars = new Dictionary<string, int>();
            Global.Instance.TotalBars.Add("m5", 74501);
            Global.Instance.TotalBars.Add("m15", 24865);
            Global.Instance.TotalBars.Add("m30", 12458);
            Global.Instance.TotalBars.Add("H1", 6254);
            Global.Instance.TotalBars.Add("H2", 3128);
            Global.Instance.TotalBars.Add("H3", 2086);
            Global.Instance.TotalBars.Add("H4", 1564);
            Global.Instance.TotalBars.Add("H6", 1044);
            Global.Instance.TotalBars.Add("H8", 783);
            Global.Instance.TotalBars.Add("D1", 261);
        }

       

      

       
    }
}
