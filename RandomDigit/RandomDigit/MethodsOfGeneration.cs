using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDigit
{
    abstract class MethodsOfGeneration
    {
        public abstract double Generate();
        public double[] Generate(int N)
        {
            double[] acc = new double[N];

            for(int i = 0; i < N; i++)
            {
                acc[i] = Generate();
            }
            return acc;
        }
    }
}
