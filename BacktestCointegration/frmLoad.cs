using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BacktestCointegration
{
    public partial class frmLoad : Form
    {
        private List<StrategyTesterResult> resultset;
        

        private Button[] button = new Button[10];

        public frmLoad(StrategyTesterResult[,] rs)
        {
            InitializeComponent();
            resultset = new List<StrategyTesterResult>();

            for (int i = 0; i < rs.GetLength(0); i++)
            {
                for (int j = 0; j < rs.GetLength(1); j++)
                {
                    resultset.Add(rs[i, j]);
                }

            }
            
            for (int i = 0; i < 10; i++)
            {
                button[i] = new Button();
                this.Controls.Add(button[i]);
                button[i].Location = new System.Drawing.Point(135, 73 + i * 35);
                button[i].Name = "button" + i.ToString();
                button[i].Size = new System.Drawing.Size(388, 31);
                button[i].TabIndex = i;
                button[i].Text = "Period=40, Bandwidth=2.5, Net=-2000";
                button[i].UseVisualStyleBackColor = true;
                button[i].Visible = true;
                button[i].Click += new System.EventHandler(this.button_Click);
            }
            
            //update(0);                       
        }
        
        public frmLoad(StrategyTesterResult[] rs)
        {
            InitializeComponent();
            resultset = new List<StrategyTesterResult>();

            for (int i = 0; i < rs.Length; i++)
            {
               resultset.Add(rs[i]);             
            }

            for (int i = 0; i < 10; i++)
            {
                button[i] = new Button();
                this.Controls.Add(button[i]);
                button[i].Location = new System.Drawing.Point(135, 73 + i * 35);
                button[i].Name = "button" + i.ToString();
                button[i].Size = new System.Drawing.Size(388, 31);
                button[i].TabIndex = i;
                button[i].Text = "Period=40, Bandwidth=2.5, Net=-2000";
                button[i].UseVisualStyleBackColor = true;
                button[i].Visible = true;
                button[i].Click += new System.EventHandler(this.button_Click);
            }

            //update(0);
        }

      

        private void button_Click(object sender, EventArgs e)
        {
           // int index = ((Button)sender).TabIndex;
            //frmResults frmresult = new frmResults(res[index]);
            //frmresult.Show();
        }

        private void txtUpdate_Click(object sender, EventArgs e)
        {
            //update(double.Parse(txtCost.Text));
        }
    }
}
