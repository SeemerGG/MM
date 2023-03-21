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
        private uint _r0, _r1;

        public MethodMediumMultipl(uint r0, uint r1)
        {
            _r0 = r0 % 10000;
            _r1 = r1 % 10000;
        }

        public override double Generate()
        {
            try
            {
                uint buf = _r0 * _r1 / 100 % 10000;
                _r0 = _r1;
                _r1 = buf;
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
