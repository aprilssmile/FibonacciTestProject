using System;
using System.Numerics;
using System.Threading.Tasks;
using FibbonacciCalculations.DTO;
using FibbonacciCalculations.Services;
using Transport.Interfaces;

namespace FibbonacciCalculations
{
    public class FibonacciService
    {
        private readonly IDataSender _sender;
        private readonly FibonacciCalculator _calculator;

        public FibonacciService(IDataSender sender)
        {
            _sender = sender;
            _calculator = new FibonacciCalculator();
        }

        public void InitCalculations(int asyncCalculationsCount)
        {
            Parallel.For(0, asyncCalculationsCount,
                i =>
                {
                    _sender.SendResult(new CalculationDTO(new BigInteger(0), new BigInteger(1)));
                });
        }

        public void Handle(CalculationDTO dto)
        {
            var newValue = _calculator.GetNext(dto.PreviousValue, dto.CurrentValue);
            Console.WriteLine(newValue);
            _sender.SendResult(new CalculationDTO(dto.CurrentValue, newValue));
        }
    }
}