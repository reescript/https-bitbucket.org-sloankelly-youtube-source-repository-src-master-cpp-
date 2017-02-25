using System;

namespace MapShapeColourizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage:\r\nmapshapecolourizer shapefile aticatacmapfile");
            }
            else
            {
                string shapeFile = args[0];
                string mapFile = args[1];

                Colourizer clr = new Colourizer(shapeFile, mapFile);
                clr.Run();
            }
        }
    }
}
