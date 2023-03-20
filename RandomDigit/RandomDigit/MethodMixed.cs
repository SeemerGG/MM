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
        private int _r0;

        public MethodMixed(int r0)
        {
            _r0 = r0;
        }

        public override double Generate()
        {
            try
            {
                string strBuf = Convert.ToString(_r0);
                string shiftLeft = strBuf.Substring(strBuf.Length / 4 + 1) + strBuf.Substring(0, strBuf.Length / 4);
                string shiftRight = strBuf.Substring(strBuf.Length - (strBuf.Length / 4)) + strBuf.Substring(0, strBuf.Length - strBuf.Length / 4);
                _r0 = (Convert.ToInt32(shiftLeft) + Convert.ToInt32(shiftRight)) % 1000000;
                return _r0 / 1000 * 0.0001;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
