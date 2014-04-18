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
using System.Threading;

namespace PokerAPI
{
    public partial class Testing : Form
    {
        //Member objects
        private bool pokerStars = true;
        private Capture capture = null;
        private Image<Bgr, Byte> imgOriginal;
        private IntPtr hWnd;
        private CardDetector cd;

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
            //Capturing the poker table image
            if (pokerStars)
            {
                //Abrindo a janela de Poker holdem...
                hWnd = WinGetHandle("No Limit Hold'em");
                try
                {
                    //Retrieving the image from screan window
                    Bitmap masterImage = (Bitmap)(ScreenCapture.CaptureWindow(hWnd));
                    //imgOriginal = new Image<Bgr, Byte>(masterImage);
                    ibOriginal.Image = new Image<Bgr, Byte>(masterImage);
                    //newImage = new Image<Bgr, Byte>(masterImage);
                }
                catch (ArgumentException ex)
                {
                    txtMessage.Text = "A janela de PokerStars não está aberta...\n" + ex.Message;
                    pokerStars = false;
                }
            }
            ibOriginal.Image = imgOriginal;
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
            else imgOriginal = new Image<Bgr, byte>("Captures/img5.png");
            Image<Bgr, byte> img = imgOriginal.Clone();
            if (img.Size != new Size(1016,728))
            {
                img = img.Resize(1016, 728, INTER.CV_INTER_CUBIC);
            }
            cd = new CardDetector();
            cd.detectTableCards(img);
            Console.WriteLine(cd.findDealer(img));
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
            //Console.WriteLine("Dealer: " + cd.findDealer(img));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadTextButton_Click(object sender, EventArgs e)
        {
            MCvBox2D[] a = CardDetector.buttonsRegions;
            //System.Windows.Forms.Cursor.Position = new Point((int) a[1].center.X, (int) a[1].center.Y);
            //(231,743)
            //(280,660)
            VirtualMouse1 vr = new VirtualMouse1();
            vr.MoveToBezierCurve((int)a[1].center.X, (int)a[1].center.Y);
            vr.doubleClick();

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
            Configure form = new Configure(pictureBox, "TextFiles/buttons.txt", 3);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }

        private void captureAll_Click(object sender, EventArgs e)
        {
            //new Thread(new ThreadStart(this.threadMethod)).Start();
            int estado = 0;
            //0: pre-flop
            //1: flop
            //2: turn
            //3: river
            int jogador = 0;
            int dealer = 0;
            bool[] jogou = new bool[9];

            CardDetector cd = new CardDetector();
            int n = 0;
            int[] previousStacks = new int[9];
            Player[] players = new Player[9];
            while (true) 
            {
                Image<Bgr, Byte> img = captureImage();
                cd.readPlayerStacks(img);
                if (n == 0) 
                {
                    for (int i = 0; i < cd.playerStacks.Length; i++) 
                    {
                        previousStacks[i] = cd.playerStacks[i];
                        players[i] = new Player(i, cd.playerStacks[i]);
                    }
                    cd.detectTableCards(img);
                    cd.printComunitaryCards();
                    int cards = cd.getComunitaryCards().ToArray().Length;
                    switch(cards)
                    {
                        case 0: 
                            estado = 0;
                            break;
                        case 3:
                            estado = 1;
                            break;
                        case 4:
                            estado = 2;
                            break;
                        case 5:
                            estado = 3;
                            break;
                        default:
                            estado = 0;
                            break;
                    }
                    printState(estado);
                }

                for (int i = 0; i < cd.playerStacks.Length; i++) 
                {
                    if (previousStacks[i] > cd.playerStacks[i] && (previousStacks[i] - cd.playerStacks[i]) <= players[i].getStack()) 
                    {
                        Console.WriteLine("O jogador " + i + " apostou " + (previousStacks[i] - cd.playerStacks[i]));
                        jogador = i;
                        players[i].raise(previousStacks[i] - cd.playerStacks[i]);
                        for (int j = 0; j <= i; j++) jogou[j] = true;
                    }
                    else if (previousStacks[i] < cd.playerStacks[i])
                    {
                        Console.WriteLine("O jogador " + i + " tem agora " + cd.playerStacks[i]);
                        players[i].setStack(cd.playerStacks[i]);
                    }
                }
                if (novaRonda(jogou))
                {
                    Console.WriteLine("nova ronda");
                    printPlayerStacks(players);
                    cd.detectTableCards(img);
                    cd.printComunitaryCards();
                    int cards = cd.getComunitaryCards().ToArray().Length;
                    switch (cards)
                    {
                        case 0:
                            estado = 0;
                            int p = cd.findDealer(img);
                            if (p != -1) 
                            {
                                dealer = p;
                                foreach (Player player in players) 
                                {
                                    if (player.getNumber() == p) player.setDealer(true);
                                    else player.setDealer(false);
                                }
                                Console.WriteLine("Dealer: " + p);
                            }
                            break;
                        case 3:
                            estado = 1;
                            break;
                        case 4:
                            estado = 2;
                            break;
                        case 5:
                            estado = 3;
                            break;
                        default:
                            estado = 0;
                            break;
                    }
                    printState(estado);
                }
                Thread.Sleep(1000);
                img.Dispose();
                for (int i = 0; i < cd.playerStacks.Length; i++)
                {
                    previousStacks[i] = cd.playerStacks[i];
                }
                n++;
            }

        }

        private Image<Bgr, Byte> captureImage() 
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>(new Size(1016,728));
            //Oppening the Poker holdem window...
            hWnd = WinGetHandle("No Limit Hold'em");
            try
            {
                //Retrieving the image from screan window
                Bitmap masterImage = (Bitmap)(ScreenCapture.CaptureWindow(hWnd));
                img = new Image<Bgr, Byte>(masterImage);
            }
            catch (ArgumentException ex)
            {
                txtMessage.Text = "A janela de PokerStars não está aberta...\n" + ex.Message;
                pokerStars = false;
            }
            return img;
        }
        private bool novaRonda(bool[] jogou) 
        {
            bool resp = true;
            foreach (bool j in jogou) 
            {
                if (!j) resp = false;
            }
            return resp;
        }

        private void dealerChipPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = imgOriginal.ToBitmap();
            pictureBox.Size = imgOriginal.Size;
            Configure form = new Configure(pictureBox, "TextFiles/dealerPositions.txt", 3);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }
        private void printPlayerStacks(Player[] players) 
        {
            Console.Write("{");
            for(int i = 0; i < players.Length - 1; i++)
            {
                Console.Write("" + players[i].getStack() + ",");
            }
            Console.Write("" + players[players.Length - 1].getStack() + "}\n");
        }
        private void printState(int state) 
        {
            switch (state) 
            {
                case 0:
                    Console.WriteLine("Estado: Pre-flop");
                    break;
                case 1:
                    Console.WriteLine("Estado: Flop");
                    break;
                case 2:
                    Console.WriteLine("Estado: Turn");
                    break;
                case 3:
                    Console.WriteLine("Estado: River");
                    break;
                default:
                    break;
            }
        }
        private void jogou(int dealer, int
    }
}
