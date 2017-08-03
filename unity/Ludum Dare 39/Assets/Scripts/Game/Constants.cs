public static class Constants 
{
    public static class Time
    {
        public static readonly int Hours = 10;
        public static readonly int Minutes = 0;
        public static readonly float Ticker = 0.5f;
    }

    public static class Objects
    {
        public const int Floor = 0;
        public const int WayBelow = 1;
        public const int Tree = 2;
        public const int Battery = 3;
        public const int Radio = 4;
    }

    public static class Energy
    {
        public static readonly int MovementCost = 5;
        public static readonly int MaxCarryBattery = 3;
        public static readonly int Volts = 33;
        public static readonly int VoltageLoss = 3;
        public static readonly float OxygenLossDelay = 2f;
        public static readonly float BatteryLossDelay = 3f;
    }
}
