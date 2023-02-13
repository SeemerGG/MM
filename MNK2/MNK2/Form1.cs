using ZedGraph;

namespace MNK2
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }

        private double Leaner(double x, double a, double b)
        {
            return x * a + b;
        }
        private double Power(double x, double a, double b)
        {
            return b * Math.Pow(x, a);
        }
        private double Exp(double x, double a, double b)
        {
            return b * Math.Exp(a * x);
        }
        private double Square(double x, double a, double b, double c)
        {
            return a * Math.Pow(x, 2) + b * x + c;
        }

        private double[,] CreateBasic(double[] x, double[] y, int n)
        {
            double[,] basic = new double[n, n + 1];

            for (int i = 0; i < x.Length; i++)
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

        private double[,] CreateBasic2(double[] x, double[] y, int n)
        {
            double[,] basic = new double[n, n + 1];

            for (int i = 0; i < x.Length; i++)
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

            return basic;
        }


        private (double, double, double, double) Determ2(double[,] basic)
        {
            double determ1 = basic[0, 1] * basic[1, 2] * basic[2,3]  - basic[2,1] * basic[1,2] * basic[0,3] + basic[1,1] * basic[2,2] * basic[0,3] + basic[2, 1] * basic[0, 2] * basic[1, 3] - basic[0, 1] * basic[2, 2] * basic[1, 3] - basic[1, 1] * basic[0, 2] * basic[2, 3];
            double determ2 = -(basic[0, 0] * basic[1, 2] * basic[2, 3] - basic[2, 0] * basic[1, 2] * basic[0, 3] + basic[0, 1] * basic[0, 3] * basic[2, 2] + basic[2, 0] * basic[0, 2] * basic[1, 3] - basic[0, 0] * basic[2, 2] * basic[1, 3] - basic[1, 0] * basic[2, 3] * basic[0, 2]);
            double determ3 = basic[0, 0] * basic[1, 1] * basic[2, 3] - basic[2, 0] * basic[1, 1] * basic[0, 3] + basic[1, 0] * basic[2, 1] * basic[0, 3] + basic[2, 0] * basic[0, 1] * basic[1, 3] - basic[0, 0] * basic[2, 1] * basic[1, 3] - basic[2, 3] * basic[0, 1] * basic[1, 0];
            double determ = basic[0, 0] * basic[1, 1] * basic[2, 2] - basic[2, 0] * basic[1, 1] * basic[0, 2] + basic[1, 0] * basic[2, 1] * basic[0, 2] + basic[2, 0] * basic[0, 1] * basic[1, 2] - basic[0, 0] * basic[2, 1] * basic[1, 2] - basic[2, 2] * basic[0, 1] * basic[1, 0];

            return (determ, determ1, determ2, determ3);
        }

        private (double, double, double) Determ(double[,] basic)
        {
            double determ1 = basic[0, 2] * basic[1, 1] - basic[0, 1] * basic[1, 2];
            double determ2 = basic[0, 0] * basic[1, 2] - basic[0, 2] * basic[1, 0];
            double determ = basic[0, 0] * basic[1, 1] - basic[1, 0] * basic[0, 1];

            return (determ, determ1, determ2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] str1 = textBox10.Text.Split();
            string[] str2 = textBox9.Text.Split();

            double[] xMas = new double[str1.Length];
            double[] yMas = new double[str2.Length];

            for(int i = 0; i < str1.Length; i++)
            {
                xMas[i] = Convert.ToDouble(str1[i]);
                yMas[i] = Convert.ToDouble(str2[i]);
            }

            double pog;
            const int n = 2;
            double determ, determ1, determ2, determ3;
            double a, b, c;
            double step = 0.01;
            
            //zedGraph
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Метод наименьших квадратов";
            pane.CurveList.Clear();
            PointPairList list = new PointPairList();
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();

            // Рисую начальные точки
            list.Add(xMas, yMas);
            LineItem curve1 = pane.AddCurve("Inital values", list, Color.Black, SymbolType.Diamond);
            curve1.Line.IsVisible = false;
            curve1.Symbol.Fill.Color = Color.Black;
            curve1.Symbol.Fill.Type = FillType.Solid;
            curve1.Symbol.Size = 7;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            double[,] basic = new double[n, n + 1];

            //Lenear

            basic = CreateBasic(xMas, yMas, n);

            (determ, determ1, determ2) = Determ(basic);

            a = determ1 / determ;
            b = determ2 / determ;

            textBox1.Text = String.Format("{0:f2}, {1:f2}", a, b);

            pog = 0;

            for(int i = 0; i < xMas.Length; i++)
            {
                pog += Math.Pow((yMas[i] - Leaner(xMas[i], a, b)), 2);
            }

            for(double x = xMas[0]; x < xMas.Last(); x += step)
            {
                double y = Leaner(x, a, b);
                list1.Add(x, y);
            }

            LineItem curve2 = pane.AddCurve("Leaner", list1, Color.Blue, SymbolType.None);
            textBox8.Text = Convert.ToString(pog);



            //Power

            double[] xLn = new double[xMas.Length];
            double[] yLn = new double[yMas.Length];

            for(int i = 0; i < xMas.Length; i++)
            {
                xLn[i] = Math.Log(xMas[i]);
                yLn[i] = Math.Log(yMas[i]);
            }

            basic = CreateBasic(xLn, yLn, n);

            (determ, determ1, determ2) = Determ(basic);

            a = determ1 / determ;
            b = Math.Exp(determ2 / determ);

            textBox2.Text = String.Format("{0:f2}, {1:f2}", a, b);

            pog = 0;

            for (int i = 0; i < xMas.Length; i++)
            {
                pog += Math.Pow((yMas[i] - Power(xMas[i], a, b)), 2);
            }

            for (double x = xMas[0]; x < xMas.Last(); x += step)
            {
                double y = Power(x, a, b);
                list2.Add(x, y);
            }

            LineItem curve3 = pane.AddCurve("Power", list2, Color.Green, SymbolType.None);
            textBox7.Text = Convert.ToString(pog);

            //Exp

            basic = CreateBasic(xMas, yLn, n);

            (determ, determ1, determ2) = Determ(basic);

            a = determ1 / determ;
            b = Math.Exp(determ2 / determ);

            textBox3.Text = String.Format("{0:f2}, {1:f2}", a, b);

            pog = 0;

            for (int i = 0; i < xMas.Length; i++)
            {
                pog += Math.Pow((yMas[i] - Exp(xMas[i], a, b)), 2);
            }

            for (double x = xMas[0]; x < xMas.Last(); x += step)
            {
                double y = Exp(x, a, b);
                list3.Add(x, y);
            }

            LineItem curve4 = pane.AddCurve("Exp", list3, Color.Yellow, SymbolType.None);
            textBox6.Text = Convert.ToString(pog);

            //Square

            basic = CreateBasic2(xMas, yMas, n+1);

            (determ, determ1, determ2, determ3) = Determ2(basic);

            a = determ1 / determ;
            b = determ2 / determ;
            c = determ3 / determ;

            textBox4.Text = String.Format("{0:f2}, {1:f2}, {2:f2}", a, b, c);

            pog = 0;

            for (int i = 0; i < xMas.Length; i++)
            {
                pog += Math.Pow((yMas[i] - Square(xMas[i], a, b, c)), 2);
            }

            for (double x = xMas[0]; x < xMas.Last(); x += step)
            {
                double y = Square(x, a, b, c);
                list4.Add(x, y);
            }

            LineItem curve5 = pane.AddCurve("Square", list4, Color.Red, SymbolType.None);
            textBox5.Text = Convert.ToString(pog);


            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
    }
}