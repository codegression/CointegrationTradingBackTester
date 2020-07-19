using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BacktestCointegration
{
    public class LinearRegression
    {
        public static double[] Build(double[,] data, int response_variable_index=-1)
        {
            int n = data.GetLength(0);
            int m = data.GetLength(1);
            double[] w = null;
            double[] weight = new double[m + 1];
            int res = 0;
            int nvars = 0;
            if (response_variable_index == -1)
            {
                response_variable_index = m - 1;
            }
            if (response_variable_index < m - 1)
            {
                //Swap columns   (m - 1) and 
                for (int i = 0; i < n; i++)
                {
                    double temp = data[i, m - 1];
                    data[i, m - 1] = data[i, response_variable_index];
                    data[i, response_variable_index] = temp;
                }
            }
            alglib.linearmodel lm = null;
            alglib.lrreport ar = null;
            alglib.lrbuild(data, n, m - 1, out res, out lm, out ar);
            alglib.lrunpack(lm, out w, out nvars);                                             
            for (int j = 0; j < m-1; j++)
            {               
                weight[j] = w[j];                              
            }
            //Swap back
            if (response_variable_index < m - 1)
            {
                //Swap columns   (m - 1) and 
                for (int i = 0; i < n; i++)
                {
                    double temp = data[i, m - 1];
                    data[i, m - 1] = data[i, response_variable_index];
                    data[i, response_variable_index] = temp;
                }
            }
            weight[m - 1] = weight[response_variable_index];
            weight[response_variable_index] = -1;
            weight[m] = w[w.Length - 1]; //Intercept        
            return weight;
        }

        public static double Predict(double[] data, double[] coefficients)
        {
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i] * coefficients[i];
            }
            sum += coefficients[coefficients.Length - 1];
            return sum;

        }
        public static double Predict(double[,] data, int data_index, double[] coefficients)
        {
            double sum = 0;
            for (int i = 0; i < coefficients.Length - 1; i++)
            {
                sum += data[i, data_index] * coefficients[i];
            }
            sum += coefficients[coefficients.Length - 1];
            return sum;
        }
        public static double Predict2(double[,] data, int data_index, double[] coefficients)
        {
            double sum = 0;
            for (int i = 0; i < coefficients.Length - 1; i++)
            {
                sum += data[data_index, i] * coefficients[i];
            }
            sum += coefficients[coefficients.Length - 1];
            return sum;
        }



        public static double[] StandardizeWeights(double[] w, double[] x, bool[] inverse)
        {
            double[] Wn = new double[x.Length];           
            for (int i = 0; i < x.Length; i++)
            {
                Wn[i] = w[i];
                if (inverse[i])
                {
                    Wn[i] = -1 * x[i] * Wn[i];
                }             
            }           
            return Wn;
        }
        public static double[] NormalizeWeights(double[] w, double[] x, bool[] inverse)
        {
            double[] Wn = new double[x.Length];
            double min = double.PositiveInfinity;
            for (int i = 0; i < x.Length; i++)
            {
                Wn[i] = w[i];
                if (inverse[i])
                {
                    Wn[i] = -1 * Wn[i] * x[i];
                }
                double m = Math.Abs(Wn[i]);
                if (m < min && m != 0)
                {
                    min = m;
                }
            }
            for (int i = 0; i < x.Length; i++)
            {
                Wn[i] = Math.Round(Wn[i] / min, 2);
            }
            return Wn;
        }
        public static double TotalNormalizedWeights(double[] w, double[] x, bool[] inverse)
        {
            double[] Wn = new double[x.Length];
            double min = double.PositiveInfinity;
            for (int i = 0; i < x.Length; i++)
            {
                Wn[i] = w[i];
                if (inverse[i])
                {
                    Wn[i] = -1 * Wn[i] * x[i];
                }
                double m = Math.Abs(Wn[i]);
                if (m < min && m != 0)
                {
                    min = m;
                }
            }
            for (int i = 0; i < x.Length; i++)
            {
                Wn[i] = Math.Round(Wn[i] / min, 2);
            }
            double total = 0;
            for (int i = 0; i < x.Length; i++)
            {
                total += Math.Round(Math.Abs(Wn[i]), 2);
            }
            return total;
        }


        public static int[] getTradeSizes(double[] Coefficients)
        {
           // try
            {
                double equity = 10000;
                double usablemargin = 10000;
                double usablemarginpert = (equity > 0) ? (usablemargin * 100 / equity) : 0;
                double allowance = Math.Floor((equity * 10) / 1000);
                int[] tradesizes = new int[Coefficients.Length];
                double total = 0;

                for (int i = 0; i < Coefficients.Length; i++)
                {
                    total += Math.Abs(Coefficients[i]);
                }
                if (usablemarginpert < 50)
                {
                    return null;
                }
                if (total > allowance || total > 50000)
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
            {
                //return null;
            }
        }

    }
}
