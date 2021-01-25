using System;
using UnityEngine;

namespace Code
{
    public class TragedyCalculator : PerformanceCalculator
    {
        public TragedyCalculator(Performance performance, Play play) : base(performance, play)
        {
        }

        public override int AmountFor()
        {
            var result = 40000;
            if (Performance.Audience > 30)
            {
                result += 1000 * (Performance.Audience - 30);
            }

            return result;
        }
    }

    public class ComedyCalculator : PerformanceCalculator
    {
        public ComedyCalculator(Performance performance, Play play) : base(performance, play)
        {
        }

        public override int AmountFor()
        {
            var result = 30000;
            if (Performance.Audience > 20)
            {
                result += 10000 + 500 * (Performance.Audience - 20);
            }

            result += 300 * Performance.Audience;
            return result;
        }
    }
    public abstract class PerformanceCalculator
    {
        protected readonly Performance Performance;
        public readonly Play Play;

        public PerformanceCalculator(Performance performance, Play play)
        {
            Performance = performance;
            Play = play;
        }

        public abstract int AmountFor();
        
        public int VolumeCredits()
        {
            var result = Mathf.Max(Performance.Audience - 30, 0);
            if (Play.Type == "comedy") result += Mathf.FloorToInt(Performance.Audience / 5f);
            return result;
        }
    }
}
