namespace Workintech02UnitTest
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public double Divide(int a, int b)
        {
            if(b==0)
            {
                throw new System.DivideByZeroException("Cannot divide by zero");
            }
            return a / b;
        }

        public int Times(int a, int b)
        {
            return a * b;
        }
    }
}
