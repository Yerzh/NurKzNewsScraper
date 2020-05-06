using System;
using System.IO;

namespace MapColors
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, @"Res\", "boundaries.txt");
            ColorGraph cg = new ColorGraph(filePath);
            if (!cg.ColorVertices(4))
                Console.WriteLine("Solution doesn't exist");
            else
                cg.ShowColors();
        }
    }
}
