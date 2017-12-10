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
        private readonly ILogger _logger;

        private readonly FibonacciCalculator _calculator;

        public FibonacciService(IDataSender sender, ILogger logger)
        {
            _sender = sender;
            _logger = logger;
            _calculator = new FibonacciCalculator();
        }

        public void InitCalculations(int asyncCalculationsCount)
        {
            _logger.Info($"Start {asyncCalculationsCount} calculations of fibonacci number");

            Parallel.For(0, asyncCalculationsCount,
                i =>
                {
                    _sender.SendResult(new CalculationDTO(new BigInteger(0), new BigInteger(1)));
                });

            _logger.Info("All calculations started");

        }

        public void Handle(CalculationDTO dto)
        {
            var newValue = _calculator.GetNext(dto.PreviousValue, dto.CurrentValue);
            _logger.Debug(newValue);

            _sender.SendResult(new CalculationDTO(dto.CurrentValue, newValue));
        }
    }
}