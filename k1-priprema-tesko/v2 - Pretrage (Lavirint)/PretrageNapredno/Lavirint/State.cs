using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class State
    {
        public State parent;
        public Node node;
        public double cost;
        public int level;
        public List<Node> kutije = new List<Node>();

        public State sledeceStanje(Node node)
        {
            State rez = new State();
            rez.node = node;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;
            rez.kutije = new List<Node>(kutije);
            if (Main.lavirint.polja[node.markI, node.markJ] == 4) {
                if (!rez.kutije.Contains(node))
                {
                    //Main.lavirint.polja[node.markI, node.markJ] = 0;
                    rez.kutije.Add(node);
                }
            }
            return rez;
        }


        public List<State> mogucaSledecaStanja()
        {
            List<State> rez = new List<State>();
            foreach (Node nextNode in this.node.getLinkedNodes())
            {
                State next = sledeceStanje(nextNode);
                //bool found = false; 
                //foreach (State prev in path()) {
                //    found = found || prev.node.Equals(next.node);
                //}
                //if (!found) 
                rez.Add(next);
            }
            return rez;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjiNode.Equals(this.node) && kutije.Count == 2;
        }

        public List<State> path()
        {
            List<State> putanja = new List<State>();
            State tt = this;
            while (tt != null)
            {
                putanja.Insert(0, tt);
                tt = tt.parent;
            }
            return putanja;
        }

        public bool cirkularnaPutanja()
        {
            // TODO 3: proveriti da li trenutno stanje odgovara poziciji koja je vec vidjena u grani pretrazivanja
            List<State> grana = this.path();
            int i;
            int c = 0;
            for (i = 0; i < grana.Count; i++) {
                if (grana[i].node.Equals(this.node)) c++;
            }
            return c > 1;        
        }

        public override int GetHashCode()
        {
            int code =  100 * this.node.markI + node.markJ;
            for (int i = 0; i < 2; i++) {
                code *= 10000;
                if (i < kutije.Count)
                {
                    code += node.GetHashCode();
                }
                else {
                    code += 9999;
                }
            }
            return code; 
        }

        public override bool Equals(object obj)
        {
            State other = (State)obj;
            return other.node.markI == node.markI && other.node.markJ == node.markJ && 
                other.kutije.Count == kutije.Count;
        }
        public void PrintPath() {
            List<State> p = path();
            foreach (State s in p) {
                Console.Write(s.node.markI + "-" + s.node.markJ + "-" + s.kutije.Count);
                Console.Write(" -> ");
            }
            Console.WriteLine();
        }
    }
   
}
