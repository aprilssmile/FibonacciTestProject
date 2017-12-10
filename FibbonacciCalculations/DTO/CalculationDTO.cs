using System.Numerics;

namespace FibbonacciCalculations.DTO
{
    public class CalculationDTO
    {
        public CalculationDTO(BigInteger previousValue, BigInteger currentValue)
        {
            PreviousValue = previousValue;
            CurrentValue = currentValue;
        }

        public BigInteger PreviousValue { get; }
        public BigInteger CurrentValue { get; }
    }
}
