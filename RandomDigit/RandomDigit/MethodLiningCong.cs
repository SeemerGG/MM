using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDigit
{
    internal class MethodLiningCong : MethodsOfGeneration
    {
        private uint _M, _k, _b, _r0;

        public MethodLiningCong(uint M, uint k, uint b, uint r0)
        {
            _M = M;
            _k = k;
            _b = b;
            _r0 = r0;
        }

        public override double Generate()
        {
            try
            {
                _r0 = (_k * _r0 + _b) % _M;
                return _r0 / (double)_M;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
