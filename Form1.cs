using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Diagnostics;
using MouseManipulator;
using Emgu.CV.OCR;


namespace PokerAPI
{
    public partial class Testing : Form
    {
        //Member objects
        private bool pokerStars = false;
        private Capture capture = null;
        private Image<Bgr, Byte> imgOriginal;
        private IntPtr hWnd;
        private CardDetector cd;
        //private static int count = 0;
        //private Tesseract _ocr;

        public Testing()
        {
            InitializeComponent();
            hWnd = WinGetHandle("No Limit Hold'em"); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Application.Idle += processFrameAndUpdateGUI;
        }
        private void Form1_FormClosed(Object sender, FormClosedEventArgs e) 
        {
            if (capture != null) 
            {
                capture.Dispose();
            }
        }
        private void processFrameAndUpdateGUI(Object sender, EventArgs args) {
            //TODO: 
            //Bitmap masterImage = (Bitmap) (new ScreenCapture().CaptureScreen());
            //ibOriginal.Image = imgOriginal;
        }
        /// <summary>
        /// Handler for the button click events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseResumeButton_Click(object sender, EventArgs e)
        {
            if (pokerStars)
            {
                //Abrindo a janela de Poker holdem...
                hWnd = WinGetHandle("No Limit Hold'em");
                try
                {
                    //Retrieving the image from screan window
                    Bitmap masterImage = (Bitmap)(ScreenCapture.CaptureWindow(hWnd));
                    imgOriginal = new Image<Bgr, Byte>(masterImage);
                }
                catch (ArgumentException ex)
                {
                    txtMessage.Text = "A janela de PokerStars não está aberta...\n" + ex.Message;
                    pokerStars = false;
                }
            }
            else imgOriginal = new Image<Bgr, byte>("Captures/img8.png");
            Image<Bgr, byte> img = imgOriginal.Clone();
            
            if (img.Size != new Size(1016,728))
            {
                img.Resize(1016, 728, INTER.CV_INTER_CUBIC);
            }            
            cd = new CardDetector();
            cd.detectTableCards(img);
            
            ibOriginal.Image = imgOriginal;
            txtMessage.Text = "";
            foreach(Card card in cd.getComunitaryCards())
            {
                txtMessage.AppendText(card.ToString() + ";");
            }
            txtMessage.AppendText("\nHoleCards: ");
            foreach (Card card in cd.getHoleCards())
            {
                txtMessage.AppendText(card.ToString() + ";");
            }
            cd.readPlayerStacks(img);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadTextButton_Click(object sender, EventArgs e)
        {
            /*
            Tesseract tess = new Tesseract("tessdata", "eng", Tesseract.OcrEngineMode.OEM_DEFAULT);
            Image<Bgr, Byte> image = new Image<Bgr, byte>("Captures/textoPT.png");
            tess.Recognize(image);
            Console.WriteLine(tess.GetText());
            */
        }
        /// <summary>
        /// Function that retrieves the window IntPtr with a given name
        /// </summary>
        /// <param name="wName"></param> Window name
        /// <returns></returns>
        private IntPtr WinGetHandle(string wName)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(wName))
                {
                    hWnd = pList.MainWindowHandle;
                }
            }
            return hWnd;
        }

        private void tableCardsRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = imgOriginal.ToBitmap();
            pictureBox.Size = imgOriginal.Size;
            Configure form = new Configure(pictureBox, "TextFiles/tableCardsRegion.txt", 0);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void holeCardsRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = imgOriginal.ToBitmap();
            pictureBox.Size = imgOriginal.Size;
            Configure form = new Configure(pictureBox, "TextFiles/holeCardsRegion.txt", 1);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }

        private void playersStacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = imgOriginal.ToBitmap();
            pictureBox.Size = imgOriginal.Size;
            Configure form = new Configure(pictureBox, "TextFiles/playersStacks.txt", 2);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }

        private void bottonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = imgOriginal.ToBitmap();
            pictureBox.Size = imgOriginal.Size;
            Configure form = new Configure(pictureBox, "TextFiles/bottons.txt", 3);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }
    }
}
