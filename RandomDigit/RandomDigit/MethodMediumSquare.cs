﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomDigit
{
    internal class MethodMediumSquare : MethodsOfGeneration 
    {
        private uint _r0; 

        public MethodMediumSquare(uint r0)
        {
            _r0 = r0 % 10000;
        }

        public override double Generate()
        {
            try
            {
                _r0 = Convert.ToUInt32(Math.Pow(_r0, 2) / 100.0) % 10000;
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
