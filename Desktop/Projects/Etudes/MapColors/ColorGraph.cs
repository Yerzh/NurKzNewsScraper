using System;
using System.IO;
using System.Collections.Generic;

namespace MapColors
{
    public class ColorGraph
    {
        private Dictionary<string, int> dict;
        private string[] keys;
        private List<int>[] adj;
        private int[] colors;

        public int V { get; private set; }
        public int E { get; private set; }

        public ColorGraph(string filePath)
        {            
            dict = new Dictionary<string, int>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] a = line.Split();
                foreach(var i in a)
                    if (!Contains(i))
                        dict.Add(i, dict.Count);
            }

            keys = new string[dict.Count];
            foreach (string name in dict.Keys)
                keys[dict[name]] = name;

            this.V = keys.Length;
            this.E = lines.Length;
            
            adj = new List<int>[V];
            for (int i = 0; i < V; i++)
                adj[i] = new List<int>();

            foreach(var line in lines)
            {
                string[] splitted = line.Split();
                AddEdge(Index(splitted[0]), Index(splitted[1]));
            }
        }

        public void AddEdge(int v, int w)
        {
            adj[v].Add(w);
            adj[w].Add(v);
        }

        public IEnumerable<int> Adj(int v)
        {
            return adj[v];
        }

        public override string ToString()
        {
            string s = V + " вершин, " + E + " ребер" + Environment.NewLine;
            for (int v = 0; v < V; v++)
            {
                s += Name(v) + ": ";
                foreach (int w in this.Adj(v))
                    s += Name(w) + " ";

                s += Environment.NewLine;
            }

            return s;
        }

        public bool Contains(string s) { return dict.ContainsKey(s); }

        public int Index(string s) { return dict[s]; }

        public string Name(int v) { return keys[v]; }

        public bool ColorVertices(int colorCount)
        {
            colors = new int[V];

            if (!ColoringUtil(colorCount, 0))
                return false;

            return true;
        }

        private bool ColoringUtil(int colorCount, int v)
        {
            if (v == V)
                return true;

            for (int c = 1; c <= colorCount; c++)
            {
                if (IsRightColor(v, c))
                {
                    colors[v] = c;

                    if (ColoringUtil(colorCount, v + 1))
                        return true;

                    colors[v] = 0;
                }
            }

            return false;
        }

        private bool IsRightColor(int v, int c)
        {
            foreach (var i in Adj(v))
                if (c == colors[i])
                    return false;

            return true;
        }

        public void ShowColors()
        {
            Console.WriteLine("Solution Exists: Following are the assigned colors");
            foreach (var c in colors)
                Console.Write(c + " ");
            Console.WriteLine();
        }
    }
}

