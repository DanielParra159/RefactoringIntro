using System;

namespace Code
{
    public class PerformanceCalculatorFactory
    {
        public PerformanceCalculator Create(Performance performance, Play play)
        {
            switch (play.Type)
            {
                case "tragedy":
                    return new TragedyCalculator(performance, play);
                case "comedy":
                    return new ComedyCalculator(performance, play);
                default:
                    throw new Exception($"Unknown type: {play.Type}");
            }
        }
    }
}