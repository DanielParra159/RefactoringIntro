using System;
using UnityEngine;

namespace Code
{
    public class PerformanceCalculator
    {
        private readonly Performance _performance;
        public readonly Play Play;

        public PerformanceCalculator(Performance performance, Play play)
        {
            _performance = performance;
            Play = play;
        }
        
        public int AmountFor()
        {
            var result = 0;

            switch (Play.Type)
            {
                case "tragedy":
                    result = 40000;
                    if (_performance.Audience > 30)
                    {
                        result += 1000 * (_performance.Audience - 30);
                    }

                    break;
                case "comedy":
                    result = 30000;
                    if (_performance.Audience > 20)
                    {
                        result += 10000 + 500 * (_performance.Audience - 20);
                    }

                    result += 300 * _performance.Audience;
                    break;
                default:
                    throw new Exception($"Unknown type: {Play.Type}");
            }

            return result;
        }
        
        
        public int VolumeCredits()
        {
            var result = Mathf.Max(_performance.Audience - 30, 0);
            if (Play.Type == "comedy") result += Mathf.FloorToInt(_performance.Audience / 5f);
            return result;
        }
    }
}
