using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandsAndBridges
{
    [Serializable]
    public class Scene
    {
        public float width;
        public float height;
        public GameModes currentGame;

        public Scene(float width, float height)
        {
            this.width = width;
            this.height = height;

            InitialFactors.X = width / 16;
            InitialFactors.Y = height / 11;

            currentGame = new GameModes();
        } 

        public void Draw(Graphics g)
        {
            DrawGrid(g);
            currentGame.DrawGame(g);
        }

        public void DrawGrid(Graphics g)
        {
            Pen pen = new Pen(Color.Gray, 1);
            pen.DashStyle = DashStyle.Dash;
            float FACTOR_X = InitialFactors.X;
            float FACTOR_Y = InitialFactors.Y;
            for (int i = 0; i < 16; i++)
            {
                g.DrawLine(pen, FACTOR_X, 0, FACTOR_X, height);
                FACTOR_X += InitialFactors.X;
            }
            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(pen, 0, FACTOR_Y, width, FACTOR_Y);
                FACTOR_Y += InitialFactors.Y;
            }
        }

        public Island IsCloseToISland(Point location)
        {
            return currentGame.IsInIslandArea(location);
        }

    }
}
