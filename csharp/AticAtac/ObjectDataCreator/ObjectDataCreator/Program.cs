using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDataCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Usage:\r\nobjectdatacreator objectfile.txt");
            }
            else
            {
                BackgroundItemsCreator creator = new BackgroundItemsCreator(args[0]);
                creator.Run();
            }
        }
    }
}
