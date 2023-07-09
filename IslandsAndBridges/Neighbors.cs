using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IslandsAndBridges
{
    [Serializable]
    public class Neighbors
    {
        public Island Neighbor1;
        public Island Neighbor2;

        public int bridgeCount;

        public int Xleft;
        public int Xright;
        public int Ylower;
        public int Yupper;

        public Rectangle SharedTeritory;
        
        Pen pen = new Pen(Color.SaddleBrown, 2);

        public Neighbors() { }

        public Neighbors(Island one, Island two)
        {
            Neighbor1 = one;
            Neighbor2 = two;
            bridgeCount = 0;
            SetNeighborsTeritory();
        }

        public void DrawBridges(Graphics g)
        {
            if (bridgeCount > 0)
            {
                g.DrawLine(pen, Neighbor1.Corner.X + 7, Neighbor1.Corner.Y + 7, Neighbor2.Corner.X + 7, Neighbor2.Corner.Y + 7);
            }
            if (bridgeCount > 1)
            {
                g.DrawLine(pen, Neighbor1.Corner.X - 5, Neighbor1.Corner.Y - 5, Neighbor2.Corner.X - 5, Neighbor2.Corner.Y - 5);
            }
        }

        public void DeleteBridges()
        {
            UpdateIslandValue();
            bridgeCount = 0;
        }

        public bool AreNeighbors(Island one, Island two)
        {
            return ((one.Equals(Neighbor1) || one.Equals(Neighbor2)) && (two.Equals(Neighbor1) || two.Equals(Neighbor2)));
        }

        public void NewBridge()
        {
            if(Neighbor1.Value != 0 && Neighbor2.Value != 0)
            {
                bridgeCount++;
                Neighbor1.Value--;
                Neighbor2.Value--;
            }
        }

        public void SetNeighborsTeritory()
        {
            Point Corner = new Point();
            Corner.X = Math.Min(Neighbor1.Corner.X - 9, Neighbor2.Corner.X - 9);
            Corner.Y = Math.Min(Neighbor1.Corner.Y - 9, Neighbor2.Corner.Y - 9);

            int height = Math.Abs(Neighbor1.Corner.Y - Neighbor2.Corner.Y) + 20;
            int width = Math.Abs(Neighbor1.Corner.X - Neighbor2.Corner.X) + 20;

            Size size = new Size(width, height);
            SharedTeritory = new Rectangle(Corner, size); 
        }

        public int ClickInTeritory(Point Click)
        {
            if(SharedTeritory.Left < Click.X && SharedTeritory.Right > Click.X &&
                SharedTeritory.Top < Click.Y && SharedTeritory.Bottom > Click.Y)
            {
                int n = bridgeCount;
                DeleteBridges();
                return n*2;
            }
            else return 0;
        }

        public void UpdateIslandValue()
        {
            Neighbor1.Value += bridgeCount;
            Neighbor2.Value += bridgeCount;
        }

        public void UndoDraw()
        {
            bridgeCount--;
            Neighbor1.Value++;
            Neighbor2.Value++;
        }

        public void UndoDelete()
        {
            bridgeCount++;
            Neighbor1.Value--;
            Neighbor2.Value--;
        }

    }
}
