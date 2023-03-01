using ZedGraph;

namespace MethodMonteCarlo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        double FTaskOne(double x)
        {
            int n = 7;//номер варианта
            if(x > 0 && x <= n)
            {
                return 10 * x / n;
            }
            if (x > n && x < 20)
            {
                return 10 * (x - 20) / (n - 20);
            }
            else
                return 0;
        }

        double FTaskTwo(double x)
        {
            return Math.Sqrt(11 - 7 * Math.Pow(Math.Sin(x), 2)); //7 - вариант 
        }

        private double Fx(double u)
        {
            return 7 + 7 * Math.Cos(u * Math.PI / 180);
        }
        private double Fy(double u)
        {
            return 7 + 7 * Math.Sin(u * Math.PI / 180);
        }



       

        void Task(int N,Func<double, double> func,double s0, double a, double step = 0.1)
        {
            int n = Convert.ToInt32(a / step);
            double b;
            int M = 0;
            double s;
            double del, absDel;
            PointPairList points = new PointPairList();
            PointPairList randPoints = new PointPairList();
            PointPairList randPointsIn = new PointPairList();
            PointPairList randPointsOut = new PointPairList();
            String answer;

            //вычисление
            for (double x = 0; x <= a; x = Math.Round(step + x, 2)) //x = Math.Round(step+x, 1)
            {
                points.Add(x, func(x)); //может быть вместо u должно быть n
            }
            b = points.Max(p => p.Y);

            //куча точек
            randPoints = GenerateRandPoint(a, b, N);

            //Находим количество точек внутри фигуры и площадь
            for (int i = 0; i < N; i++)
            {
                var point = randPoints[i];
                if (func(point.X) > point.Y)
                {
                    M++;
                    randPointsIn.Add(point);
                }
                else
                {
                    randPointsOut.Add(point);
                }
            }

            s = Convert.ToDouble(M) / N * a * b;

            //относительная и абсолютная погрешность
            absDel = Math.Abs(s - s0);
            del = absDel * 100 / s0;

            answer = String.Format("Количество точек всего N = {0} внутри фигуры M = {1}\n Площадь фигуры по методу Монте-Карло S = {2}\n" +
                " Площадь фигуры S0 = {3}\n Абсолютная погрешность равна {4}, относительная {5}%",N, M, Math.Round(s, 2), Math.Round(s0, 2), Math.Round(absDel, 2), Math.Round(del, 2));

            Draw(points, randPointsOut, randPointsIn, answer);
        }

        void Draw(PointPairList figure, PointPairList randPointsOut, PointPairList randPointIn, String answer)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Метод Монте-Карло";

            LineItem figureLine = pane.AddCurve("", figure, Color.Black, SymbolType.None);
            LineItem randPointsLineOut = pane.AddCurve("", randPointsOut, Color.DarkRed, SymbolType.Diamond);
            LineItem randPointsLineIn = pane.AddCurve("", randPointIn, Color.Green, SymbolType.Diamond);

            randPointsLineOut.Line.IsVisible = false;
            randPointsLineOut.Symbol.Fill.Type = FillType.Solid;
            randPointsLineOut.Symbol.Size = 5;

            randPointsLineIn.Line.IsVisible = false;
            randPointsLineIn.Symbol.Fill.Type = FillType.Solid;
            randPointsLineIn.Symbol.Size = 5;

            figureLine.Line.Width = 2;

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            label.Location = new Point(20, 20);
            label.AutoSize = true;
            label.Text = answer;
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(label);

        }

        void Task3(int N, Func<double, double> func1, Func<double, double> func2, double s0, double a)
        {
            double b;
            int M = 0;
            double s;
            double del, absDel;
            PointPairList points = new PointPairList();
            PointPairList randPoints = new PointPairList();
            PointPairList randPointsIn = new PointPairList();
            PointPairList randPointsOut = new PointPairList();
            String answer;

            b = a;

            //строю окружность
            for(int i = 0; i <= 360; i++)
            {
                points.Add(func1(i), func2(i));
            }

            //генериурую точки 
            randPoints  = GenerateRandPoint(a, b, N);

            //Находим количество точек внутри фигуры и площадь
            for (int i = 0; i < N; i++)
            {
                var point = randPoints[i];
                if (Math.Pow(point.X - 7, 2) + Math.Pow(point.Y - 7, 2) <= Math.Pow(a/2, 2))
                {
                    M++;
                    randPointsIn.Add(point);
                }
                else
                {
                    randPointsOut.Add(point);
                }
            }

            s = 4 * Convert.ToDouble(M) / N;
            //относительная и абсолютная погрешность
            absDel = Math.Abs(s - s0);
            del = absDel * 100 / s0;

            //Вывод параметров
            answer = String.Format("Количество точек всего N = {0} внутри фигуры M = {1}\n Площадь фигуры по методу Монте-Карло S = {2}\n" +
                 " Площадь фигуры S0 = {3}\n Абсолютная погрешность равна {4}, относительная {5}%", N, M, Math.Round(s, 2), Math.Round(s0, 2), Math.Round(absDel, 2), Math.Round(del, 2));

            Draw(points, randPointsOut, randPointsIn, answer);
        }

        static double Fi(double x, double y)
        {
            if (x > 0) return Math.PI - Math.Atan(y / x);
            else if (x < 0) return Math.PI + (Math.PI - Math.Atan(y / x));
            else if (x == 0 && y > 0) return Math.PI / 2;
            else if (x == 0 && y < 0) return Math.PI / 2 * (-1);
            else return 0;
        }

        //p(f) = sqrt(18 * cos(f)^2 + 4 * sin(f)^2
        private double P(double u)
        {
            return Math.Sqrt(18 * Math.Pow(Math.Cos(u), 2) + 4 * Math.Pow(Math.Sin(u), 2));
        }

        private double Fx4(double u)
        {
            return P(u) * Math.Cos(u);
        }

        private double Fy4(double u)
        {
            return P(u) * Math.Sin(u);
        }

        void Task4(int N, Func<double, double> func1, Func<double, double> func2, double s0 = Math.PI * 11)
        {
            double b, a;
            int M = 0;
            double s;
            double del, absDel;
            PointPairList points = new PointPairList();
            PointPairList randPoints = new PointPairList();
            PointPairList randPointsIn = new PointPairList();
            PointPairList randPointsOut = new PointPairList();
            String answer;

            //строим
            for (double i = 0; i <= Math.PI * 2; i+=0.05)
            {
                points.Add(func1(i), func2(i));
            }
            a = points.Max(p => p.X);
            b = points.Max(p => p.Y);

            //генериурую точки 
            Random rand = new Random();
            for (int i = 0; i <= N; i++)
            {
                randPoints.Add(-a + rand.NextDouble() * 2 * a, -b + rand.NextDouble() * b * 2);
            }
            //Находим количество точек внутри фигуры и площадь
            for (int i = 0; i < N; i++)
            {
                var point = randPoints[i];
                double r = Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
                double fi = Fi(point.X, point.Y);

                if(r < P(fi))
                {
                    M++;
                    randPointsIn.Add(point);
                }
                else
                {
                    randPointsOut.Add(point);
                }
            }

            s = Convert.ToDouble(M) / N * a * b * 4;

            //относительная и абсолютная погрешность
            absDel = Math.Abs(s - s0);
            del = absDel * 100 / s0;

            //Вывод параметров
            answer = String.Format("Количество точек всего N = {0} внутри фигуры M = {1}\n Площадь фигуры по методу Монте-Карло S = {2}\n" +
                 " Площадь фигуры S0 = {3}\n Абсолютная погрешность равна {4}, относительная {5}%", N, M, Math.Round(s, 2), Math.Round(s0, 2), Math.Round(absDel, 2), Math.Round(del, 2));

            Draw(points, randPointsOut, randPointsIn, answer);
        }

        PointPairList GenerateRandPoint(double a, double b, int N)
        {
            PointPairList randPoints = new PointPairList();
            Random rand = new Random();
            for (int i = 0; i <= N; i++)
            {
                randPoints.Add(rand.NextDouble() * a, rand.NextDouble() * b);
            }
            return randPoints;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task(Convert.ToInt32(numericUpDown1.Value),FTaskOne, 100, 20.0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task(Convert.ToInt32(numericUpDown1.Value), FTaskTwo, 13.3084071814083, 5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Task3(Convert.ToInt32(numericUpDown1.Value), Fx, Fy, Math.PI, 2 * 7);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task4(Convert.ToInt32(numericUpDown1.Value), Fx4, Fy4);
        }
    }
}