using UnityEngine;

namespace Code
{
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
        
        public virtual int VolumeCredits()
        {
            var result = Mathf.Max(Performance.Audience - 30, 0);
            return result;
        }
    }
}
