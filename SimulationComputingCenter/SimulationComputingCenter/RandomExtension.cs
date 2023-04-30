using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationComputingCenter
{
    public static class RandomExtension
    {
        public static bool NextBool(this Random rnd, int probility = 50)
        {

            bool[] bools = new bool[100];

            for(int i = 0; i < 100; i++)
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

            return bools[rnd.Next(0, 100)];
        }
    }
}
