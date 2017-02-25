using AticAtacTypes;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace MapShapeColourizer
{
    internal class Colourizer
    {
        string mapFile;
        string shapeFile;

        public Colourizer(string shapeFile, string mapFile)
        {
            this.shapeFile = shapeFile;
            this.mapFile = mapFile;
        }

        public void Run()
        {
            // Read in the existing map data from the ObjectDataCreator tool
            string json = File.ReadAllText(mapFile);
            AticAtacMap map = JsonConvert.DeserializeObject<AticAtacMap>(json);

            // Create a dictionary to make things a little smoother
            Dictionary<int, AticAtacScreen> screens = new Dictionary<int, AticAtacScreen>();
            foreach (var screen in map.screens)
            {
                screens.Add(screen.screenId, screen);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter w = new BinaryWriter(ms))
                {
                    using (Stream s = File.OpenRead(shapeFile))
                    {
                        using (TextReader r = new StreamReader(s))
                        {
                            string line = "";
                            while ((line = r.ReadLine()) != null)
                            {
                                string[] rawBytes = line.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                byte[] item = new byte[rawBytes.Length];

                                for (int i = 0; i < rawBytes.Length; i++)
                                {
                                    string temp = rawBytes[i].Substring(0, 3);
                                    item[i] = Convert.ToByte(temp, 16);
                                }

                                w.Write(item);
                            }
                        }
                    }
                    
                    // Go back to the start of the memory stream
                    ms.Seek(0, SeekOrigin.Begin);

                    for (int i = 0; i < 149; i++)
                    {
                        int colour = ms.ReadByte();
                        int shape = ms.ReadByte();

                        colour = colour & 7;
                        switch (colour)
                        {
                            case 0:
                                screens[i].colour = new RoomColour(0, 0, 0);
                                break;
                            case 1:
                                screens[i].colour = new RoomColour(0, 0, 0.85f);
                                break;
                            case 2:
                                screens[i].colour = new RoomColour(0.85f, 0, 0);
                                break;
                            case 3:
                                screens[i].colour = new RoomColour(0.85f, 0, 0.85f);
                                break;
                            case 4:
                                screens[i].colour = new RoomColour(0, 0.85f, 0);
                                break;
                            case 5:
                                screens[i].colour = new RoomColour(0, 0.85f, 0.85f);
                                break;
                            case 6:
                                screens[i].colour = new RoomColour(0.85f, 0.85f, 0);
                                break;
                            case 7:
                                screens[i].colour = new RoomColour(0, 0, 0);
                                break;
                        }

                        screens[i].screenShape = shape;
                    }
                }
            }

            string outJson = JsonConvert.SerializeObject(map);
            File.WriteAllText("completemap.txt", outJson);
        }
    }
}