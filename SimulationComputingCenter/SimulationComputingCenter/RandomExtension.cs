using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationComputingCenter
{
    public static class RandomExtension
    {
        public static bool NextBool(this Random rnd, double probility = 0.5)
        {
            probility *= 10;

            bool[] bools = new bool[10];

            for(int i = 0; i < 10; i++)
            {
                if(i <= probility)
                {
                    bools[i] = true;
                }
                else
                {
                    bools[i] = false;
                }
            }

            return bools[rnd.Next(0, 10)];
        }
    }
}
