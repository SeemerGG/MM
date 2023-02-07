using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MNK
{
    public class LSM
    {
        // Массивы значений Х и У задаются как свойства
        public double[] x { get; set; }
        public double[] y { get; set; }

        // Искомые коэффициенты полинома в данном случае, а в общем коэфф. при функциях
        private double[] _coeff;
        public double[] coeff { get { return coeff; } }

        // Среднеквадратичное отклонение
        public double? delta { get { return getDelta(); } }

        // Конструктор класса. Примает 2 массива значений х и у
        // Длина массивов должна быть одинакова, иначе нужно обработать исключение
        public LSM(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new ArgumentException("X and Y arrays should be equal!");
            x = new double[x.Length];
            y = new double[y.Length];

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = x[i];
                y[i] = y[i];
            }
        }

        public void Linear()
        {
            double[,] basic = new double[2, 3];

            for(int i=0; )

        }
        // Функция нахождения среднеквадратичного отклонения
        private double? getDelta()
        {
            return 0;
        }
    }
}
