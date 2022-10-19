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
    public partial class Form1 : Form
    {
        public static int uWidth;
        public static int uHeight;
        
        // The universe array
        // bool[,] universe = new bool[20, 20];
        bool[,] universe;

        // The scratchPad universe array
        // bool[,] scratchPad = new bool[20, 20];
        bool[,] scratchPad;

        // Drawing colors
        Color gridColor;
        Color gridTColor;
        Color deadCellColor;
        Color cellColor;

        // Bool to see if background and dead cells share the same color
        bool cellbgShareColor;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running

            // Setup with Settings (Reading)
            ReadSettings();
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            // CountNeightbors Implementation
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count = 0;
                    
                    if (finiteToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        // int count = CountNeighbor;
                        count = CountNeighborsFinite(x, y);
                    }
                    else if (toroidalToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        // int count = CountNeighbor;
                        count = CountNeighborsFinite(x, y);
                    }

                    // Apply the rules
                    // Check to see if cell is still alive or not
                    bool breathing = false;
                    if (universe[x, y] == true)
                    {
                        breathing = true;
                    }

                    // Turn in on/off the scratchPad
                    if (breathing == true)
                    {
                        // Any living cell with less than 2 living neighbors dies in the next generation
                        if (count < 2)
                        {
                            scratchPad[x, y] = false;
                        }
                        // Any living cell with 2 or 3 living neighbors will live on into the next generation
                        else if (count == 2 || count == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                        // Any living cell with more than 3 living neighbors will die in the next generation
                        else if (count > 3)
                        {
                            scratchPad[x, y] = false;
                        }
                    }
                    else
                    {
                        // Any dead cell with exactly 3 living neighbors will be born into the next generation
                        if (count == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                        // Otherwise, stay dead
                        else
                        {
                            scratchPad[x, y] = false;
                        }
                    }
                }
            }

            // Copy from scratchPad to universe & clear out previous copy
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;

            // Increment generation count
            generations++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            // Invalidate
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            // FLOATS - Turn ints into floats
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Pen for drawing the 10x10 grid lines (color, width)
            Pen gridTPen = new Pen(gridTColor, 2);

            // A Brush for filling the background (color)
            Brush bgBrush = new SolidBrush(deadCellColor);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Fill Living Cells
            for (int y = 0; y < universe.GetLength(1); y++)                     // Iterate through the universe in the y, top to bottom
            {
                for (int x = 0; x < universe.GetLength(0); x++)                 // Iterate through the universe in the x, left to right
                {
                    // 'RectangleF' - Float ver. of the integer Rectangle
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Checks to see if "Background and Dead Cells shar the same color" option is checked or not
                    // If they don't
                    if (cellbgShareColor == false)
                    {
                        // Fill the cell with a brush if alive
                        if (universe[x, y] == true)
                        {
                            e.Graphics.FillRectangle(cellBrush, cellRect);
                        }
                        // Fill the cell with a brush if dead (background)
                        else
                        {
                            e.Graphics.FillRectangle(bgBrush, cellRect);
                        }
                    }
                    // If they do
                    else
                    {
                        // Fill the cell with a brush if alive
                        if (universe[x, y] == true)
                        {
                            e.Graphics.FillRectangle(cellBrush, cellRect);
                        }
                    }
                }
            }

            // Set Show/Hide Status if grid toolstrip menu item is checked or not
            if (gridToolStripMenuItem.CheckState == CheckState.Checked)
            {
                // Draw Grid
                for (int y = 0; y < universe.GetLength(1); y++)                     // Iterate through the universe in the y, top to bottom
                {
                    for (int x = 0; x < universe.GetLength(0); x++)                 // Iterate through the universe in the x, left to right
                    {
                        float horX = x * cellWidth; // Vertical Line Width
                        float horY = cellHeight * universe.GetLength(1); // Height
                        float vertY = y * cellHeight; // Horizontal Line Height
                        float vertX = cellWidth * universe.GetLength(0); // Width

                        // Draw Vertical Line
                        e.Graphics.DrawLine(gridPen, horX, 0, horX, horY);
                        // Draw Horizontal Line
                        e.Graphics.DrawLine(gridPen, 0, vertY, vertX, vertY);
                    }
                }
            }

            // Set Show/Hide Status if grid 10x toolstrip menu item is checked or not
            if (gridTenToolStripMenuItem.CheckState == CheckState.Checked)
            {
                // Draw 10x10 Grid
                for (int y = 0; y < universe.GetLength(1) + 1; y++)                 // Iterate through the universe in the y, top to bottom
                {
                    for (int x = 0; x < universe.GetLength(0) + 1; x++)             // Iterate through the universe in the x, left to right
                    {
                        float horX = x * cellWidth; // Vertical Line Width
                        float horY = cellHeight * universe.GetLength(1); // Height
                        float vertY = y * cellHeight; // Horizontal Line Height
                        float vertX = cellWidth * universe.GetLength(0); // Width

                        // Draw 10x10 Vertical Line
                        if ((x % 10) == 0)
                        {
                            e.Graphics.DrawLine(gridTPen, horX, 0, horX, horY);
                        }
                        // Draw 10x10 Horizontal Line
                        if ((y % 10) == 0)
                        {
                            e.Graphics.DrawLine(gridTPen, 0, vertY, vertX, vertY);
                        }
                    }
                }
            }

            // Set Show/Hide Status if neighbor coount toolstrip menu item is checked or not
            if (neighborCountToolStripMenuItem.CheckState == CheckState.Checked)
            {
                // CountNeighbor Text
                // Iterate through the universe in the y, top to bottom
                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    // Iterate through the universe in the x, left to right
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        // 'RectangleF' - Float ver. of the integer Rectangle
                        // A rectangle to represent each cell in pixels
                        RectangleF cellRect = Rectangle.Empty;
                        cellRect.X = x * cellWidth;
                        cellRect.Y = y * cellHeight;
                        cellRect.Width = cellWidth;
                        cellRect.Height = cellHeight;

                        // CountNeighbor Font and Size
                        Font font = new Font("Arial", (cellRect.Height / 2));

                        // CountNeighbor Alignment
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        int neighbors = 0;

                        // Determines if NeighborCount is Finite or Torodial
                        if (finiteToolStripMenuItem.CheckState == CheckState.Checked)
                        {
                            neighbors = CountNeighborsFinite(x, y);
                        }
                        else if (toroidalToolStripMenuItem.CheckState == CheckState.Checked)
                        {
                            neighbors = CountNeighborsTorodial(x, y);
                        }

                        // Draws the NeighborCount into the cell as long as the count is greater than 0
                        if (neighbors > 0)
                        {
                            // If count is 2 or 3, text's color is Green
                            if (neighbors == 2 || neighbors == 3)
                            {
                                e.Graphics.DrawString(neighbors.ToString(), font, Brushes.ForestGreen, cellRect, stringFormat);
                            }
                            // Otherwise, text's color is Red
                            else
                            {
                                e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Red, cellRect, stringFormat);
                            }
                        }
                    }
                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }

        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // TODO: Prevent crashing when clicking the small whitespace between the end of grid and window edge
                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        // CountNeightbor - Finite
        private int CountNeighborsFinite(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0) continue;

                    // if xCheck is less than 0 then continue
                    if (xCheck < 0) continue;
                    // if yCheck is less than 0 then continue
                    if (yCheck < 0) continue;
                    // if xCheck is greater than or equal to xLen then continue
                    if (xCheck >= xLen) continue;
                    // if yCheck is greater than or equal to yLen then continue
                    if (yCheck >= yLen) continue;

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }

            return count;
        }

        private int CountNeighborsTorodial(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0) continue;

                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0) xCheck = xLen - 1;
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0) yCheck = yLen - 1;
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen) xCheck = 0;
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen) yCheck = 0;

                    if (universe[xCheck, yCheck] == true) count++;
                }
            }

            return count;
        }

        private void ReadSettings()
        {
            uWidth = Properties.Settings.Default.UniverseWidth;
            uHeight = Properties.Settings.Default.UniverseHeight;
            universe = new bool[uWidth, uHeight];
            scratchPad = new bool[uWidth, uHeight];
            timer.Interval = Properties.Settings.Default.TimerMS;
            graphicsPanel1.BackColor = Properties.Settings.Default.BackgroundColor;
            deadCellColor = Properties.Settings.Default.DeadCellColor;
            cellColor = Properties.Settings.Default.LivingCellColor;
            gridColor = Properties.Settings.Default.GridColor;
            gridTColor = Properties.Settings.Default.GridTenColor;
            cellbgShareColor = Properties.Settings.Default.ShareColor;
        }

        // Close Program when clicking on Exit under File
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Stops timer
            timer.Stop();

            // Clears/Empties current universe
            universe = new bool[uWidth, uHeight];
            scratchPad = new bool[uWidth, uHeight];

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            clearToolStripMenuItem_Click(sender, e);

            // Reset generations back to 0
            generations = 0;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            // Read the properties from Settings
            ReadSettings();

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            // Read the properties from Settings
            ReadSettings();

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fromCurrentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ensures that if Toroidal is checked, finite will be unchecked in the menus
            if (finiteToolStripMenuItem.CheckState == CheckState.Checked)
            {
                finiteToolStripMenuItem.CheckState = CheckState.Unchecked;
                finiteToolStripMenuItem1.CheckState = CheckState.Unchecked;
                toroidalToolStripMenuItem.CheckState = CheckState.Checked;
                toroidalToolStripMenuItem1.CheckState = CheckState.Checked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ensures that if Finite is checked, toroidal will be unchecked in the menus
            if (toroidalToolStripMenuItem.CheckState == CheckState.Checked)
            {
                toroidalToolStripMenuItem.CheckState = CheckState.Unchecked;
                toroidalToolStripMenuItem1.CheckState = CheckState.Unchecked;
                finiteToolStripMenuItem.CheckState = CheckState.Checked;
                finiteToolStripMenuItem1.CheckState = CheckState.Checked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            // Display About Box
            aboutBox.Show();
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgBG = new ColorDialog();

            // Set the Color for background (dead cell)
            dlgBG.Color = graphicsPanel1.BackColor;

            if (DialogResult.OK == dlgBG.ShowDialog())
            {
                // Get the Color for background (dead cell)
                graphicsPanel1.BackColor = dlgBG.Color;
            }
        }

        private void deadCellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgDeadCell = new ColorDialog();

            // Set the Color for dead cell
            dlgDeadCell.Color = deadCellColor;

            if (DialogResult.OK == dlgDeadCell.ShowDialog())
            {
                // Get the Color for background (dead cell)
                deadCellColor = dlgDeadCell.Color;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgCell = new ColorDialog();
            
            // Set the Color for Living Cell
            dlgCell.Color = cellColor;

            if (DialogResult.OK == dlgCell.ShowDialog())
            {
                // Get the Color for Living Cell
                cellColor = dlgCell.Color;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgGrid = new ColorDialog();
            
            // Set the Color for Grid
            dlgGrid.Color = gridColor;

            if (DialogResult.OK == dlgGrid.ShowDialog())
            {
                // Get the Color for Grid
                gridColor = dlgGrid.Color;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void gridX10ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dlgGridT = new ColorDialog();
            
            // Set the Color for 10x Grid
            dlgGridT.Color = gridTColor;

            if (DialogResult.OK == dlgGridT.ShowDialog())
            {
                // Get the Color for 10x Grid
                gridTColor = dlgGridT.Color;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (neighborCountToolStripMenuItem.CheckState == CheckState.Checked)
            {
                neighborCountToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                neighborCountToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (gridToolStripMenuItem.CheckState == CheckState.Checked)
            {
                gridToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                gridToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void gridTenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (gridTenToolStripMenuItem.CheckState == CheckState.Checked)
            {
                gridTenToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                gridTenToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void hudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (hudToolStripMenuItem.CheckState == CheckState.Checked)
            {
                hudToolStripMenuItem1.CheckState = CheckState.Checked;
            }
            else
            {
                hudToolStripMenuItem1.CheckState = CheckState.Unchecked;
            }
            
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void neighborCountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (neighborCountToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                neighborCountToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                neighborCountToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
            
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void gridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (gridToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                gridToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                gridToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void grid10xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (gridTenToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                gridTenToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                gridTenToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void hudToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Make sure both the Menu Strip and Context Menu's share the same check state
            if (hudToolStripMenuItem1.CheckState == CheckState.Checked)
            {
                hudToolStripMenuItem.CheckState = CheckState.Checked;
            }
            else
            {
                hudToolStripMenuItem.CheckState = CheckState.Unchecked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void toToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunToDialog dlgRunTo = new RunToDialog();

            // Set the properties for the Run To Generation
            dlgRunTo.RunToNum = generations;

            // If user clicks on the Accept Button, trigger the following:
            if (DialogResult.OK == dlgRunTo.ShowDialog())
            {
                // Get the properties from the Run To Generation
                int genTarget = dlgRunTo.RunToNum;

                // Generate until the Run To Generation Target
                do
                {
                    // Go to next generation
                    NextGeneration();

                    // Tell Windows you need to repaint
                    graphicsPanel1.Invalidate();
                }
                while (generations < genTarget);

            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsDialog dlgOptions = new OptionsDialog();

            // Set the properties for the Options
            dlgOptions.TimerMS = timer.Interval;
            dlgOptions.UniverseWidth = uWidth;
            dlgOptions.UniverseHeight = uHeight;
            dlgOptions.ColorShare = cellbgShareColor;

            // If user clicks on the Accept Button, trigger the following:
            if (DialogResult.OK == dlgOptions.ShowDialog())
            {
                // Get the timer properties from the Options
                timer.Interval = dlgOptions.TimerMS;

                // Get the universe width and height properties from the Options
                uWidth = dlgOptions.UniverseWidth;
                uHeight = dlgOptions.UniverseHeight;

                // Create the new universe with the new width and height
                universe = new bool[uWidth, uHeight];
                scratchPad = new bool[uWidth, uHeight];

                // Get the Color Share properties form the Options
                cellbgShareColor = dlgOptions.ColorShare;

                // Disables/Enables the Dead Cell Color Menu Item based on whether or not the option is checked or not
                deadCellColorToolStripMenuItem.Enabled = !dlgOptions.ColorShare;
                deadCellColorToolStripMenuItem1.Enabled = !dlgOptions.ColorShare;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Update Settings when applicatication is closing
            Properties.Settings.Default.UniverseWidth = uWidth;
            Properties.Settings.Default.UniverseHeight = uHeight;
            Properties.Settings.Default.TimerMS = timer.Interval;
            Properties.Settings.Default.BackgroundColor = graphicsPanel1.BackColor;
            Properties.Settings.Default.DeadCellColor = deadCellColor;
            Properties.Settings.Default.LivingCellColor = cellColor;
            Properties.Settings.Default.GridColor = gridColor;
            Properties.Settings.Default.GridTenColor = gridTColor;
            Properties.Settings.Default.ShareColor = cellbgShareColor;

            Properties.Settings.Default.Save();
        }
    }
}
