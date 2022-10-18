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
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        // Get and Set for "Timer Interval in Milliseconds" Option
        public int TimerMS
        {
            get { return (int)timerOptionsNumeric.Value; }
            set { timerOptionsNumeric.Value = value; }
        }
        // Get and Set for "Width of Universe in Cells" Option
        public int UniverseWidth
        {
            get { return (int)cellWidthOptionsNumeric.Value; }
            set { cellWidthOptionsNumeric.Value = value; }
        }
        // Get and Set for "Height of Universe in Cells" Option
        public int UniverseHeight
        {
            get { return (int)cellHeightOptionsNumeric.Value; }
            set { cellHeightOptionsNumeric.Value = value; }
        }
    }
}
