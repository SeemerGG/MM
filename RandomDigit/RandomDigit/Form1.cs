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

namespace RandomDigit
{
    public partial class Form1 : Form
    {
        int r0, M, k, b, N, countDigit;

        public Form1()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetParametr();
            MethodMediumSquare obj = new MethodMediumSquare(r0);
            Draw(obj.Generate(countDigit));
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void GetParametr()
        {
            N = Convert.ToInt32(textBox1.Text);
            r0 = Convert.ToInt32(textBox2.Text);
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
        }
    }
}
