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
    }
}
