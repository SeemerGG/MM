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
            r0 = k * r0 + b % M;
            return r0;
        }
    }
}
