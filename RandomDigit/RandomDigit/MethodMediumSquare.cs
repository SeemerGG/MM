using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDigit
{
    internal class MethodMediumSquare : MethodsOfGeneration 
    {
        private int _r0; 

        public MethodMediumSquare(int r0)
        {
            _r0 = r0;
        }

        public override double Generate()
        {
            try
            {
                int square = Convert.ToInt32(Math.Pow(_r0, 2));
                string str = Convert.ToString(square);
                _r0 = Convert.ToInt32(str.Substring(str.Length / 2 , 4));
                return _r0 * 0.0001;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
