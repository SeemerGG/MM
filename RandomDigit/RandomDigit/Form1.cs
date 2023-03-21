using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDigit
{
    public partial class Form1 : Form
    {
<<<<<<< HEAD
        ulong r0, M, k, b;
        int N, countDigit;

=======
>>>>>>> parent of 67b3e7c (Не успел)
        public Form1()
        {
            InitializeComponent();
            string str = "";
            for (int i = 0; i < 10; i++)
                str += Convert.ToString(()) + " ";

            textBox1.Text = str;
        }
        int mainDigitForMed = 9876;
        int R0ForMedMut = 9876;
        int R1ForMedMut = 6789;
        int R0ForMix = 657945;
        //метод серединных квадратов
        double MediumSquare()
        {
            string buf = Convert.ToString(Math.Pow(mainDigitForMed, 2));
            mainDigitForMed = Convert.ToInt32(buf.Substring(buf.Length / 2, 4));
            return mainDigitForMed * 0.0001;
        }
        //метод серединных произведений 
        double MediumMultipl()
        {
            string buf = Convert.ToString(R0ForMedMut*R1ForMedMut);
            int buf2 = Convert.ToInt32(buf.Substring(buf.Length / 2, 4));
            R0ForMedMut = R1ForMedMut;
            R1ForMedMut = buf2;
            return R1ForMedMut * 0.0001; 
        }
        //Метод перемешивания
        double Mix()
        {
            string buf = Convert.ToString(R0ForMix);
            //сдвиг влево
            string buf2 = buf.Substring(2, buf.Length - 2) + buf.Substring(0, 2);
            //сдвиг вправо
            string buf3 = buf.Substring(0, buf.Length - 2) + buf.Substring(buf.Length - 3, 2);
            buf = Convert.ToString(Convert.ToInt32(buf2) + Convert.ToInt32(buf3));
            R0ForMix = Convert.ToInt32(buf);
            return Math.Round(Convert.ToDouble("0," + buf), 4);
        }
        //Линейный конгруэндный метод
        int M = Convert.ToInt32(Math.Pow(2, 31)) - 1;
        int k = 1220703125;
        int r0 = 7;
        int b = 7;
        double Linea()
        {
<<<<<<< HEAD

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void GetParametr()
        {
            N = Convert.ToInt32(textBox1.Text);
            r0 = Convert.ToUInt64(textBox2.Text);
            countDigit = Convert.ToInt32(textBox3.Text);
        }

        double GetMathWait(double[] r)
        {
            return r.Sum() / N;
        }

        double GetDisp(double[] r, double m)
        {
            return r.Aggregate((x1, x2) => (x1 - m) + (x2 - m))/N;
        }

        void Draw(double[] r)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            double[] values = new double[N];

            for(int k = 0; k < r.Length; k++)
            {
                for (int i = 0; i < N - 1; i++)
                {
                     if (r[k] > i * 1 / N && r[k] < (i + 1) * 1 / N)
                     {
                        values[i] += 1;
                     }
                }
            }

            BarItem bar = pane.AddBar("", null, values, Color.AliceBlue);
            pane.BarSettings.MinClusterGap = 0.0f;

            zedGraphControl1.AxisChange();

            zedGraphControl1.Invalidate();

            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            label.Location = new Point(200, 10);
            label.AutoSize = true;
            label.Text = String.Format("Матиматическое ожидание: {0:f4}, Дисперсия: {1:f4}", GetMathWait(r), GetDisp(r, GetMathWait(r)));
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(label);
=======
            r0 = k * r0 + b % M;
            return r0;
>>>>>>> parent of 67b3e7c (Не успел)
        }
    }
}
