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
    public partial class RunToDialog : Form
    {
        public RunToDialog()
        {
            InitializeComponent();
        }

        // Get and Set for Run To
        public int RunToNum
        {
            get { return (int)runToNumeric.Value; }
            set { runToNumeric.Value = value; }
        }
    }
}
