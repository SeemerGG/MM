using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace RandomDigit
{
    public partial class Form1 : Form
    {
        uint r0, M = (uint)Math.Pow(2, 31) - 1, k = 1220703125, b = 7; 
        int N, countDigit;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetParametr();
            MethodMediumMultipl obj = new MethodMediumMultipl(r0, 2435);
            Draw(obj.Generate(countDigit));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetParametr();
            MethodLiningCong obj = new MethodLiningCong(2*3*3*5*7*9, 2*3*25*7 + 1, 103, r0);
            Draw(obj.Generate(countDigit));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetParametr();
            MethodMediumSquare obj = new MethodMediumSquare(r0);
            Draw(obj.Generate(countDigit));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetParametr();
            MethodMixed obj = new MethodMixed(r0);
            Draw(obj.Generate(countDigit));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void GetParametr()
        {
            N = Convert.ToInt32(textBox1.Text);
            r0 = Convert.ToUInt32(textBox2.Text);
            countDigit = Convert.ToInt32(textBox3.Text);
        }

        double GetMathWait(double[] r)
        {
            return r.Sum() / countDigit;
        }

        double GetDisp(double[] r, double m)
        {
            return r.Aggregate((x1, x2) => Math.Pow((x1 - m),2) + Math.Pow((x2 - m), 2))/countDigit;
        }

        void Draw(double[] r)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Генерация случайных чисел";
            pane.CurveList.Clear();

            double[] valuesY = new double[N];
            string[] valuesX = new string[N];
            for(int i = 0; i < N; i++)
            {
                valuesX[i] = String.Format("[{0}:{1}]", i * 1.0 / N, (i + 1) * 1.0 / N);
            }
            for (int k = 0; k < r.Length; k++)
            {
                for (int i = 0; i < N; i++)
                {
                    if (r[k] > i * 1.0 / N && r[k] < (i + 1) * 1.0 / N)
                    {
                        valuesY[i] += 1;
                    }
                }
            }

            BarItem bar = pane.AddBar("",null, valuesY, Color.AliceBlue);
            pane.XAxis.Type = AxisType.Text;
            pane.XAxis.Scale.TextLabels = valuesX;
            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraphControl1.AxisChange();

            zedGraphControl1.Invalidate();

            //System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            //label.Location = new Point(200, 10);
            //label.AutoSize = true;
            label4.Text = String.Format("Матиматическое ожидание: {0:f4}, Дисперсия: {1:f4}", GetMathWait(r), GetDisp(r, GetMathWait(r)));
        }
    }
}
