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
        public List<Node> kutijePlave = new List<Node>();
        public List<Node> kutijeNarandzaste = new List<Node>();
        public static int brojPlavih = 3;
        public static int brojNarandzastih = 2;

        public State sledeceStanje(Node node)
        {
            State rez = new State();
            rez.node = node;
            rez.parent = this;
            rez.cost = this.cost + 1;
            rez.level = this.level + 1;
            rez.kutijePlave = new List<Node>(kutijePlave);
            rez.kutijeNarandzaste = new List<Node>(kutijeNarandzaste);
            if (Main.lavirint.polja[node.markI, node.markJ] == 4) {
                if (!rez.kutijePlave.Contains(node))
                {
                    //Main.lavirint.polja[node.markI, node.markJ] = 0;
                    rez.kutijePlave.Add(node);
                }
            }
            if (Main.lavirint.polja[node.markI, node.markJ] == 5) {
                if (!rez.kutijeNarandzaste.Contains(node)) {
                    rez.kutijeNarandzaste.Add(node);
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
                // ako je broj plavih manji od zeljenog broja, ne mozemo ici na narandzaste kutije
                if (kutijePlave.Count < brojPlavih && Main.lavirint.polja[node.markI, node.markJ] == 5) {
                    continue; 
                }
                rez.Add(next);
            }
            
            return rez;
        }

        public bool isKrajnjeStanje()
        {
            return Main.krajnjiNode.Equals(this.node) && kutijePlave.Count == brojPlavih &&
                kutijeNarandzaste.Count == brojNarandzastih;
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
            int code =  10 * this.node.markI + node.markJ;
            
            for (int i = 0; i < brojPlavih; i++) {
                code *= 1000;
                if (i >= kutijePlave.Count) code += 99;
                else code += kutijePlave[i].markI * 10 + kutijePlave[i].markJ;
            }
            for (int i = 0; i < brojNarandzastih; i++)
            {
                code *= 1000; 
                if (i >= kutijeNarandzaste.Count) code += 99;
                else code += kutijeNarandzaste[i].markI * 10 + kutijeNarandzaste[i].markJ;
            }
            return code; 
        }

        public override bool Equals(object obj)
        {
            State other = (State)obj;
            return other.node.markI == node.markI && other.node.markJ == node.markJ &&
                other.kutijeNarandzaste.Count == kutijeNarandzaste.Count
                && other.kutijePlave.Count == kutijePlave.Count; 
        }
        public void PrintPath() {
            List<State> p = path();
            foreach (State s in p) {
                Console.Write(s.node.markI + "-" + s.node.markJ);
                Console.Write(" -> ");
            }
            Console.WriteLine();
        }
    }
   
}
