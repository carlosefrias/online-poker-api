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


namespace PokerAPI
{
    public partial class PokerAPIWindow : Form
    {
        //Member objects
        private Capture capture = null;
        private Image<Bgr, Byte> imgOriginal;
        private int count = 0;

        public PokerAPIWindow()
        {
            InitializeComponent();
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
            IntPtr hWnd = WinGetHandle("No Limit Hold'em");
            try
            {
                Bitmap masterImage = (Bitmap)(ScreenCapture.CaptureWindow(hWnd));
                ScreenCapture.CaptureWindowToFile(hWnd, "Captures/img" + (++count) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                imgOriginal = new Image<Bgr, Byte>(masterImage);
                ibOriginal.Image = imgOriginal;
                txtMessage.Text = "";
            }
            catch (ArgumentException ex) {
                txtMessage.Text = "A janela de PokerStars não está aberta...\n" + ex.Message;
            } 
            //System.Windows.Forms.Cursor.Position = new Point(100, 100);
            //Thread.Sleep(interval);
            //VirtualMouse1.Move(0 , 0);
            //VirtualMouse1.LeftClick();
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
    }
}
