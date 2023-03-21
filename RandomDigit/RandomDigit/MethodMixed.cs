using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDigit
{
    internal class MethodMixed : MethodsOfGeneration
    {
        private uint _r0;

        public MethodMixed(uint r0)
        {
            _r0 = r0 % 10000;
        }

        public override double Generate()
        {
            try
            {
                uint shiftLeft = _r0 * 100 + _r0 % 100;
                uint shiftRight = _r0 / 100 + _r0 / 1000000 * 1000000;
                _r0 = (shiftLeft + shiftRight) % 100000000;

                return _r0 * 0.00000001;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
