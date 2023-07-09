using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandsAndBridges
{
    [Serializable]
    public partial class GameForm : Form
    {
        public Scene Scene { get; set; }

        public Island FirstClickedIsland;
        public Island LastClickedIsland;

        public Point clickPoint;
        public Point movePoint;

        public bool drawLine;
        public bool hasFirstCheckpoint;
        public bool updateIsland;
        public bool drawBridges;
        public bool cursorTracker;
        public bool firstClick;
        public bool canPlay;

        public DateTime ElapsedTime;
        string formattedTime;

        ErrorProvider error = new ErrorProvider();

        Solutions solution;


        public GameForm()
        { 
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            DoubleBuffered = true;
            drawLine = false;
            hasFirstCheckpoint = false;
            updateIsland = false;
            cursorTracker = true;
            firstClick = true;
            canPlay = true;
            potentialError.Text = string.Empty;

            this.Load += GameForm_Load;
            Invalidate();
        } 


        private void GameForm_Load(object sender, EventArgs e)
        {
            Scene = new Scene(this.Width-17, this.Height-40);
            this.Paint += GameForm_Paint;
        } 

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Scene.Draw(e.Graphics);
            DrawCursorAssister(e.Graphics);
            UpdateVisitedIslands(e.Graphics); 
        } 

        private void UpdateVisitedIslands(Graphics g)
        {
            if (updateIsland)
            {
                FirstClickedIsland.UpdateIslandOutline(g);
                LastClickedIsland.UpdateIslandOutline(g);
            }
        } 
        private void DrawCursorAssister(Graphics g)
        {
            if (drawLine && cursorTracker)
            {
                Pen p = new Pen(Color.Gray, 2);
                p.DashStyle = DashStyle.Dash;
                g.DrawLine(p, FirstClickedIsland.Corner, movePoint);
            }
        } 

        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (canPlay)
            {
                Island currentIsland = Scene.IsCloseToISland(e.Location);
                TimeHandler();
                error.Clear();
                if (e.Button == MouseButtons.Left && currentIsland != null)
                {
                    if (hasFirstCheckpoint && FirstClickedIsland.IsValidNeighbor(currentIsland))
                    {
                        IsIslandFull(currentIsland);
                        if(!Scene.currentGame.BridgeIntersect(FirstClickedIsland, currentIsland))
                        {
                            ConnectIslands(currentIsland);
                        }
                        else
                        {
                            error.SetError(potentialError, "Bridges must not intersect.");
                            drawLine = false;
                            hasFirstCheckpoint = false;
                        }
                        if (Scene.currentGame.GameOver())
                        {
                            Time.Stop();
                            Scene.currentGame.ShowGameOverMessage(formattedTime);
                            canPlay = false;
                        }
                    }
                    else if (hasFirstCheckpoint && !FirstClickedIsland.IsValidNeighbor(currentIsland))
                    {
                        error.SetError(potentialError, "The islands you selected are not valid neighbors!\n" +
                            "Neighbors have to have to be on the same X or Y coordinate.");
                        drawLine = false;
                        hasFirstCheckpoint = false;
                    }
                    else
                    {
                        clickPoint = e.Location;
                        ClickFirstNeighbor(currentIsland);
                    }
                }
                else
                {
                    Scene.currentGame.ClickInTeritory(e.Location);
                    EmptyClick();
                }
                UpdateProgressBar();
            }
            
            Invalidate();
        } 

        private void TimeHandler()
        {
            if (firstClick)
            {
                ElapsedTime = DateTime.Now;
                Time.Start();
                firstClick = false;
            }
        }
        private void UpdateProgressBar()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = Scene.currentGame.totalValue;
            progressBar.Value = Scene.currentGame.currentValue;
        }
        private void EmptyClick()
        {
            hasFirstCheckpoint = false;
            drawLine = false;
            updateIsland = false;
        } 
        private void ClickFirstNeighbor(Island i)
        {
            FirstClickedIsland = i;
            hasFirstCheckpoint = true;
            if (i.Value != 0)
            {
                drawLine = true;
            }
            IsIslandFull(i);
            updateIsland = false;
        } 

        private void IsIslandFull(Island i)
        {
            if(i.Value == 0)
            {
                error = new ErrorProvider();
                error.SetError(potentialError, "The island you selected already has enough bridges.");
            }
        }
        private void ConnectIslands(Island i)
        {
            LastClickedIsland = i;
            if(!Scene.currentGame.NewBridge(FirstClickedIsland, i))
            {
                error.SetError(potentialError, "The islands you're trying to connect are not valid neighbors.");
            }
            hasFirstCheckpoint = false;
            drawLine = false;
            updateIsland = true;
        } 

        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            movePoint = e.Location;
            Invalidate();
        } 

        private void CursorTrackerMenu_Click(object sender, EventArgs e)
        {
            cursorTracker = !cursorTracker;
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - ElapsedTime;
            formattedTime = $"{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";
            TimerLabel.Text = formattedTime;
        }

        private void TipBtn_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int value = random.Next(0, 6);
            switch (value)
            {
                case 0:
                    {
                        MessageBox.Show("Start by building bridges from the islands on the corners.");
                        break;
                    }
                case 1:
                    {
                        MessageBox.Show("Islands that require 7 or 8 neighbors have to be connected with at least 4 other islands.");
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Islands that require 5 or 6 neighbors have to be connected with at least 3 other islands.");
                        break;
                    }
                case 3:
                    {
                        MessageBox.Show("Islands that require 3 or 4 neighbors have to be connected with at least 2 other islands."); 
                        break;
                    }
                case 4:
                    {
                        MessageBox.Show("No hints this time. Try again. :D");
                        break;
                    }
                case 5:
                    {
                        MessageBox.Show("No hints this time. You can do it! :D");
                        break;
                    }
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scene.currentGame.Undo();
            UpdateProgressBar();
            Invalidate();
        }

        private void solutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            solution = new Solutions();
            solution.BackgroundImage = Scene.currentGame.GetSolutionBackground();
            solution.Show();
            solution.ClientSize = new Size(solution.BackgroundImage.Width, solution.BackgroundImage.Height);
        }
    }
}
