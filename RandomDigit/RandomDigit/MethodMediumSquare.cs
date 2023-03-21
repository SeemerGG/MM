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
        private ulong _r0; 

        public MethodMediumSquare(ulong r0)
        {
            _r0 = r0;
        }

        public override double Generate()
        {
            try
            {
                string str = Convert.ToString(Math.Pow(_r0, 2));
                _r0 = Convert.ToUInt64(str.Substring(str.Length / 2 - 1, 4));
                return _r0 * 0.0001;
                

            }
            catch (Exception e)
            {
                if(_r0%10000 == 0)
                {
                    _r0 = (uint)Math.Pow(_r0, 2);
                    Generate();
                }
                MessageBox.Show(e.Message);
                return 0;
            }
        }
    }
}
