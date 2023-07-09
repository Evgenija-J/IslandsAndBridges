using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslandsAndBridges
{
    [Serializable]
    public partial class MenuForm : Form
    {
        GameForm gameDisplay;
        
        public MenuForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        } 

        private void EasyBtn_Click(object sender, EventArgs e)
        {
            SetInitialFactors(true, false, false);
            gameDisplay = new GameForm();
            gameDisplay.Show(); 
        } 

        private void MediumBtn_Click(object sender, EventArgs e)
        {
            SetInitialFactors(false, true, false);
            gameDisplay = new GameForm();
            gameDisplay.Show();
        } //DONE

        private void HardBtn_Click(object sender, EventArgs e)
        {
            SetInitialFactors(false, false, true);
            gameDisplay = new GameForm();
            gameDisplay.Show();
        } //DONE

        private void MenuForm_Paint(object sender, PaintEventArgs e)
        {
        } //DONE

        private void SetInitialFactors(bool a, bool b, bool c)
        {
            InitialFactors.easy = a;
            InitialFactors.medium = b;
            InitialFactors.hard = c;
        } //DONE

        private void button1_Click(object sender, EventArgs e)
        {
            ExplainationForm explaination = new ExplainationForm();
            explaination.Show();
        }
    }
}
