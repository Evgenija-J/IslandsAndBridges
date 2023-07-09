using IslandsAndBridges.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace IslandsAndBridges
{
    [Serializable]
    public class GameModes
    {
        public List<Island> GameIslands;
        public List<Neighbors> GameNeighbors;

        public Stack<StacksValues> MainStack = new Stack<StacksValues>();

        Random Rand = new Random();

        public int counter = 0;
        public int totalValue = 0;
        public int currentValue = 0;
        
        public int[] CurrentGame;

        public int[] Easy1 =
       {
            2,1,2,6,1,5,10,1,3,14,1,2,
            7,2,1,9,2,4,13,2,4,15,2,3,
            1,3,3,6,3,6,
            3,4,2,5,4,3,10,4,3,12,4,3,
            1,5,4,4,5,4,
            10,6,1,15,6,4,
            2,7,1,4,7,3,7,7,3,9,7,5,12,7,2,
            1,8,3,5,8,3,8,8,1,10,8,3,13,8,3,
            2,9,1,6,9,4,15,9,3
        };

        public int[] Easy2 =
        {
            2,1,3,4,1,4,6,1,6,10,1,6,15,1,4,
            1,2,2,3,2,1,5,2,1,
            2,3,3,7,3,1,10,3,6,14,3,3,
            6,4,3,9,4,1,
            1,5,4,5,5,3,10,5,4,13,5,4,
            3,6,3,6,6,3,12,6,1,
            4,7,1,7,7,2,13,7,4,
            2,8,1,5,8,1,10,8,2,14,8,3,
            1,9,3,3,9,4,6,9,5,15,9,4
        };

        public int[] Medium1 =
        {
            1,1,2,3,1,4,5,1,1,7,1,3,15,1,3,
            4,3,2,7,3,4,11,3,3,14,3,2,
            1,4,1,3,4,7,12,4,5,15,4,5,
            4,5,2,7,5,4,9,5,3,
            1,6,1,3,6,3,8,6,1,10,6,2,12,6,5,
            2,8,2,7,8,4,9,8,4,12,8,3,
            1,9,2,3,9,2,15,9,2
        };

        public int[] Medium2 =
        {
            1,1,3,3,1,4,7,1,5,13,1,2,
            4,2,3,6,2,3,8,2,3,15,2,3,
            1,3,2,9,3,1,11,3,3,14,3,1,
            4,4,2,7,4,3,
            1,5,2,3,5,6,6,5,5,12,5,3,15,5,5,
            4,6,1,8,6,3,11,6,3,
            3,7,2,7,7,2,10,7,1,
            9,8,2,12,8,3,
            1,9,4,4,9,4,6,9,4,15,9,4
        };

        public int[] Hard1 =
        {
           2,1,2,4,1,2,6,1,4,14,1,2, 
           2,3,4,4,3,5,6,3,6,10,3,3,13,3,4,15,3,4,
           3,4,1,
           9,5,1,11,5,1,13,5,3,15,5,3,
           4,7,2,15,7,1,
           3,8,2,6,8,4,9,8,2,11,8,3,13,8,4,
           2,9,3,4,9,3,10,9,5,15,9,2
        };

        public int[] Hard2 =
        {
            1,1,3,6,1,4,9,1,1,11,1,3,13,1,5,15,1,2,
            2,3,1,4,3,2,6,3,4,10,3,1,
            1,4,3,13,4,6,15,4,3,
            2,6,3,4,6,3,9,6,5,12,6,2,15,6,3,
            2,8,2,5,8,2,7,8,3,9,8,5,12,8,2,
            1,9,1,4,9,3,6,9,4,13,9,4,15,9,2
        };

        public GameModes()
        {
            if (InitialFactors.easy)
            {
                counter = Rand.Next(1, 3);
            }
            else if (InitialFactors.medium)
            {
                counter = Rand.Next(3, 5);
            }
            else
            {
                counter = Rand.Next(5, 7);
            }
            SetGame();
        } 
        public void DrawGame(Graphics g)
        {
            foreach (Neighbors n in GameNeighbors)
            {
                n.DrawBridges(g);
            }
            foreach (Island i in GameIslands)
            {
                i.DrawIsland(g);
            }
        } 

        public void SetGame()
        {
            GamePicker();
            GameArrayToList();
            SetNeighbors();
        } 

        private void GamePicker()
        {
            switch (counter)
            {
                case 1: CurrentGame = Easy1; break;
                case 2: CurrentGame = Easy2; break;
                case 3: CurrentGame = Medium1; break;
                case 4: CurrentGame = Medium2; break;
                case 5: CurrentGame = Hard1; break;
                case 6: CurrentGame = Hard2; break;
            }
        } 
        private void GameArrayToList()
        {
            GameIslands = new List<Island>();
            Island island;
            Point point = new Point();
            for (int i = 0; i < CurrentGame.Length; i += 3)
            {
                point.X = (int)(InitialFactors.X * CurrentGame[i]);
                point.Y = (int)(InitialFactors.Y * CurrentGame[i + 1]);
                island = new Island(point, CurrentGame[i + 2]);
                totalValue += CurrentGame[i + 2];
                GameIslands.Add(island);
            }
        } 
        public void SetNeighbors()
        {
            GameNeighbors = new List<Neighbors>();
            bool neighborsExist = false;
            foreach (Island i in GameIslands)
            {
                foreach (Island j in GameIslands)
                {
                    neighborsExist = false;
                    if (i.IsValidNeighbor(j))
                    {
                        Neighbors currentNeighbors = new Neighbors(i, j);
                        Neighbors prev = new Neighbors(j, i);
                        foreach (Neighbors n in GameNeighbors)
                        {
                            if (n.Equals(prev))
                            {
                                neighborsExist = true;
                                break;
                            }
                        }
                        if (!neighborsExist && CloseNeighbors(currentNeighbors.Neighbor1, currentNeighbors.Neighbor2))
                        {
                            GameNeighbors.Add(currentNeighbors);
                        }
                    }
                }
            }
        } 

        public Island IsInIslandArea(Point location)
        {
            Island currentIsland = null;
            foreach (Island i in GameIslands)
            {
                currentIsland = i.VisitIsland(location);
                if (currentIsland != null)
                {
                    break;
                }
            }
            return currentIsland;
        } 

        public bool NewBridge(Island a, Island b)
        {
            foreach (Neighbors n in GameNeighbors)
            {
                if (n.AreNeighbors(a, b) && (a.Value != 0 && b.Value != 0))
                {
                    if(a.Value != 0 && b.Value != 0)
                        currentValue += 2;
                    n.NewBridge();
                    StacksValues s = new StacksValues(true, n);
                    MainStack.Push(s);
                    return true;
                }
            }
            return false;
        } 

        public int ClickInTeritory(Point Click)
        {
            foreach(Neighbors n in GameNeighbors)
            {
                int val = n.ClickInTeritory(Click);
                currentValue -= val;
                StacksValues s = new StacksValues(false, n);
                if (val != 0)
                {
                    MainStack.Push(s);
                }
            }
            return currentValue;
        }

        public bool GameOver()
        {
            foreach(Island i in GameIslands)
            {
                if(i.Value != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void ShowGameOverMessage(string time)
        {
            MessageBox.Show("CONGRATULATIONS! You have won the game! :D\n" +
                "Your time is: " + time);
        }

        public bool CloseNeighbors(Island a, Island b)
        {
            foreach (Island i in GameIslands)
            {
                if (a.Corner.X == i.Corner.X && i.Corner.X == b.Corner.X)
                {
                    if ((a.Corner.Y > i.Corner.Y && b.Corner.Y < i.Corner.Y) ||
                        (a.Corner.Y < i.Corner.Y && b.Corner.Y > i.Corner.Y))
                    {
                        return false;
                    }
                }
                else if (a.Corner.Y == i.Corner.Y && i.Corner.Y == b.Corner.Y)
                {
                    if ((a.Corner.X > i.Corner.X && b.Corner.X < i.Corner.X) ||
                        (a.Corner.X < i.Corner.X && b.Corner.X > i.Corner.X))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsVertical(Island a, Island b)
        {
            if (a.Corner.X == b.Corner.X)//Islands are vertical
            {
                return true;
            }
            return false;
        }

        private bool IsHorizontal(Island a, Island b)
        {
            if (a.Corner.Y == b.Corner.Y) //Islands are horizontal
            {
                return true;
            }
            return false;
        }

        public bool BridgeIntersect(Island a, Island b)
        {
            foreach(Neighbors n in GameNeighbors)
            {
                if (n.bridgeCount != 0 && Intersection(a, b, n.Neighbor1, n.Neighbor2))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Intersection(Island verticalA, Island verticalB, Island horizontalA, Island horizontalB)
        {
            if((IsHorizontal(verticalA, verticalB) && IsHorizontal(horizontalA, horizontalB)) ||
                (IsVertical(verticalA, verticalB) && IsVertical(horizontalA, horizontalB)))
            {
                return false;
            }
            else if(IsHorizontal(verticalA, verticalB))
            {
                Island tmp = verticalA;
                verticalA = horizontalA;
                horizontalA = tmp;

                tmp = verticalB;
                verticalB = horizontalB;
                horizontalB = tmp;
            }

            int X = verticalA.Corner.X;
            int top = Math.Max(verticalA.Corner.Y, verticalB.Corner.Y);
            int bottom = Math.Min(verticalA.Corner.Y, verticalB.Corner.Y);

            int Y = horizontalA.Corner.Y;
            int right = Math.Max(horizontalA.Corner.X, horizontalB.Corner.X);
            int left = Math.Min(horizontalA.Corner.X, horizontalB.Corner.X);

            if (top > Y && Y > bottom && left < X && X < right)
            {
                return true;
            }
            else return false;
        }

        public void Undo()
        {
            if(MainStack.Count != 0)
            {
                StacksValues SV = MainStack.Pop();
                Neighbors n = SV.neighbors;
                SV.Undo();
                if (SV.draw)
                {
                    currentValue -= 2;
                }
                else
                {
                    currentValue += 2;
                }
            }   
        }

        public Image GetSolutionBackground()
        {
            System.Resources.ResourceManager RM = new System.Resources.ResourceManager
                ("YourAppliacationNameSpace.Properties.Resources", typeof(Resources).Assembly);
            switch (counter)
            {
                case 1: return Resources.EasyMode1; 
                case 2: return Resources.EasyMode2; 
                case 3: return Resources.MediumMode1;
                case 4: return Resources.MediumMode2;
                case 5: return Resources.HardMode1;
                case 6: return Resources.HardMode2;
            }
            return Resources.EasyMode1;
        }
    }
}
