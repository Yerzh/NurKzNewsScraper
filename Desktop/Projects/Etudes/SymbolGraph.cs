using System;
using System.IO;
using System.Collections.Generic;

namespace MapColors
{
    public class SymbolGraph
    {
        private Dictionary<string, int> dict;
        private string[] keys;
        private MapColors.Graph G;

        public SymbolGraph(string filePath, string sp)
        {
            dict = new Dictionary<string, int>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] a = line.Split(sp);
                for (int i = 0; i < a.Length; i++)
                    if (!dict.ContainsKey(a[i]))
                        dict.Add(a[i], dict.Count); 
            }
            keys = new string[dict.Count];
            foreach (string name in dict.Keys)
                keys[dict[name]] = name;
            G = new Graph(dict.Count);
            foreach (var line in lines)
            {
                string[] a = line.Split(sp);
                for (int i = 0; i < a.Length; i++)
                    G.
            }
        }
    }
}

