using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsAndBridges
{
    [Serializable]
    public class Island
    {
        public Point Center;
        public Point Corner;

        public float Radius { get; set; } = Math.Min(InitialFactors.X / 2, InitialFactors.Y / 2);
        public int Value { get; set; }

        public Island(Point point, int value)
        {
            this.Corner = point;
            Corner.Y = (int)(Corner.Y + (2 * Radius ));
            this.Center.X = (int)(point.X - Radius);
            this.Center.Y = (int)(point.Y + Radius);
            this.Value = value;
        } 

        public void DrawIsland(Graphics g)
        {
            Brush b = new SolidBrush(Color.LightGray);
            g.FillEllipse(b, Center.X, Center.Y, Radius * 2, Radius * 2);
            b.Dispose();
            UpdateIslandOutline(g);
        } 

        public void UpdateIslandOutline(Graphics g)
        {
            Pen p = new Pen(Color.Green, 3);
            switch (Value)
            {
                case 0:
                    {
                        Brush b = new SolidBrush(Color.Green);
                        g.FillEllipse(b, Center.X, Center.Y, Radius * 2, Radius * 2);
                        break;
                    }
                case 1:
                    {
                        p.Color = Color.Yellow;
                        break;
                    }
                case 2:
                    {
                        p.Color = Color.Orange;
                        break;
                    }
                case 3:
                    {
                        p.Color = Color.Red;
                        break;
                    }
                case 4:
                    {
                        p.Color = Color.Purple;
                        break;
                    }
                case 5:
                    {
                        p.Color = Color.DarkBlue;
                        break;
                    }
                case 6:
                    {
                        p.Color = Color.LightBlue;
                        break;
                    }
                case 7:
                    {
                        p.Color = Color.Brown;
                        break;
                    }
                case 8:
                    {
                        p.Color = Color.Black;
                        break;
                    }
                default:
                    {
                        Brush b = new SolidBrush(Color.Black);
                        g.FillEllipse(b, Center.X, Center.Y, Radius * 2, Radius * 2);
                        break;
                    }
            }
            g.DrawEllipse(p, Center.X, Center.Y, Radius * 2, Radius * 2);
            p.Dispose();
        } 

        public Island VisitIsland(Point location)
        {
            if (Math.Sqrt(Math.Pow(location.X - (Center.X + Radius), 2) + Math.Pow(location.Y - (Center.Y + Radius), 2)) <= Radius)
            {
                return this;
            }
            return null;
        } 

        public bool IsValidNeighbor(Island currentIsland)
        {
            if ((this.Corner.X == currentIsland.Corner.X || this.Corner.Y == currentIsland.Corner.Y) &&
                    !currentIsland.Equals(this))
            {
                return true;
            }
            else return false;
        } 
    }
}
