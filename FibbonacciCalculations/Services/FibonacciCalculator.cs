using System.Numerics;

namespace FibbonacciCalculations.Services
{
    public class FibonacciCalculator
    {
        public BigInteger GetNext(BigInteger prevValue, BigInteger currentValue)
        {
            return prevValue + currentValue;
        }
    }
}