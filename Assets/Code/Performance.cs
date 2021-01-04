namespace Code
{
    public class Performance
    {
        public readonly string PlayId;
        public readonly int Audience;

        public Performance(string playId, int audience)
        {
            PlayId = playId;
            Audience = audience;
        }
    }
}