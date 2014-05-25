using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MouseManipulator
{
    public class VirtualMouse1
    {
        [DllImport("user32.dll")]
        extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        private const int MOUSEEVENTF_MOVE = 0x0001, MOUSEEVENTF_LEFTDOWN = 0x0002, MOUSEEVENTF_LEFTUP = 0x0004, MOUSEEVENTF_RIGHTDOWN = 0x0008, 
            MOUSEEVENTF_RIGHTUP = 0x0010, MOUSEEVENTF_MIDDLEDOWN = 0x0020, MOUSEEVENTF_MIDDLEUP = 0x0040, MOUSEEVENTF_ABSOLUTE = 0x8000;
        private Point position;
        private Rectangle[] buttons = new Rectangle[8];
        private Point TopLeftCorner;
        private RECT pRect;
        /*
         * 0 - Fold button
         * 1 - Call button
         * 2 - Raise button
         * 3 - Raise textfield
         * 4 - Min raise button
         * 5 - 1/2 raise button
         * 6 - Pot raise button
         * 7 - Max(all in) raise button
         */
        /// <summary>
        /// Default constructor for the VirtualMouse class
        /// </summary>
        public VirtualMouse1(IntPtr hWnd) 
        {
            position = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            //Get coordinates relative to window
            GetWindowRect(hWnd, out pRect);
            TopLeftCorner = new Point(pRect.Left, pRect.Top);
            loadButtonPositions();
        }
        public VirtualMouse1() 
        {
            TopLeftCorner = new Point(0, 0);
            loadButtonPositions();
        }
        public void fold() 
        {
            pressButton(0);
        }
        public void call() 
        {
            pressButton(1);
        }
        public void raise() 
        {
            pressButton(2);
        }
        public void raise(int value)
        {
            //Thread.Sleep(3000);
            Rectangle textField = buttons[3];
            RandomNumbers rn = new RandomNumbers((int)DateTime.Now.Ticks);
            int x = (int)rn.NextDouble(textField.X, textField.X + textField.Width);
            int y = (int)rn.NextDouble(textField.Y, textField.Y + textField.Height);
            MoveToBezierCurve(x, y);
            Thread.Sleep(200);
            doubleClick();
            Thread.Sleep(300);
            SendKeys.Send("" + value);
            raise();
        }
        public void raiseMin() 
        {
            pressButton(4);
            Thread.Sleep(100);
            raise();
        }
        public void raiseOneHalf() 
        {
            pressButton(5);
            Thread.Sleep(100);
            raise();
        }
        public void raisePot() 
        {
            pressButton(6);
            Thread.Sleep(100);
            raise();
        }
        public void allIn() 
        {
            pressButton(7);
            Thread.Sleep(100);
            raise();
        }
        private void pressButton(int i) 
        {
            //Thread.Sleep(3000);
            Rectangle button = buttons[i];
            RandomNumbers rn = new RandomNumbers((int)DateTime.Now.Ticks);
            int x = (int)rn.NextDouble(button.X, button.X + button.Width);
            int y = (int)rn.NextDouble(button.Y, button.Y + button.Height);
            MoveToBezierCurve(x, y);
            LeftClick();           
        }
        public void addChips(bool addChips) 
        {
            if (addChips)
            {
                Rectangle rect = new Rectangle(364, 392, 167, 14);
                RandomNumbers rn = new RandomNumbers((int)DateTime.Now.Ticks);
                int x = (int)rn.NextDouble(rect.X, rect.X + rect.Width);
                int y = (int)rn.NextDouble(rect.Y, rect.Y + rect.Height);
                MoveToBezierCurve(x, y);
                LeftClick();
            }
        }
        public void moveToRamdomPlace()
        {
            RandomNumbers rn = new RandomNumbers((int)DateTime.Now.Ticks);
            int x = (int) rn.NextDouble(pRect.Left, pRect.Right);
            int y = (int) rn.NextDouble(pRect.Bottom, pRect.Top);
            Thread.Sleep((int)(rn.NextDouble(0.0, 1.0) * 1000));
            MoveToBezierCurve(x, y);
        }
        /// <summary>
        /// Method that loads from disk the position of all the buttons
        /// </summary>
        private void loadButtonPositions()
        {
            try
            {
                using (StreamReader sr = new StreamReader("TextFiles/buttons.txt"))
                {
                    string line;
                    for (int i = 0; i < 8; i++)
                    {
                        line = sr.ReadLine();
                        string[] words = line.Split(' ');
                        buttons[i] = new Rectangle(int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]), int.Parse(words[3]));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Method that moves the mouse acording to de vector (xDelta, yDelta)
        /// </summary>
        /// <param name="xDelta"></param>
        /// <param name="yDelta"></param>
        public void Move(int xDelta, int yDelta)
        {
            position.X += xDelta;
            position.Y += yDelta;
            System.Windows.Forms.Cursor.Position = position;
        }
        /// <summary>
        /// Method that moves the mouse to a specific spot (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveTo(int x, int y)
        {
            x += TopLeftCorner.X;
            y += TopLeftCorner.Y;
            System.Windows.Forms.Cursor.Position = new Point(x, y);
            position = new Point(x, y);
        }
        /// <summary>
        /// Method that iteratively moves the mouse traveling a Bezier path with some noise
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveToBezierCurve(int x, int y) 
        {
            x += TopLeftCorner.X;
            y += TopLeftCorner.Y;
            Point finalPoint = new Point(x, y);
            Point initialPoint = position;
            Point[] points;
            double dist = distance(initialPoint, finalPoint);
            int speedFactor;
            if (dist < 80) 
            {
                points = new Point[2];
                points[0] = initialPoint;
                points[1] = finalPoint;
                speedFactor = 8;
            }
            else if (dist < 200)
            {
                points = new Point[3];
                points[0] = initialPoint;
                points[1] = randowPoint(initialPoint, finalPoint);
                points[2] = finalPoint;
                speedFactor = 4;
            }
            else if (dist < 400)
            {
                Point middlePoint = new Point((int)(0.5 * initialPoint.X + 0.5 * finalPoint.X),
                                              (int)(0.5 * initialPoint.Y + 0.5 * finalPoint.Y));
                points = new Point[4];
                points[0] = initialPoint;
                points[1] = randowPoint(initialPoint, middlePoint);
                points[2] = randowPoint(middlePoint, finalPoint);
                points[3] = finalPoint;
                speedFactor = 2;
            }
            else
            {
                Point oneThird = new Point((int)((1.0 / 3.0) * initialPoint.X + (1.0 / 3.0) * finalPoint.X),
                                           (int)((1.0 / 3.0) * initialPoint.Y + (1.0 / 3.0) * finalPoint.Y));
                Point twoThirds = new Point((int)((2.0 / 3.0) * initialPoint.X + (2.0 / 3.0) * finalPoint.X),
                                           (int)((2.0 / 3.0) * initialPoint.Y + (2.0 / 3.0) * finalPoint.Y));
                points = new Point[5];
                points[0] = initialPoint;
                points[1] = randowPoint(initialPoint, oneThird);
                points[2] = randowPoint(oneThird, twoThirds);
                points[3] = randowPoint(twoThirds, finalPoint);
                points[4] = finalPoint;
                speedFactor = 1;
            }
            for (double t = 0.0; t <= 1.0; t += 0.001 * speedFactor) 
            {
                Point newpoint = calculateBezierPoint(points, t);
                Thread.Sleep(1);
                System.Windows.Forms.Cursor.Position = newpoint;
            }
            position = finalPoint;
        }
        /// <summary>
        /// Function that generates a random point between two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private Point randowPoint(Point p1, Point p2)
        {
            RandomNumbers rn = new RandomNumbers((int) DateTime.Now.Ticks);
            return new Point((int)rn.NextDouble(p1.X, p2.X), (int)rn.NextDouble(p1.Y, p2.Y));
        }
        /// <summary>
        /// Function that calculates the distance between two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private double distance(Point p1, Point p2) 
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        /// <summary>
        /// Function that calculates the next point according to the Bézier curve
        /// </summary>
        /// <param name="points"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private Point calculateBezierPoint(Point[] points, double t) {
            int grau = points.Length;
            Point p;
            switch (grau) 
            {
                case 2://linear Bézier curve 
                    p = new Point((int)((1 - t) * points[0].X + t * points[1].X), 
                                  (int)((1 - t) * points[0].Y + t * points[1].Y));
                    break;
                case 3://Quadratic Bézier curve
                    p = new Point((int)((1 - t) * (1 - t) * points[0].X + 2 * (1 - t) * t * points[1].X + t * t * points[2].X),
                                  (int)((1 - t) * (1 - t) * points[0].Y + 2 * (1 - t) * t * points[1].Y + t * t * points[2].Y));
                    break;
                case 4://Cubic Bézier curve
                    p = new Point((int)((1 - t) * (1 - t) * (1 - t) * points[0].X + 3 * (1 - t) * (1 - t) * t * points[1].X + 3 * (1 - t) * t * t * points[2].X + t * t * t * points[3].X),
                                  (int)((1 - t) * (1 - t) * (1 - t) * points[0].Y + 3 * (1 - t) * (1 - t) * t * points[1].Y + 3 * (1 - t) * t * t * points[2].Y + t * t * t * points[3].Y));
                    break;
                default:
                    //fourth-order Bézier curve
                    p = new Point ((int)((1 - t) * (1 - t) * (1 - t) * (1 - t) * points[0].X + 4 * (1 - t) * (1 - t) * (1 - t) * t * points[1].X + 6 * (1 - t) * (1 - t) * t * t * points[2].X + 4 * (1 - t) * t * t * t * points[3].X + t * t * t * t * points[4].X),
                                   (int)((1 - t) * (1 - t) * (1 - t) * (1 - t) * points[0].Y + 4 * (1 - t) * (1 - t) * (1 - t) * t * points[1].Y + 6 * (1 - t) * (1 - t) * t * t * points[2].Y + 4 * (1 - t) * t * t * t * points[3].Y + t * t * t * t * points[4].Y));
                    break;
            }
            //Adding noize
            RandomNumbers rn = new RandomNumbers((int)DateTime.Now.Ticks);
            double ruidoX = NextGaussian(rn, t * (1 - t), 1.5);
            double ruidoY = NextGaussian(rn, t * (1 - t), 1.5);
            p.X += (int) ruidoX;
            p.Y += (int) ruidoY;
            return p;
        }
        /// <summary>
        /// Funtion tha generates a random value according to a Gaussian curve
        /// </summary>
        /// <param name="r"></param>
        /// <param name="mu"></param>
        /// <param name="sigma"></param>
        /// <returns></returns>
        private double NextGaussian(Random r, double mu, double sigma)
        {
            var u1 = r.NextDouble();
            var u2 = r.NextDouble();

            var rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);
            var rand_normal = mu + sigma * rand_std_normal;
            return rand_normal;
        }
        /// <summary>
        /// Method tha generates a left button click on the mouse
        /// </summary>
        public void LeftClick()
        {
            LeftDown();
            LeftUp();
        }
        /// <summary>
        /// Method tha generates a double click on the left button on the mouse
        /// </summary>
        public  void doubleClick() 
        {
            LeftClick();
            Thread.Sleep(10);
            LeftClick();
        }
        /// <summary>
        /// Method that generates pressing the left button of the mouse
        /// </summary>
        private void LeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
        /// <summary>
        /// Method that generates the release of the pressed left button of the mouse 
        /// </summary>
        private void LeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
        /// <summary>
        /// Method tha generates a right button click on the mouse
        /// </summary>
        public void RightClick()
        {
            RightDown();
            RightUp();
        }
        /// <summary>
        /// Method that generates pressing the right button of the mouse
        /// </summary>
        private void RightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
        /// <summary>
        /// Method that generates the release of the pressed right button of the mouse 
        /// </summary>
        private void RightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }
        public Rectangle[] getButtonsRectangles() 
        {
            return this.buttons;
        }

    }
    public class RandomNumbers : Random
    {
        public RandomNumbers(int seed) : base(seed) { }

        public double NextDouble(double minimum, double maximum)
        {
            return base.NextDouble() * Math.Abs(maximum - minimum) + Math.Min(minimum, maximum);
        }
    }
}