﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettlersOfCatan
{
    public partial class GameScreen : Form
    {
        private const int NUM_OF_INTERSECTION_BUTTONS = 54;
        private const int INTERSECTION_BUTTON_SIZE = 30;
        private const int MAX_INTERSECTION_COLUMNS = 11;
        private const int MAX_INTERSECTION_ROWS = 6;

        private const int MAX_ROAD_ROWS = 11;
        private const int MAX_ROAD_COLUMNS = 10;
        private static Size HORIZONTAL_ROAD_SIZE = new Size(30, 10);
        private static Size VERTICAL_ROAD_SIZE = new Size(10, 100);

        private const int MAX_RESOURCE_HEX_ROWS = 5;
        private const int MAX_RESOURCE_HEX_COLUMNS = 5;
        private const int HEX_SIDE_DIMENSION = 150;

        private const int X_INCREMENT = 75;
        private const int Y_INCREMENT = 150;

        // Intersection Grid
        // 7
        // 9
        // 11
        // 11
        // 9
        // 7

        private List<List<Button>> intersectionGrid = new List<List<Button>>();
        private List<PictureBox> waterHexes = new List<PictureBox>();
        private List<List<ResourceHex>> hexGrid = new List<List<ResourceHex>>();
        private Button[,] roadGrid = new Button[MAX_ROAD_ROWS,MAX_ROAD_COLUMNS];
        private Panel boardPanel = new Panel();
        private Panel buttonPanel = new Panel();
        //private List<System.Windows.Forms.Button> intersectionButtons = new List<System.Windows.Forms.Button>();

        /** Utility function to determine if a number is even */
        private bool isEven(int n) { return (n % 2 == 0); }

        public GameScreen()
        {

            InitializeComponent();
            initializeAll();

            initializeBoardAndButtonPanels();
        }

        /** initializeAll()
         * 
         * Initializes all private fields with empty structures to give them
         * the correct dimensions
         */
        private void initializeAll()
        {
            initializeWaterHexes();
            initializeIntersectionGrid();
            initializeHexGrid();
            initializeRoadGrid();
        }

        /** initializeIntersectionGrid()
         * 
         * Initializes the private field intersectionGrid to hold a double array
         * made of Lists containing Buttons
         */
        private void initializeIntersectionGrid()
        {
            for (int r = 0; r < MAX_INTERSECTION_ROWS; r++)
            {
                List<Button> l = new List<Button>(MAX_INTERSECTION_COLUMNS);
                for (int c = 0; c < MAX_INTERSECTION_COLUMNS; c++)
                {
                    l.Add(new Button());
                }
                intersectionGrid.Add(l);
            }
        }

        /** initializeWaterHexes()
         * 
         * Initializes the waterHexes private field to prepare it for holding
         * all the water hexes that are generated 
         */
        private void initializeWaterHexes()
        {
            for (int i = 0; i < 18; i++)
            {
                waterHexes.Add(new PictureBox());
            }
        }

        /** initializeHexGrid()
         * 
         * Initializes the private field hexGrid just like the intersection buttons
         * to prepare them for actually holding the randomized hexes
         */
        private void initializeHexGrid()
        {
            for (int r = 0; r < MAX_RESOURCE_HEX_ROWS; r++)
            {
                List<ResourceHex> l = new List<ResourceHex>(MAX_INTERSECTION_COLUMNS);
                for (int c = 0; c < MAX_RESOURCE_HEX_COLUMNS; c++)
                {
                    l.Add(new ResourceHex());
                }
                hexGrid.Add(l);
            }
        }

        /** initializeRoadGrid()
         * 
         * Initializes the private fiels roadGrid so that it can be filled
         * with the road buttons
         */
        private void initializeRoadGrid()
        {
            for (int r = 0; r < MAX_ROAD_ROWS; r++)
            {
                for (int c = 0; c < MAX_ROAD_COLUMNS; c++)
                {
                    roadGrid[r, c] = null;
                }
            }
        }

        private void setupRoadGrid()
        {
            /*
            // Coordinate variables for plotting buttons in correct locations
            int x = 150 - INTERSECTION_BUTTON_SIZE / 2;
            int y = 75 - INTERSECTION_BUTTON_SIZE / 2;

            for (int i = 0; i < NUM_OF_INTERSECTION_BUTTONS; i++)
            {

                for (int r = 0; r < MAX_INTERSECTION_ROWS; r++)
                {
                    for (int c = 0; c < MAX_INTERSECTION_COLUMNS; c++)
                    {
                        Button b = new Button();
                        b.BackColor = Color.White;
                        b.Font = new Font("Georgia", 20, FontStyle.Bold | FontStyle.Strikeout);
                        b.Size = new Size(INTERSECTION_BUTTON_SIZE, INTERSECTION_BUTTON_SIZE);
                        b.Location = new Point(x, y);
                        b.Click += intersectionButton_Click;
                        if ((r == 0 || r == 5) && (c < 2 || c > 8))
                        {
                            b = null;
                        }
                        else if ((r == 1 || r == 4) && (c < 1 || c > 9))
                        {
                            b = null;
                        }
                        intersectionGrid[r][c] = b;
                        boardPanel.Controls.Add(b);
                        x += X_INCREMENT;
                    }
                    x = 150 - INTERSECTION_BUTTON_SIZE / 2;
                    y += Y_INCREMENT;
                }
            }
             * */


            int x = 315;
            int y = 70;
            int x_diff = 55;
            int y_diff = 150;

            int columnMax = 6;
            // the horizontal rows
            for (int r = 0; r < MAX_ROAD_ROWS; r+=2)
            {
                for (int c = 0; c < columnMax; c++)
                {
                    Button b = new Button();
                    b.Size = HORIZONTAL_ROAD_SIZE;
                    b.Location = new Point(x, y);
                    b.BackColor = Color.White;
                    buttonPanel.Controls.Add(b);
                    
                    x += x_diff;
                }
                if (r < 4) columnMax += 2;
                else if (r >= 6) columnMax -= 2;

                x -= 85;
                y += y_diff;
            }

            columnMax = 4;
            for (int r = 1; r < MAX_ROAD_ROWS; r += 2)
            {
                for (int c = 0; c < columnMax; c++)
                {
                    Button b = new Button();
                    b.Size = VERTICAL_ROAD_SIZE;


                }
                columnMax = (r < 5) ? columnMax + 1 : columnMax - 1;
            }





        }

        private void setupHexGrid()
        {
            // Coordinate variables for plotting buttons in correct locations
            int x = HEX_SIDE_DIMENSION * 2;
            int y = HEX_SIDE_DIMENSION / 2;
            
                for (int c = 0; c < MAX_RESOURCE_HEX_COLUMNS; c++)
                {
                    for (int r = 0; r < MAX_RESOURCE_HEX_ROWS; r++)
                    {
                        ResourceHex h = new ResourceHex();
                        h.Location = new Point(x, y);
                        if ((r == 0 || r == 4) && (c > 2))
                        {
                            h = null;
                        }
                        else if ((r == 1 || r == 3) && (c > 3))
                        {
                            h = null;
                        }
                        hexGrid[r][c] = h;
                        boardPanel.Controls.Add(h);
                        x = (r < 2) ? x - HEX_SIDE_DIMENSION / 2: x + HEX_SIDE_DIMENSION / 2;
                        y += HEX_SIDE_DIMENSION;
                    }
                    y = HEX_SIDE_DIMENSION / 2;
                    x = HEX_SIDE_DIMENSION * (3 + c);
                }
        }

        private void setupWaterHexes()
        {
            int waterCount = 0;
            int x = 225;
            int x_diff = 150;
            int y = 0;
            int y_diff = 600;

            // Set up top and bottom (short hexes)
            for (int i = 0; i < 4; i++)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Blue;
                pb.Size = new Size(150, 75);
                pb.Location = new Point(x, y);
                waterHexes[waterCount] = pb;
                waterCount++;
                boardPanel.Controls.Add(pb);
               
                PictureBox pb2 = new PictureBox();
                pb2.BackColor = Color.Blue;
                pb2.Size = new Size(150, 75);
                pb2.Location = new Point(x, y + 825);
                waterHexes[waterCount] = pb2;
                waterCount++;
                boardPanel.Controls.Add(pb2);
                x += x_diff;
            }

            // Set up the water hexes on the left
            x = 150;
            x_diff = 75;
            y = 75;
            y_diff = 600;
            for (int i = 0; i < 3; i++)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Blue;
                pb.Size = new Size(150, 150);
                pb.Location = new Point(x, y);
                waterHexes[waterCount] = pb;
                waterCount++;
                boardPanel.Controls.Add(pb);

                if (i == 2) break;

                PictureBox pb2 = new PictureBox();
                pb2.BackColor = Color.Blue;
                pb2.Size = new Size(150, 150);
                pb2.Location = new Point(x, y + y_diff);
                waterHexes[waterCount] = pb2;
                waterCount++;
                boardPanel.Controls.Add(pb2);

                x -= x_diff;
                y += 150;
                y_diff -= 150 * (i+2);
            }

            // Set up the water hexes on the right
            x = 750;
            x_diff = 75;
            y = 75;
            y_diff = 600;
            for (int i = 0; i < 3; i++)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Blue;
                pb.Size = new Size(150, 150);
                pb.Location = new Point(x, y);
                waterHexes[waterCount] = pb;
                waterCount++;
                boardPanel.Controls.Add(pb);

                if (i == 2) break;

                PictureBox pb2 = new PictureBox();
                pb2.BackColor = Color.Blue;
                pb2.Size = new Size(150, 150);
                pb2.Location = new Point(x, y + y_diff);
                waterHexes[waterCount] = pb2;
                waterCount++;
                boardPanel.Controls.Add(pb2);

                x += x_diff;
                y += 150;
                y_diff -= 150 * (i + 2);
            }
        }

        private void initializeBoardAndButtonPanels()
        {
            boardPanel.Location = new Point(0, 0);
            boardPanel.Size = new Size(1050, 1050);

            setupIntersectionButtons();

            for (int i = 0; i < MAX_INTERSECTION_ROWS; i++)
            {
                for (int j = 0; j < MAX_INTERSECTION_COLUMNS; j++)
                {
                    boardPanel.Controls.Add(intersectionGrid[i][j]);
                }
            }

            setupWaterHexes();
            setupHexGrid();

            buttonPanel.Location = new Point(0, 0);
            buttonPanel.Size = new Size(1050, 1050);
            buttonPanel.BackColor = Color.FromArgb(0, 100, 100, 100);
            setupRoadGrid();

            buttonPanel.BringToFront();
            this.Controls.Add(buttonPanel);
            this.Controls.Add(boardPanel);
            
        }

        private void setupIntersectionButtons(){

            // Coordinate variables for plotting buttons in correct locations
            int x = 150 - INTERSECTION_BUTTON_SIZE/2;
            int y = 75 - INTERSECTION_BUTTON_SIZE/2;

            for(int i = 0; i < NUM_OF_INTERSECTION_BUTTONS; i++){

                for (int r = 0; r < MAX_INTERSECTION_ROWS; r++)
                {
                    for (int c = 0; c < MAX_INTERSECTION_COLUMNS; c++)
                    {
                        Button b = new Button();
                        b.BackColor = Color.White;
                        b.Font = new Font("Georgia", 20, FontStyle.Bold | FontStyle.Strikeout);
                        b.Size = new Size(INTERSECTION_BUTTON_SIZE, INTERSECTION_BUTTON_SIZE);
                        b.Location = new Point(x, y);
                        b.Click += intersectionButton_Click;
                        if ((r == 0 || r == 5) && (c < 2 || c > 8))
                        {
                            b = null;
                        }
                        else if ((r == 1 || r == 4) && (c < 1 || c > 9))
                        {
                            b = null;
                        }
                        intersectionGrid[r][c] = b;
                        boardPanel.Controls.Add(b);
                        x += X_INCREMENT;
                    }
                    x = 150 - INTERSECTION_BUTTON_SIZE/2;
                    y += Y_INCREMENT;
                }
            }
        }

        private void ItemToBuildComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox myComboBox = (ComboBox)sender;
            if (myComboBox.SelectedIndex == 0)
            {
                // Show all available road buttons
             
            }
            else if (myComboBox.SelectedIndex == 1)
            {
                // Show all available settlement buttons
            }
            else if (myComboBox.SelectedIndex == 2)
            {
                // enable all relevant current settlement buttons to be changed into cities
            }
        }

        private void showOnlyOpenRoadButtons()
        {
            // Disable all city/settlement buttons

            // Hide all unused city/settlement buttons

            // Show and enable all unused road buttons

        }


        private void intersectionButton_Click(object sender, EventArgs e)
        {
            Button theButton = (Button)sender;


            if (theButton.Width == 30 && theButton.BackColor != System.Drawing.Color.White)
            {
                theButton.Text = "*";
                theButton.ForeColor = Color.White;
                theButton.Enabled = false;
            }

            theButton.BackColor = Color.Orange;
        }

        //private void AddControls(int cNumber) 
        //{ 
            
        //}

        //}


    }
}
