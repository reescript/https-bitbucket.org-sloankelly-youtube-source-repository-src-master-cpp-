using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using AticAtacTypes;

namespace ObjectDataCreator
{
    internal class BackgroundItemsCreator
    {
        string path;

        public BackgroundItemsCreator(string path)
        {
            this.path = path;
        }

        internal void Run()
        {
            List<BackgroundItem> items = new List<BackgroundItem>();

            using (Stream s = File.OpenRead(path))
            {
                using (TextReader r = new StreamReader(s))
                {
                    string line = "";
                    int index = 0;
                    while ((line = r.ReadLine()) != null)
                    {
                        byte[] item = new byte[8];
                        string[] rawBytes = line.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < 8; i++)
                        {
                            string temp = rawBytes[i].Substring(0, 3);
                            item[i] = Convert.ToByte(temp, 16);
                        }

                        items.Add(new BackgroundItem(index++, item));
                    }
                }
            }

            // Link the items in the list depending on their even/odd state
            for (int i = 0; i < items.Count; i++)
            {
                if (IsDoor(items[i]))
                {
                    if (i == 0 || i % 2 == 0)
                    {
                        // It's even
                        items[i].LinkedId = i + 1;
                    }
                    else
                    {
                        // it's odd!
                        items[i].LinkedId = i - 1;
                    }
                }
            }

            items.Sort(
                (item1, item2)
                    => item1.Screen.CompareTo(item2.Screen));

            AticAtacMap map = new AticAtacMap() { screens = new List<AticAtacScreen>() };
            AticAtacScreen current = null;

            foreach (var item in items)
            {
                if (current == null || current.screenId != item.Screen)
                {
                    current = new AticAtacScreen() { screenId = item.Screen };
                    current.objects = new List<BackgroundObject>();
                    current.colour = new RoomColour() { red = 1f, green = 1f, blue = 1f };
                    map.screens.Add(current);
                }

                current.objects.Add(new BackgroundObject(item));
            }

            string json = JsonConvert.SerializeObject(map);
            File.WriteAllText("aticatacmap.txt", json);
        }

        bool IsDoor(BackgroundItem bk)
        {
            if (bk.Graphic>= 1 && bk.Graphic<=16)
            {
                return true;
            }

            if (bk.Graphic >=23 && bk.Graphic <=26)
            {
                return true;
            }

            if (bk.Graphic>=32 && bk.Graphic <=36)
            {
                return true;
            }

            return false;
        }
    }
}