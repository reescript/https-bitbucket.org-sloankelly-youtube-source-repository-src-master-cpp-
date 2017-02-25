using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AticAtacDrawer
{
    /*
    Copyright (c) 2016 Sloan Kelly

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    */
    public partial class AticAtacRoomDrawer : Form
    {
        bool hasDrawn = false;
        int index = 0;

        List<RoomData> data = new List<RoomData>();

        public AticAtacRoomDrawer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the room data from the file.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<RoomData> GetRooms(BinaryReader reader)
        {
            for (int i = 0; i < 13; i++)
            {
                int width = reader.ReadByte();
                int height = reader.ReadByte();

                int pointsOffset = reader.ReadByte();
                pointsOffset += reader.ReadByte() << 8;

                int lineToOffset = reader.ReadByte();
                lineToOffset += reader.ReadByte() << 8;

                RoomData roomData = new RoomData() { Height = height, Width = width, LinesOffset = lineToOffset, PointsOffset = pointsOffset };
                data.Add(roomData);
            }

            return data;
        }

        /// <summary>
        /// Parse the data file for the lines that draw the room shapes.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="room"></param>
        public void ParseLineData(BinaryReader reader, RoomData room)
        {
            bool finished = false;
            while (!finished)
            {
                byte b = reader.ReadByte();
                if (b == 255)
                {
                    if (room.LineData.Count > 0)
                    {
                        byte lastByte = room.LineData[room.LineData.Count - 1];
                        if (lastByte == 255)
                        {
                            finished = true;
                        }
                    }
                }
                
                room.LineData.Add(b);
            }
        }

        /// <summary>
        /// Redraw the picture box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            List<RoomData> rooms = null;

            if (!hasDrawn)
            {
                // *** You need to get your own binary data ***
                // What I did was to get a file .... from somewhere
                // I then used a spectrum emulator and saved raw
                // memory from 0 -> 65535.

                using (Stream s = File.OpenRead(@"g:\aticdump.BIN"))
                {
                    using (BinaryReader reader = new BinaryReader(s))
                    {
                        s.Seek(0xA982, SeekOrigin.Begin);
                        rooms = GetRooms(reader);

                        foreach (RoomData room in rooms)
                        {
                            s.Seek(room.LinesOffset, SeekOrigin.Begin);
                            ParseLineData(reader, room);
                        }

                        // Create a new bitmap image to save the room data
                        Bitmap bmp = new Bitmap(192, 192);

                        // Create a graphics object from the image
                        Graphics graphics = Graphics.FromImage(bmp);

                        // Draw the room to the bitmap
                        DescribeRoom(rooms[index], s, reader, graphics);

                        // Blit the bitmap to the form's picture box
                        e.Graphics.DrawImage(bmp, Point.Empty);

                        // Save the bitmap image
                        bmp.Save("room" + index + ".png");
                    }
                }
            }
        }

        /// <summary>
        /// Draw the room shape.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="s"></param>
        /// <param name="r"></param>
        /// <param name="graphics"></param>
        private void DescribeRoom(RoomData room, Stream s, BinaryReader r, Graphics graphics)
        {
            // Commented out because the game isn't using scaled images any more
            //float scale = 4f;
            //graphics.ScaleTransform(scale, scale);
            //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Clear the image to black
            graphics.Clear(Color.Black);

            int index = 0;
            while (room.LineData[index] != 255)
            {
                int offset = room.LineData[index] * 2;
                s.Seek(offset + room.PointsOffset, SeekOrigin.Begin);

                byte x = r.ReadByte();
                byte y = r.ReadByte();

                index++;
                while (room.LineData[index] != 255)
                {
                    int offset2 = room.LineData[index] * 2;
                    s.Seek(offset2 + room.PointsOffset, SeekOrigin.Begin);

                    byte tox = r.ReadByte();
                    byte toy = r.ReadByte();

                    Pen p = new Pen(Brushes.White, 1f);

                    graphics.DrawLine(p, new Point((int)x, (int)y), new Point((int)tox, (int)toy));
                    index++;
                }
                index++;
            }
        }

        /// <summary>
        /// Each click of the mouse draws the next room. It loops at the end.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            index++;                    // Increment the current room shape
            index %= 13;                // Clamp to the # of room shapes
            pictureBox1.Invalidate();   // Force the picture box to redraw
        }
    }
}
