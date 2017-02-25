namespace AticAtacTypes
{
    public class BackgroundObject
    {
        public byte graphic;
        public byte unknown;
        public int x;
        public int y;
        public int id; // the background object's ID
        public int linkedId; // The object the background object is connected
        public byte flags;
        public byte doorTiming;
        public byte unknownFlags;

        public BackgroundObject()
        {

        }

        public BackgroundObject(BackgroundItem item)
        {
            graphic = item.Graphic;
            unknown = item.Unknown;
            x = item.X;
            y = item.Y;
            id = item.Id;
            linkedId = item.LinkedId;
            flags = item.Flags;
            doorTiming = item.DoorTiming;
            unknownFlags = item.UnknownFlags;
        }
    }
}
