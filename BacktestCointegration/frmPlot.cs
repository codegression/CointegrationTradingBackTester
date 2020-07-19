using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace BacktestCointegration
{
    public partial class frmPlot : Form
    {
        private ZedGraphControl zGraph1 = new ZedGraphControl();
        private string[] datastring;
        public frmPlot()
        {
            InitializeComponent();
            this.Controls.Add(zGraph1);
            zGraph1.Dock = DockStyle.Fill;
            zGraph1.IsShowHScrollBar = true;
            zGraph1.IsAutoScrollRange = true;
            zGraph1.IsShowVScrollBar = true;
            zGraph1.IsShowPointValues = true;

            zGraph1.PointValueEvent += new ZedGraphControl.PointValueHandler(zGraph1_PointValueEvent);

        }

        string zGraph1_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            if (datastring == null)
            {
                return curve.Points[iPt].ToString();
            }
            else
            {
                return datastring[iPt].ToString();
            }
        }




        public void draw(double[] y, string title="", string x_title="", string y_title="", int vertical_line=-1, string[] datanotes=null)
        {
            GraphPane myPane = zGraph1.GraphPane;
           
            this.Text = title;
            myPane.Title.Text = title;
            myPane.XAxis.Title.Text = x_title;
            myPane.YAxis.Title.Text = y_title;
            datastring = datanotes;

            ColorSymbolRotator rotator = new ColorSymbolRotator();

           

            double[] x = new double[y.Length];

            for (int j = 0; j < y.Length; j++)
            {
                x[j] = j;
            }
            
            LineItem curve1 = myPane.AddCurve("", x, y, rotator.NextColor, SymbolType.None);           
            curve1.Symbol.Fill = new Fill(Color.White);
            curve1.Line.Width = 2;
            myPane.YAxis.Scale.Min = y.Min();
            myPane.YAxis.Scale.Max = y.Max();
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = y.Length - 1;

            if (vertical_line >= 0)
            {
                LineObj line = new LineObj(Color.Brown, vertical_line, 0, vertical_line, myPane.YAxis.Scale.Max);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dot;
                myPane.GraphObjList.Add(line);
                
            }
           
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);
            zGraph1.AxisChange();

        }


        public void draw(double[,] data, string[] titles)
        {
            MasterPane myMaster = zGraph1.MasterPane;

            myMaster.PaneList.Clear();

            // Set the masterpane title
            myMaster.Title.Text = "Cointegration coefficients";
            myMaster.Title.IsVisible = true;

            // Fill the masterpane background with a color gradient
            myMaster.Fill = new Fill(Color.White, Color.MediumSlateBlue, 45.0F);

            // Set the margins to 10 points
            myMaster.Margin.All = 10;

            // Enable the masterpane legend
            myMaster.Legend.IsVisible = true;
            myMaster.Legend.Position = LegendPos.TopCenter;



            // Initialize a color and symbol type rotator
            ColorSymbolRotator rotator = new ColorSymbolRotator();

            // Create some new GraphPanes
            int n = data.GetLength(0);
            int m = data.GetLength(1);
            for (int i = 0; i < m; i++)
            {
                double[] x = new double[n];
                double[] y = new double[n];
                for (int j = 0; j < n; j++)
                {
                    x[j] = j;
                    y[j] = data[j, i] ;
                }

                // Create a new graph - rect dimensions do not matter here, since it
                // will be resized by MasterPane.AutoPaneLayout()
                GraphPane myPane = new GraphPane(new Rectangle(10, 10, 10, 10),
                    titles[i],
                   "",
                   "Value");

                // Fill the GraphPane background with a color gradient
                myPane.Fill = new Fill(Color.White, Color.LightYellow, 45.0F);
                myPane.BaseDimension = 6.0F;



                // Add a curve to the Graph, use the next sequential color and symbol
                LineItem myCurve = myPane.AddCurve(titles[i], x, y, rotator.NextColor, SymbolType.None);
                // Fill the symbols with white to make them opaque
                myCurve.Symbol.Fill = new Fill(Color.White);
                myCurve.Line.Width = 4;
                // Add the GraphPane to the MasterPane
                myMaster.Add(myPane);
                zGraph1.Height += 100;
            }



            using (Graphics g = this.CreateGraphics())
            {
                // Tell ZedGraph to auto layout the new GraphPanes
                myMaster.SetLayout(g, PaneLayout.SingleColumn);
            }

            zGraph1.AxisChange();

        }






        public void draw(double[] LB, double[] Mean, double[] UB)
        {
            GraphPane myPane = zGraph1.GraphPane;
       

            myPane.Title.Text = "";
            myPane.XAxis.Title.Text = "";
            myPane.YAxis.Title.Text = "Price ratio";

            ColorSymbolRotator rotator = new ColorSymbolRotator();

            //double[] y = new double[100];
            //double[] lb = new double[100];
            //double[] m = new double[100];
            //double[] ub = new double[100];

            //for (int j = 0; j < 100; j++)
            //{
            //    y[j] = j;
            //    lb[j] = LB[j + 100];
            //    m[j] = Mean[j + 100];
            //    ub[j] = UB[j + 100];
            //}




            //LineItem curve1 = myPane.AddCurve("LB", y, lb, rotator.NextColor, SymbolType.None);
            //curve1.Symbol.Fill = new Fill(Color.White);
            //curve1.Line.Width = 1;
            
            //LineItem curve2 = myPane.AddCurve("Mean", y, m, rotator.NextColor, SymbolType.None);
            //curve2.Symbol.Fill = new Fill(Color.White);
            //curve2.Line.Width = 1;
            
            //LineItem curve3 = myPane.AddCurve("UB", y, ub, rotator.NextColor, SymbolType.None);
            //curve3.Symbol.Fill = new Fill(Color.White);
            //curve3.Line.Width = 1;

            double[] y = new double[LB.Length];
         
            for (int j = 0; j < LB.Length; j++)
            {
                y[j] = j;
                
            }




            LineItem curve1 = myPane.AddCurve("LB", y, LB, rotator.NextColor, SymbolType.None);
            curve1.Symbol.Fill = new Fill(Color.White);
            curve1.Line.Width = 1;

            LineItem curve2 = myPane.AddCurve("Mean", y, Mean, rotator.NextColor, SymbolType.None);
            curve2.Symbol.Fill = new Fill(Color.White);
            curve2.Line.Width = 1;

            LineItem curve3 = myPane.AddCurve("UB", y, UB, rotator.NextColor, SymbolType.None);
            curve3.Symbol.Fill = new Fill(Color.White);
            curve3.Line.Width = 1;

            // Fill the axis background with a gradient
            //myPane.XAxis.Scale.Min = 0;
            //myPane.XAxis.Scale.Max = 99;

            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);
            zGraph1.AxisChange();

        }
    }
}
