using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lavirint
{
    class AStarSearch
    {
        private State findMin(List<State> elements) {
            State res = elements[0];
            foreach (State elem in elements) {
                if (this.heuristicFunction(res) > this.heuristicFunction(elem)) {
                    res = elem;
                }
            }
            return res;
        }
        public State search(State pocetnoStanje)
        {
            // TODO 5.1: Implementirati algoritam vodjene pretrage A*
            List<State> stanjaNaObradi = new List<State>();
            stanjaNaObradi.Add(pocetnoStanje);
            while (stanjaNaObradi.Count > 0)
            {
                State naObradi = findMin(stanjaNaObradi);

                if (!naObradi.cirkularnaPutanja())
                {
                    Main.allSearchStates.Add(naObradi);
                    if (naObradi.isKrajnjeStanje())
                    {
                        return naObradi;
                    }
                    List<State> mogucaSledecaStanja = naObradi.mogucaSledecaStanja();
                    stanjaNaObradi.AddRange(mogucaSledecaStanja);
                }
                stanjaNaObradi.Remove(naObradi);
            }
            return null;
        }

        
        public double heuristicFunction(State s)
        {
            // TODO 5.2: Implementirati heuristicku funkciju (funkcija odredjuje rastojanje)
            double c = s.cost;
            return s.cost + Math.Abs(Main.krajnjiNode.markI - s.node.markI)
                + Math.Abs(Main.krajnjiNode.markJ - s.node.markJ);
        }
    }
}
