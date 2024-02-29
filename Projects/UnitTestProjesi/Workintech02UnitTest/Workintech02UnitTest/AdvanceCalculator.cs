using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workintech02UnitTest
{
    public class AdvanceCalculator : IAdvanceCalculator
    {
        public int Modulus(int a, int b)
        {
            var result = a % b;
            return result;
        }

        public int Power(int a, int b)
        {
            var result = (int)Math.Pow(a, b);
            return result;
        }

        public int SquareRoot(int a)
        {
            var result = (int)Math.Sqrt(a);
            return result;
        }

        public bool IsPrime(int a)
        {
            if (a < 2)
            {
                return false;
            }
            else if (a == 2)
            {
                return true;
            }
            else if (a % 2 == 0)
            {
                return false;
            }
            else
            {
                for (int i = 3; i <= Math.Sqrt(a); i += 2)
                {
                    if (a % i == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
