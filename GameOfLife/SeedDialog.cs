using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class SeedDialog : Form
    {
        public SeedDialog()
        {
            InitializeComponent();
        }
        private void seedRandomizeButton_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            int randSeed = rand.Next();

            seedNumeric.Value = randSeed;
        }
        public int Seed
        {
            get { return (int)seedNumeric.Value; }
            set { seedNumeric.Value = value; }
        }
    }
}
