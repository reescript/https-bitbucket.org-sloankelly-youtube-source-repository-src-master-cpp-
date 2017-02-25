namespace AticAtacTypes
{
    public class BackgroundItem
    {
        public BackgroundItem(int id, byte[] item)
        {
            Graphic = item[0];
            Id = id;
            Screen = item[1];
            Unknown = item[2];
            X = item[3];
            Y = item[4];
            Flags = item[5];
            DoorTiming = item[6];
            UnknownFlags = item[7];
        }

        public int Id { get; set; }
        public int LinkedId { get; set; }

        public byte Graphic { get; set; }
        public byte Screen { get; set; }
        public byte Unknown { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte Flags { get; set; }
        public byte DoorTiming { get; set; }
        public byte UnknownFlags { get; set; }
    }
}
