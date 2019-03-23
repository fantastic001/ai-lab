using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretrageOsnovno
{
    class Graph
    {
        public Dictionary<string, Node> graph = null;

        public Graph(string[] linesNodes, string[] linesLinks)
        {
            graph = new Dictionary<string, Node>();
            formGraph(linesNodes, linesLinks);
        }

        // TODO 1 : implementirati metodu formiranja grafa
        private void formGraph(string[] linesNodes, string[] linesLinks)
        {
            foreach (string node in linesNodes) {
                string name = node.Split(':')[0];
                int heuristic = Int32.Parse(node.Split(':')[1]);
                Node graphnode = new Node(name, heuristic);
                graph[name] = graphnode;
            }
            foreach (string linkstring in linesLinks) {
                string[] data = linkstring.Split(':')[1].Split(',');
                graph[data[0]].Links.Add(new Link(graph[data[0]], graph[data[1]], linkstring, Double.Parse(data[2])));
            }
        }

        #region ispis
        public void printGraph()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, Node> kvp in graph)
            {
                foreach (Link link in kvp.Value.Links)
                {
                    Console.WriteLine(link.Name + ":" + link.StartNode + "," + link.EndNode + "," + link.Cost);
                }
            }
        }
        #endregion
    }
}
