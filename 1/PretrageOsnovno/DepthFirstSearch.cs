﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace PretrageOsnovno
{
    class DepthFirstSearch
    {
        public State Search(string startNodeName, string endNodeName)
        {
            HashSet<Node> visited = new HashSet<Node>();
            Stack<State> queue = new Stack<State>();
            State current = new State(Program.instance.graph[startNodeName]);
            queue.Push(current);
            while (queue.Count > 0) {
                State s = queue.Pop();
                if (s.Node.Name.Equals(endNodeName)) {
                    return s;
                }
                if (!visited.Contains(s.Node)) {
                    visited.Add(s.Node);
                    foreach (State child in s.children()) {
                        queue.Push(child);
                    }
                }
            }
            return null;
        }
    }
}
