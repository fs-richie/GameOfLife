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

        public int GetNumber()
        {
            return (int)runToNumeric.Value;
        }

        public void SetNumber(int number)
        {
            runToNumeric.Value = number;
        }
    }
}
