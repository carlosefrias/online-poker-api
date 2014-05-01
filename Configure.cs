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
                case 3:
                    MessageBox.Show(messageDealerPositions[count]);
                    break;
                case 4:
                    MessageBox.Show(messageButtons[count]);
                    break;
                default:
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
        private int type;//0 - communitary cards; 1 - hole cards; 2 - stack values;
        private int count = 0;
        private string txt = "";

        private string[] messageButtons = {"Select the fold button region",
                                           "Select the call button region",
                                           "Select the raise button region",
                                           "Select the raise textbox region",
                                           "Select the min button region",
                                           "Select the 1/2 button region",
                                           "Select the pot button region",
                                           "Select the máx button region"};
        private string[] messageHoleCards = { "Select your hole cards region",
                                              "Select player1's hole cards region",
                                              "Select player2's hole cards region",
                                              "Select player3's hole cards region",
                                              "Select player4's hole cards region",
                                              "Select player5's hole cards region",
                                              "Select player6's hole cards region",
                                              "Select player7's hole cards region",
                                              "Select player8's hole cards region"};
        private string[] messagePlayerStacks = { "Select your stack region",
                                              "Select player1's stack region",
                                              "Select player2's stack region",
                                              "Select player3's stack region",
                                              "Select player4's stack region",
                                              "Select player5's stack region",
                                              "Select player6's stack region",
                                              "Select player7's stack region",
                                              "Select player8's stack region"};
        private string[] messageDealerPositions = {"Select possible zone for the dealer chip if you're the dealer",
                                                   "Select possible zone for the dealer chip if player 1 is the dealer",
                                                   "Select possible zone for the dealer chip if player 2 is the dealer",
                                                   "Select possible zone for the dealer chip if player 3 is the dealer",
                                                   "Select possible zone for the dealer chip if player 4 is the dealer",
                                                   "Select possible zone for the dealer chip if player 5 is the dealer",
                                                   "Select possible zone for the dealer chip if player 6 is the dealer",
                                                   "Select possible zone for the dealer chip if player 7 is the dealer",
                                                   "Select possible zone for the dealer chip if player 8 is the dealer"};
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
                    txt = "" + Rect.X + " " + Rect.Y + " " + Rect.Size.Width + " " + Rect.Size.Height;
                    saveToFile();
                    this.Close();
                    break;
                case 1:
                    processSelectedRectangle(messageHoleCards);
                    break;
                case 2:
                    processSelectedRectangle(messagePlayerStacks);
                    break;
                case 3:
                    processSelectedRectangle(messageDealerPositions);
                    break;
                default:
                    processSelectedRectangle(messageButtons);
                    break;
            }
        }
        private void processSelectedRectangle(string[] msg) 
        {
            txt += "" + Rect.X + " " + Rect.Y + " " + Rect.Size.Width + " " + Rect.Size.Height + "\n";
            count++;
            if (count != msg.Length) MessageBox.Show(msg[count]);
            else
            {
                txt = txt.Substring(0, txt.Length - 1);
                saveToFile();
                this.Close();
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
