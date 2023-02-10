using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace MNK
{
    public partial class Form1 : Form
    {
        


        
        public Form1()
        {
            InitializeComponent();
            //double[] x = { 2, 4, 5, 6, 7, 8 };
            //double[] y = { 2.4, 2.9, 3, 3.5, 3.6, 3.7 };

            
        }

        private double f1(double x, double a, double b)
        {
            return x * a + b;
        }

        private double f2(double x, double a, double b)
        {
            return b * Math.Pow(x, a);
        }

        private double f3(double x, double a, double b)
        {
            return b * Math.Exp(a * x);
        }

        private double f4(double x, double a, double b, double c)
        {
            return a * Math.Pow(x, 2) + b * x + c;
        }

        public void PrintGraf(double[] x, double[] y, int end, double h)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();

            LSM first = new LSM(x, y);
            double a, b;
            (a, b) = first.Linear();

            for(double i=0; i<end; i+=h)
            {
                list.Add(i, f1(i, a, b));
            }

            LineItem curve = pane.AddCurve("Leaner", list, Color.Blue, SymbolType.None);

            (a, b) = first.Exp();

            list.Clear();

            for (double i = 0; i < end; i += h)
            {
                list.Add(i, f2(i, a, b));
            }

            pane.AddCurve("Exp", list, Color.Red, SymbolType.None);

            list.Clear();

            (a, b) = first.Power();

            for(double i = 0; i< end; i+=h)
            {
                list.Add(i, f3(i, a, b));
            }

            pane.AddCurve("Power", list, Color.Yellow, SymbolType.None);

            list.Clear();

            double[] coef = first.Squere();

            for(double i=0;i<end; i+=h)
            {
                list.Add(i, f4(i, coef[0], coef[1], coef[2]));
            }

            pane.AddCurve("Square", list, Color.Brown, SymbolType.None);


            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> x = new List<double>(from p in textBox3.Text.Split() select Convert.ToDouble(p));
            List<double> y = new List<double>(from p in textBox4.Text.Split() select Convert.ToDouble(p));
            double step = Convert.ToDouble(textBox1.Text);
            double end = Convert.ToDouble(textBox2.Text);
            


        }
    }
}
