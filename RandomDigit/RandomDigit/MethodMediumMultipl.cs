using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDigit
{
    internal class MethodMediumMultipl : MethodsOfGeneration 
    {
        private int _r0, _r1;

        public MethodMediumMultipl(int r0, int r1)
        {
            _r0 = r0;
            _r1 = r1;
        }

        public override double Generate()
        {
            try
            {
                int r2 = _r0* _r1;
                string str = Convert.ToString(r2);
                _r0 = _r1;
                _r1 = Convert.ToInt32(str.Substring(str.Length - 1, 4));
                return _r1 * 0.0001;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
