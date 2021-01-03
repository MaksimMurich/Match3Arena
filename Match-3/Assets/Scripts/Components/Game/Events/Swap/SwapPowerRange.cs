namespace Match3.Components.Game.Events
{
    public class SwapPowerRange
    {
        public float Power;
        public float RangeMin;
        public float RangeMax;

        internal void SetPower(float value)
        {
            Power = value;
        }
    }
}
