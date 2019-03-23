using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PretrageOsnovno
{
    class BreadthFirstSearch
    {
        public State Search(string startNodeName, string endNodeName)
        {
            HashSet<Node> visited = new HashSet<Node>();
            Queue<State> queue = new Queue<State>();
            State current = new State(Program.instance.graph[startNodeName]);
            queue.Enqueue(current);
            while (queue.Count > 0)
            {
                State s = queue.Dequeue();
                if (s.Node.Name.Equals(endNodeName))
                {
                    return s;
                }
                if (!visited.Contains(s.Node))
                {
                    visited.Add(s.Node);
                    foreach (State child in s.children())
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            return null;
        }
    }
}
