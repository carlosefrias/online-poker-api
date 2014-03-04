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
using Puma.Net;
using Emgu.CV.OCR;
using System.IO;


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
            else imgOriginal = new Image<Bgr, byte>("Captures/img1.png");
            Image<Bgr, byte> img = imgOriginal.Clone();
            
            cd = new CardDetector();
            cd.detectTableCards(img);
            
            ibOriginal.Image = imgOriginal;
            txtMessage.Text = "";
            foreach(Card card in cd.getComunitaryCards())
            {
                txtMessage.AppendText(card.ToString() + "\n");
            }
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

        private void ReadTextButton_Click(object sender, EventArgs e)
        {
            //Saving the image to disk
            //ScreenCapture.CaptureWindowToFile(hWnd, "Captures/img.png", System.Drawing.Imaging.ImageFormat.Png);
            //Reading the text in that image
            
            //string path = "Captures/teste7.jpg";
            /*
            PumaPage inputFile = new PumaPage("C:/Users/Carlos/Documents/Visual Studio 2012/Projects/PokerAPI/PokerAPI/bin/Debug/" + path);
            inputFile.FileFormat = PumaFileFormat.TxtAscii;
            inputFile.Language = PumaLanguage.English;
            string txt = "";
            int potValue = -1;
            try
            {
                txt = inputFile.RecognizeToString();
                Console.WriteLine(txt);
                string[] words = txt.Split(' ', '\n');
                foreach (string word in words) 
                {
                    try
                    {
                        potValue = Int32.Parse(word);
                        Console.WriteLine(potValue);
                    }
                    catch (FormatException) 
                    {
                    }
                }
                txtMessage.Text = "O valor do pote é: " + potValue;
                //txtMessage.Text = txt;
            }
            catch (RecognitionEngineException reqEx) 
            {
                txtMessage.Text = "Não consegue ler texto da imagem\n" + reqEx.Message;
            }
            inputFile.Dispose();
            */
            Bitmap bmp = new Bitmap(@"C:\temp\New Folder\dotnet\eurotext.tif");
            Tesseract ocr = new Tesseract();
            // ocr.SetVariable("tessedit_char_whitelist", "0123456789");
            ocr.Init(@"C:\temp\tessdata", "eng", Tesseract.OcrEngineMode.OEM_CUBE_ONLY);
            // List<tessnet2.Word> r1 = ocr.DoOCR(bmp, new Rectangle(792, 247, 130, 54));
			//List<tessnet2.Word> r1 = ocr.DoOCR(bmp, Rectangle.Empty);
			//int lc = tessnet2.Tesseract.LineCount(r1);
            //Tesseract ocr = new Tesseract("tessdata", "eng", Tesseract.OcrEngineMode.OEM_TESSERACT_ONLY);
            //Image img = Image.FromFile("C:/Users/Carlos/Documents/Visual Studio 2012/Projects/PokerAPI/PokerAPI/bin/Debug/" + path);
            //ocr.Recognize(new Image<Bgr, Byte>(new Bitmap(img)));
            //create OCR
            //_ocr = new Tesseract();
            //You can download more language definition data from
            //http://code.google.com/p/tesseract-ocr/downloads/list
            //Languages supported includes:
            //Dutch, Spanish, German, Italian, French and English
            //_ocr.Init("eng", false);
            //ocr.Recognize(imgOriginal);
            //txtMessage.Text = ocr.GetText();
        }
    }
}
