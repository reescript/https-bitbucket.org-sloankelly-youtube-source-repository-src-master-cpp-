using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AticAtacDrawer
{
    public class RoomData
    {
        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int PointsOffset
        {
            get;
            set;
        }

        public int LinesOffset
        {
            get;
            set;
        }

        public List<Byte> LineData
        {
            get;
            private set;
        }

        public RoomData()
        {
            LineData = new List<byte>();
        }
    }
}
