using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        // Declare universe width
        public static int uWidth;
        // Declare universe height
        public static int uHeight;

        // Declare universe seed (Default is 1337)
        public static int seed = 1337;

        // Declare how many cells are alive (Default is 0)
        public static int aliveCount = 0;
        
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
            toolStripStatusLabelGenerations.Text = "Generations: " + generations.ToString();
            intervalStatusLabel.Text = "Interval: " + timer.Interval.ToString() + "ms";
            aliveStatusLabel.Text = "Alive: " + aliveCount.ToString();
            seedStatusLabel.Text = "Seed: " + seed.ToString();

            // Invalidate
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();

            // Count Alive Cells
            CountAlive();
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

            if (hudToolStripMenuItem.CheckState == CheckState.Checked)
            {
                // Get the height of the entire grid
                float gridHeight = cellHeight * universe.GetLength(1);
                // Set the x coord
                float x = 10;
                // Set the y coord
                float y = gridHeight - (gridHeight / 5);
                // Set the spacing between the strings
                float spacing = (gridHeight / 50) + 5;

                // Set the font for the hud, its size, and have it be bold
                Font hudFont = new Font("Arial", (gridHeight / 50), FontStyle.Bold);

                // Set the formatting
                StringFormat hudFormat = new StringFormat();
                hudFormat.Alignment = StringAlignment.Near;
                hudFormat.LineAlignment = StringAlignment.Near;

                // Draw the Strings for the HUD
                e.Graphics.DrawString("Generations: " + generations.ToString(), hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                y += spacing;
                e.Graphics.DrawString("Alive Cells: " + aliveCount.ToString(), hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                y += spacing;
                if (finiteToolStripMenuItem.CheckState == CheckState.Checked)
                {
                    e.Graphics.DrawString("Boundary Type: Finite", hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                    y += spacing;
                }
                else
                {
                    e.Graphics.DrawString("Boundary Type: Toroidal", hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                    y += spacing;
                }
                e.Graphics.DrawString("Interval: " + timer.Interval.ToString(), hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                y += spacing;
                e.Graphics.DrawString("Universe Size: {Width = " + uWidth.ToString() + ", Height = " + uHeight.ToString() + "}", hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                y += spacing;
                e.Graphics.DrawString("Seed: " + seed.ToString(), hudFont, Brushes.Red, new PointF(x, y), hudFormat);
                y += spacing;
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

                    // If alive, increase count
                    if (universe[xCheck, yCheck] == true) count++;
                }
            }

            // Return the count
            return count;
        }
        
        // Read the Settings
        private void ReadSettings()
        {
            uWidth = Properties.Settings.Default.UniverseWidth; // Set the universe Width
            uHeight = Properties.Settings.Default.UniverseHeight; // Set the universe Height
            universe = new bool[uWidth, uHeight]; // Change the universe size
            scratchPad = new bool[uWidth, uHeight]; // Change the scratchPad size
            timer.Interval = Properties.Settings.Default.TimerMS; // Set the timer interval
            graphicsPanel1.BackColor = Properties.Settings.Default.BackgroundColor; // Set the background color
            deadCellColor = Properties.Settings.Default.DeadCellColor; // Set the dead cell color
            cellColor = Properties.Settings.Default.LivingCellColor; // Set the living cell color
            gridColor = Properties.Settings.Default.GridColor; // Set the Grid color
            gridTColor = Properties.Settings.Default.GridTenColor; // Set the Grid 10x color
            cellbgShareColor = Properties.Settings.Default.ShareColor; // Set whether the background and dead cell share the same color
        }

        // Count Alive Cells
        private void CountAlive()
        {
            // Reset alive count to 0
            aliveCount = 0;

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // If cell is alive, increase count by 1
                    if (universe[x, y] == true)
                    {
                        aliveCount++;
                    }
                }
            }
        }

        // Randomize the Universe
        private void RandomizeUniverse()
        {
            Random rand = new Random(seed);

            // Randomizes universe from Time
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int randInt = rand.Next(0, 3); // Random numbers between 0 and 2

                    // If random number is 0, cell is alive
                    if (randInt == 0)
                    {
                        universe[x, y] = true;
                    }
                    // If random number is not 0, cell is dead
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
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

            // Count Alive Cells
            CountAlive();
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

            // Count Alive Cells
            CountAlive();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            clearToolStripMenuItem_Click(sender, e);

            // Reset generations back to 0
            generations = 0;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();

            // Count Alive Cells
            CountAlive();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();

            // Read the properties from Settings
            ReadSettings();

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

            // Count Alive Cells
            CountAlive();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();

            // Read the properties from Settings
            ReadSettings();

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

            // Count Alive Cells
            CountAlive();
        }

        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeedDialog dlgSeed = new SeedDialog();

            // Sed the Seed
            dlgSeed.Seed = seed;

            if (DialogResult.OK == dlgSeed.ShowDialog())
            {
                // Get the Seed
                seed = dlgSeed.Seed;

                // Start the Randomization process with the new seed
                RandomizeUniverse();
            }

            // Count Alive Cells
            CountAlive();
        }

        private void fromCurrentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Start the Randomization process with current seed
            RandomizeUniverse();

            CountAlive();
        }
        private void fromTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set seed to "current" "time"
            // seed = Environment.TickCount;

            // Start the Randomization process
            Random rand = new Random();

            // Randomizes universe from Time
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int randInt = rand.Next(0, 3); // Random numbers between 0 and 2

                    // If random number is 0, cell is alive
                    if (randInt == 0)
                    {
                        universe[x, y] = true;
                    }
                    // If random number is not 0, cell is dead
                    else
                    {
                        universe[x, y] = false;
                    }
                }
            }

            // Count Alive Cells
            CountAlive();
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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.Filter = "All Files|*.*|Cells|*.cells";
            dlgSave.FilterIndex = 2;
            dlgSave.DefaultExt = "cells";

            if (DialogResult.OK == dlgSave.ShowDialog())
            {
                StreamWriter saveWriter = new StreamWriter(dlgSave.FileName);

                // Iterate through the universe one row at a time
                for (int y = 0; y < uHeight; y++)
                {
                    // Create a string to represent the current row
                    String currentRow = String.Empty;

                    // Iterate through the current row one cell at a time
                    for (int x = 0; x < uWidth; x++)
                    {
                        // If the universe[x, y] is alive then appen 'O' (capital O) to the row string
                        if (universe[x, y] == true)
                        {
                            currentRow += "O";
                        }
                        // Else if the universe[x, y] is dead then append '.' (period) to the row string
                        else
                        {
                            currentRow += ".";
                        }
                    }

                    // Once the current row has been read through and the string constructed then write it to the file using WriteLine
                    saveWriter.WriteLine(currentRow);
                }

                // After all rows and columns have been written then close the file
                saveWriter.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "All Files|*.*|Cells|*.cells";
            dlgOpen.FilterIndex = 2;

            if (DialogResult.OK == dlgOpen.ShowDialog())
            {
                StreamReader openReader = new StreamReader(dlgOpen.FileName);

                // Create a couple variables to calculate the width and height of the data in the file
                int maxWidth = 0;
                int maxHeight = 0;

                // Iterate through the file once to get its size
                while (!openReader.EndOfStream)
                {
                    // Read one row at a time
                    string row = openReader.ReadLine();

                    // If the row begins with '!' then it is a comment and it should be ignored
                    if (row.StartsWith("!")) { }
                    // If the row is not a comment then it is a row of cells
                    // Increment the maxHeight variable for each row read
                    else
                    {
                        maxHeight++;

                        // Get the length of the current row string and adjust the maxWidth variable if necessary
                        if (row.Length >= maxWidth)
                        {
                            maxWidth = row.Length;
                        }
                    }
                }

                // Resize the current universe and scratchPad to the width and height of the file calculated above
                uWidth = maxWidth;
                uHeight = maxHeight;

                universe = new bool[uWidth, uHeight];
                scratchPad = new bool[uWidth, uHeight];

                // Reset the file pointer back to the beginning of the file
                openReader.BaseStream.Seek(0, SeekOrigin.Begin);

                int cellRow = 0;

                // Iterate through the file again, this time reading in the cells
                while (!openReader.EndOfStream)
                {
                    // Read one row at a time
                    string row = openReader.ReadLine();

                    // If the row begins with '!' then it is a comment and should be ignored
                    if (row.StartsWith("!")) { }
                    else
                    {
                        // If the row is not a comment then it is a row of cells and needs to be iterated through
                        for (int xPos = 0; xPos < row.Length; xPos++)
                        {
                            // if row[xPos] is a 'O' (capital O) then set the corresponding cell in the universe to alive
                            if (row[xPos] == 'O')
                            {
                                universe[xPos, cellRow] = true;
                            }
                            // if row[xPos] is a '.' (period) then set the corresponding cell in the universe to dead
                            else if (row[xPos] == '.')
                            {
                                universe[xPos, cellRow] = false;
                            }
                        }
                    }

                    cellRow++;
                }

                // Close the file
                openReader.Close();
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "All Files|*.*|Cells|*.cells";
            dlgOpen.FilterIndex = 2;

            if (DialogResult.OK == dlgOpen.ShowDialog())
            {
                StreamReader openReader = new StreamReader(dlgOpen.FileName);

                // Reset the file pointer back to the beginning of the file
                openReader.BaseStream.Seek(0, SeekOrigin.Begin);

                int cellRow = 0;

                // Iterate through the file again, this time reading in the cells
                while (!openReader.EndOfStream)
                {
                    // Read one row at a time
                    string row = openReader.ReadLine();

                    // If the row begins with '!' then it is a comment and should be ignored
                    if (row.StartsWith("!")) { }
                    else
                    {
                        // If the row is not a comment then it is a row of cells and needs to be iterated through
                        for (int xPos = 0; xPos < row.Length; xPos++)
                        {
                            // if row[xPos] is a 'O' (capital O) then set the corresponding cell in the universe to alive
                            if (row[xPos] == 'O')
                            {
                                universe[xPos, cellRow] = true;
                            }
                            // if row[xPos] is a '.' (period) then set the corresponding cell in the universe to dead
                            else if (row[xPos] == '.')
                            {
                                universe[xPos, cellRow] = false;
                            }
                        }
                    }

                    cellRow++;
                }

                // Close the file
                openReader.Close();
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
