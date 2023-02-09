using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MNK
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


            double[] x = { 2, 4, 5, 6, 7, 8 };
            double[] y = { 2.4, 2.9, 3, 3.5, 3.6, 3.7 };

            LSM first = new LSM(x, y);
            first.Linear();
            
        }
    }
}
