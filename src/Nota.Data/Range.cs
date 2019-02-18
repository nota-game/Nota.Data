namespace Nota.Data
{
    public class Range
    {
        public Range(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }

        public float Max { get; }

        public float Min { get; }
    }
}