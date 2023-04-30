using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationComputingCenter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Controls.Clear();
            int timeToNextTask = Convert.ToInt32(textBox1.Text);
            int timeSort = Convert.ToInt32(textBox2.Text);
            int timeErrCorrection = Convert.ToInt32(textBox3.Text);
            int ErrProbility = Convert.ToInt32(textBox4.Text);
            int timeComputing = Convert.ToInt32(textBox5.Text);
            int countTask = Convert.ToInt32(textBox6.Text);

            Simulation model = new Simulation(countTask, timeToNextTask, timeSort, timeComputing, timeErrCorrection, ErrProbility);

            Label result = new Label();
            result.AutoSize = true;
            result.Text = model.GetStringInfo();
            result.Location = new Point(20, 20);
            groupBox2.Controls.Add(result);
        }
    }
}
