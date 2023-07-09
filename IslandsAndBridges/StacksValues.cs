using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsAndBridges
{
    [Serializable]
    public class StacksValues
    {
        public bool draw;
        public Neighbors neighbors;

        public StacksValues() { }
        public StacksValues(bool d, Neighbors n) 
        {
            draw = d;
            neighbors = n;
        }

        public void Undo()
        {
            if (draw)
            {
                neighbors.UndoDraw();
            }
            else
            {
                neighbors.UndoDelete();
            }
        }
    }
}
