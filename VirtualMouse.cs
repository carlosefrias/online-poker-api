using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace MouseManipulator
{
    public class VirtualMouse1
    {
        [DllImport("user32.dll")]
        extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001, MOUSEEVENTF_LEFTDOWN = 0x0002, MOUSEEVENTF_LEFTUP = 0x0004, MOUSEEVENTF_RIGHTDOWN = 0x0008, 
            MOUSEEVENTF_RIGHTUP = 0x0010, MOUSEEVENTF_MIDDLEDOWN = 0x0020, MOUSEEVENTF_MIDDLEUP = 0x0040, MOUSEEVENTF_ABSOLUTE = 0x8000;
        private Point position;
        public VirtualMouse1() 
        {
            position = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
        }
        public void Move(int xDelta, int yDelta)
        {
            position.X += xDelta;
            position.Y += yDelta;
            System.Windows.Forms.Cursor.Position = position;
        }
        public void MoveTo(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new Point(x, y);
            position = new Point(x, y);
        }
        public void MoveToBezierCurve(int x, int y) 
        {
            Point finalPoint = new Point(x, y);
            Point initialPoint = position;
            Console.WriteLine("ponto:(" + initialPoint.X + "," + initialPoint.Y + ")");
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
        private Point randowPoint(Point p1, Point p2)
        {
            RandomNumbers rn = new RandomNumbers((int) DateTime.Now.Ticks);
            return new Point((int)rn.NextDouble(p1.X, p2.X), (int)rn.NextDouble(p1.Y, p2.Y));
        }
        private double distance(Point p1, Point p2) 
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
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
            return p;
        }
        public void LeftClick()
        {
            LeftDown();
            LeftUp();
        }
        public  void doubleClick() 
        {
            LeftClick();
            Thread.Sleep(10);
            LeftClick();
        }
        private void LeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        private void LeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        public void RightClick()
        {
            RightDown();
            RightUp();
        }

        private void RightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        }

        private void RightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
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