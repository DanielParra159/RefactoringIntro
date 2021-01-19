namespace Code
{
    public class PerformanceData
    {
        public readonly string PlayId;
        public readonly int Audience;
        public readonly Play Play;
        public readonly int Amount;
        public readonly int VolumeCredits;

        public PerformanceData(string playId, int audience, Play play, int amount, int volumeCredits)
        {
            PlayId = playId;
            Audience = audience;
            Play = play;
            Amount = amount;
            VolumeCredits = volumeCredits;
        }
    }
}