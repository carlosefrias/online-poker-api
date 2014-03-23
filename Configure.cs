using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerAPI
{
    public class Configure : Form
    {
        /// <summary>
        /// Default construtor
        /// </summary>
        public Configure(PictureBox picBox, string filename, int type) 
        {
            this.pictureBox = picBox;
            this.filename = filename;
            this.type = type;

            switch (type)
            {
                case 0:
                    MessageBox.Show("Select the communitary card region");
                    break;
                case 1:
                    MessageBox.Show(messageHoleCards[count]);
                    break;
                case 2:
                    MessageBox.Show(messagePlayerStacks[count]);
                    break;
                default:
                    MessageBox.Show(messageButtons[count]);
                    break;
            }
            pictureBox.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox.MouseUp  += new MouseEventHandler(pictureBox1_MouseUp);
            pictureBox.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox.Paint += new PaintEventHandler(pictureBox1_Paint);

        }
        public PictureBox pictureBox;
        private string filename;
        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private bool selectedRectangle = false;
        private int type;//0 - communitary cards; 1 - hole cards; 2 - stack values;
        private int count = 0;
        private string txt = "";

        private string[] messageButtons = {"Select the fold button region",
                                           "Select the call button region",
                                           "Select the raise button region",
                                           "Select the raise textbox region"};
        private string[] messageHoleCards = { "Select your hole cards region",
                                              "Select player1's hole cards region",
                                              "Select player2's hole cards region",
                                              "Select player3's hole cards region",
                                              "Select player4's hole cards region",
                                              "Select player5's hole cards region",
                                              "Select player6's hole cards region",
                                              "Select player7's hole cards region",
                                              "Select player8's hole cards region",
                                              "Select player9's hole cards region"};
        private string[] messagePlayerStacks = { "Select your stack region",
                                              "Select player1's stack region",
                                              "Select player2's stack region",
                                              "Select player3's stack region",
                                              "Select player4's stack region",
                                              "Select player5's stack region",
                                              "Select player6's stack region",
                                              "Select player7's stack region",
                                              "Select player8's stack region",
                                              "Select player9's stack region"};
        /// <summary>
        /// Method that detects the upper left corner of the selected rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Determine the initial rectangle coordinates...
            RectStartPoint = e.Location;
            Invalidate();
        }

        /// <summary>
        /// Method that drags the selected rectangle over the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point tempEndPoint = e.Location;
            Rect.Location = new Point(
                Math.Min(RectStartPoint.X, tempEndPoint.X),
                Math.Min(RectStartPoint.Y, tempEndPoint.Y));
            Rect.Size = new Size(
                Math.Abs(RectStartPoint.X - tempEndPoint.X),
                Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Method that paints the selected rectangle over the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Draw the rectangle...
            if (pictureBox.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                {
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
        }

        /// <summary>
        /// Method that finalizes the selected rectangle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (type) 
            {
                case 0:
                    if (!selectedRectangle)
                    {
                        txt = "" + Rect.X + " " + Rect.Y + " " + Rect.Size.Width + " " + Rect.Size.Height;
                        saveToFile();
                        this.Close();
                        selectedRectangle = true;
                    }
                    break;
                case 1:
                    txt += "" + Rect.X + " " + Rect.Y + " " + Rect.Size.Width + " " + Rect.Size.Height + "\n";
                    count++;
                    if(count != 9) MessageBox.Show(messageHoleCards[count]);
                    else
                    {
                        txt = txt.Substring(0, txt.Length - 1);
                        saveToFile();
                        this.Close();
                    }
                    break;
                case 2:
                    txt += "" + Rect.X + " " + Rect.Y + " " + Rect.Size.Width + " " + Rect.Size.Height + "\n";
                    count++;
                    if(count !=9) MessageBox.Show(messagePlayerStacks[count]);
                    else
                    {
                        txt = txt.Substring(0, txt.Length - 1);
                        saveToFile();
                        this.Close();
                    }
                    break;
                default:
                    txt += "" + Rect.X + " " + Rect.Y + " " + Rect.Size.Width + " " + Rect.Size.Height + "\n";
                    count++;
                    if(count != 4) MessageBox.Show(messageButtons[count]);
                    else
                    {
                        txt = txt.Substring(0, txt.Length - 1);
                        saveToFile();
                        this.Close();
                    }
                    break;
            }
        }
        /// <summary>
        /// Method that saves into a file the upper left coordinates and dimentions of the rectangles
        /// </summary>
        private void saveToFile() 
        {
            System.IO.File.WriteAllText(filename, txt);
        }
    }
}
