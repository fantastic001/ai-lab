using System;
using System.Collections.Generic;
using System.Text;

namespace Lavirint
{
    public class Node
    {
        public int markI, markJ;

        public Node(int markI, int markJ)
        {
            this.markI = markI;
            this.markJ = markJ;
        }

        private bool validCoords(int markI, int markJ)
        {

            return markI >= 0 && markI < Main.lavirint.brojVrsta && markJ >= 0 && markJ < Main.lavirint.brojKolona &&
                Main.lavirint.polja[markI, markJ] != 1
                && Main.lavirint.polja[markI, markJ] != 5; 
        }

        public List<Node> getLinkedNodes()
        {
            // TODO 1: Implementirati metodu tako da odredjuje dozvoljeno kretanje u lavirintu.
            List<Node> nextNodes = new List<Node>();
            int[] markis = { -1, 1, 0, 0 };
            int[] markjs = { 0, 0, 1, -1 };
            for (int i = 0; i < 4; i++) {
                if (validCoords(markI + markis[i], markJ + markjs[i])) {
                    nextNodes.Add(new Node(markI + markis[i], markJ + markjs[i]));
                }
            }
            return nextNodes;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Node node = (Node)obj;
            return this.markI == node.markI && this.markJ == node.markJ;
        }

        public override int GetHashCode()
        {
            return 100 * this.markI + this.markJ;
        }
    }
}
