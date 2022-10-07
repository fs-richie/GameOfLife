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
        // The universe array
        bool[,] universe = new bool[40, 40];

        // The scratchPad universe array
        bool[,] scratchPad = new bool[40, 40];

        // Drawing colors
        Color gridColor = Color.LightGray;
        Color gridTColor = Color.Black;
        Color cellColor = Color.LightBlue;

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

            /*
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);
            */

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridTPen = new Pen(gridTColor, 2);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

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

                    /*
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;
                    */

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    
                    /*
                    // CountNeighbor Text
                    Font font = new Font("Arial", (cellRect.Height/2));

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    int neighbors = 0;

                    if (finiteToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        neighbors = CountNeighborsFinite(x, y);
                    }
                    else if (toroidalToolStripMenuItem.CheckState == CheckState.Checked)
                    {
                        neighbors = CountNeighborsTorodial(x, y);
                    }

                    if (neighbors > 0)
                    {
                        e.Graphics.DrawString(neighbors.ToString(), font, Brushes.Black, cellRect, stringFormat);
                    }
                    */
                }
            }

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
                    Font font = new Font("Arial", (cellRect.Height/2));

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

        private void ResetUniverse()
        {
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();

            // Reset generations back to 0
            generations = 0;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
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

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetUniverse();
        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (finiteToolStripMenuItem.CheckState == CheckState.Checked)
            {
                finiteToolStripMenuItem.CheckState = CheckState.Unchecked;
                toroidalToolStripMenuItem.CheckState = CheckState.Checked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toroidalToolStripMenuItem.CheckState == CheckState.Checked)
            {
                toroidalToolStripMenuItem.CheckState = CheckState.Unchecked;
                finiteToolStripMenuItem.CheckState = CheckState.Checked;
            }

            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tell Windows you need to repaint
            graphicsPanel1.Invalidate();
        }
    }
}
