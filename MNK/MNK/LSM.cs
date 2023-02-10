using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using ZedGraph;

namespace MNK
{
    public class LSM
    {
        
        public double[] x;
        public double[] y;
        public double step;
        public double end;
        public double[] coefL, coefP, coefE, coefQ;

        public LSM(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new ArgumentException("X and Y arrays should be equal!");
            this.y = y;
            this.x = x;
        }

        public void Linear()
        {
            const int n = 2;



           // Matrix matr = new Matrix(x, y);
            double[,] basic = CreateBasic(x, y, n);

            //for (int i = 0; i < n; i++)
            //{
            //    basic[0, 0] += Math.Pow(x[i], 2);
            //    basic[0, 1] += x[i];
            //    basic[0, 2] += x[i] * y[i];
            //    basic[1, 0] += x[i];
            //    basic[1, 2] += y[i];
            //}
            //basic[1, 1] = x.Length;

            double determ, determ1, determ2;
            (determ, determ1, determ2) = Determ(basic);

            double a = determ1 / determ;
            double b = determ2 / determ;

            return (a, b);
        }

        private (double, double, double) Determ(double[,] basic)
        {
            double determ1 = basic[0, 1] * basic[1, 2] - basic[0, 2] * basic[1, 1];
            double determ2 = basic[0, 0] * basic[1, 2] - basic[0, 2] * basic[1, 0];
            double determ = basic[0, 0] * basic[1, 1] - basic[1, 0] * basic[0, 1];

            return (determ, determ1, determ2);
        }
        
        private double[,] CreateBasic(double[] x, double[] y, int n)
        {
            double[,] basic = new double[n, n+1];

            for (int i = 0; i < n; i++)
            {
                basic[0, 0] += Math.Pow(x[i], 2);
                basic[0, 1] += x[i];
                basic[0, 2] += x[i] * y[i];
                basic[1, 0] += x[i];
                basic[1, 2] += y[i];
            }
            basic[1, 1] = x.Length;

            return basic;
        }

        public (double, double) Linear()
        {
            int n = 2; 

            double[,] basic = CreateBasic(x, y, n);

            //for (int i = 0; i < n; i++)
            //{
            //    basic[0, 0] += Math.Pow(x[i], 2);
            //    basic[0, 1] += x[i];
            //    basic[0, 2] += x[i] * y[i];
            //    basic[1, 0] += x[i];
            //    basic[1, 2] += y[i];
            //}
            //basic[1, 1] = x.Length;

            double determ, determ1, determ2;
            (determ, determ1, determ2) = Determ(basic);

            double a = determ1 / determ;
            double b = determ2 / determ;

            return (a, b);
        }

        public (double, double) Power()
        {
            int n = 2;

            double[] xLn = new double[x.Length];
            double[] yLn = new double[y.Length];
            double[,] basic;

            for (int i = 0; i < x.Length; i++)
            {
                xLn[i] = Math.Log(x[i]);
                yLn[i] = Math.Log(y[i]);
            }
            
            basic = CreateBasic(xLn, yLn, n);
            
            double determ, determ1, determ2;
            (determ, determ1, determ2) = Determ(basic);

            double a = determ1 / determ;
            double b = Math.Exp(determ2 / determ);

            return (a, b);
        }

        public (double, double) Exp()
        {
            int n = 2;
            double[] yLn = new double[y.Length];
            double[,] basic;
            double determ, determ1, determ2;
            double a, b;

            for (int i = 0; i < y.Length; i++)
            {
                yLn[i] = Math.Log(y[i]);
            }

            basic = CreateBasic(x, yLn, n);

            (determ, determ1, determ2) = Determ(basic);

            a = determ1 / determ;
            b = Math.Exp(determ2 / determ);

            return (a, b);
        }

        public double[] Squere()
        {
            int n = 3;
            double[] coef = new double[3];
            double[,] basic = new double[n+1, n+1];
            double [] determ = new double[n+1];

            for (int i = 0; i < n-1; i++)
            {
                basic[0, 0] += Math.Pow(x[i], 4);
                basic[0, 1] += Math.Pow(x[i], 3);
                basic[0, 2] += Math.Pow(x[i], 2);
                basic[0, 3] += Math.Pow(x[i], 2) * y[i];
                basic[1, 0] += Math.Pow(x[i], 3);
                basic[1, 1] += Math.Pow(x[i], 2);
                basic[1, 2] += x[i];
                basic[1, 3] += x[i] * y[i];
                basic[2, 0] += Math.Pow(x[i], 2);
                basic[2, 1] += x[i];
                basic[2, 3] += y[i];
            }
                
            basic[2, 2] = x.Length;


            

            Matrix matrix = new Matrix(basic);

            for(int i = 1; i < n+1; i++)
            {
                determ[i] = Matrix.Determ(matrix.GetMinor(0, i));
            }
            
            determ[0] = Matrix.Determ(matrix);
            
            for(int i=0;i<n;i++)
            {
                coef[i] = determ[i++] / determ[0];
            }
            return coef;
        }
    }
    
}
