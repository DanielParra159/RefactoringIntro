using UnityEngine;

namespace Code
{
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

        public override int VolumeCredits()
        {
            return base.VolumeCredits() + Mathf.FloorToInt(Performance.Audience / 5f);
        }
    }
}