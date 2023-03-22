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

namespace RungeKut
{
    using func2 = Func<double, double, double>;
    using func1 = Func<double, double>;

    public partial class Form1 : Form
    {

        static double eps = 0.001;

        (func2 x, func2 y, func1 x_s, func1 y_s, double x0, double y0) get_test1()
        {
            double f_x(double x, double y)
            {
                return -2 * x + 4 * y;
            }

            double f_y(double x, double y)
            {
                return -x + 3 * y;
            }

            double f_x_s(double t)
            {
                return 4 * Math.Exp(-t) - Math.Exp(2 * t);
            }

            double f_y_s(double t)
            {
                return Math.Exp(-t) - Math.Exp(2 * t);
            }

            return (f_x, f_y, f_x_s, f_y_s, 3, 0);
        }

        (func2 x, func2 y, func1 x_s, func1 y_s, double x0, double y0) get_test2()
        {
            double f_x(double x, double y)
            {
                return y;
            }

            double f_y(double x, double y)
            {
                return 2 * y;
            }

            double f_x_s(double t)
            {
                return Math.Exp(2 * t) + 1;
            }

            double f_y_s(double t)
            {
                return 2 * Math.Exp(2 * t);
            }

            return (f_x, f_y, f_x_s, f_y_s, 2, 2);
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        public void SolveAndShow(int n, bool flag)
        {
            func2 f_x, f_y;
            func1 x_s, y_s;
            double x0, y0;
            if(flag)
            {
                (f_x, f_y, x_s, y_s, x0, y0) = get_test1();
            }
            else
            {
                (f_x, f_y, x_s, y_s, x0, y0) = get_test1();
            }
            
            Range r = new Range(0, 1);

            PointPairList s = MethodRungeKuti.Solve2EquationSystem(f_x, f_y, r, n, x0, y0);

            List<double> x = new List<double>(), y = new List<double>();

            for (double t = r.Start; t <= r.End; t += eps)
            {
                x.Add(x_s(t));
                y.Add(y_s(t));
            }

            PointPairList points = new PointPairList(x.ToArray(), y.ToArray());

            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Метод Рунге-Куты";

            LineItem line1 = pane.AddCurve("Точное решение", points, Color.BlueViolet, SymbolType.None);
            LineItem line2 = pane.AddCurve("Приблеженное решение", s, Color.Green, SymbolType.None);


            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        //private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        //{
            
        //    if(radioButton1.Checked)
        //    {
        //        SolveAndShow(n, true);
        //    }
        //    if(radioButton2.Checked)
        //    {
        //        SolveAndShow(n, false);
        //    }
        //}

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Controls.Clear();
            int n = Convert.ToInt32(textBox1.Text);
            SolveAndShow(n, true);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            zedGraphControl1.Controls.Clear();
            int n = Convert.ToInt32(textBox1.Text);
            SolveAndShow(n, false);
        }
    }
}
