﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PokerAPI
{
    class CardDetector
    {
        //Class members
        private bool DEBUG = true;
        private const double MIN_TMPLT_MATCHING = 0.95;

        private Card[] allCards = new Card[52];
        private MCvBox2D tableCardRegion = new MCvBox2D();
        private Image<Bgr, Byte> comunitaryCardRegion;
        private List<Card> comunitaryCards = new List<Card>();
        private List<Card> comunitaryCardsMethod2 = new List<Card>();

        /// <summary>
        /// Function that loads all card templates from disk
        /// </summary>
        private void loadTemplates() 
        {
            string[] template_paths = Directory.GetFiles("templates");
            string[] corner_template_paths = Directory.GetFiles("CornerTemplates");
            int i = 0;
            foreach (string template_name in template_paths)
            {
                //loading the full card template
                string[] w = template_name.Split('\\', '_', '.');
                if (w[w.Length - 1].Equals("png"))
                {
                    Card.Rank r;
                    switch (w[1])
                    {
                        case "01":
                            r = Card.Rank.ace;
                            break;
                        case "02":
                            r = Card.Rank.douce;
                            break;
                        case "03":
                            r = Card.Rank.three;
                            break;
                        case "04":
                            r = Card.Rank.four;
                            break;
                        case "05":
                            r = Card.Rank.five;
                            break;
                        case "06":
                            r = Card.Rank.six;
                            break;
                        case "07":
                            r = Card.Rank.seven;
                            break;
                        case "08":
                            r = Card.Rank.eight;
                            break;
                        case "09":
                            r = Card.Rank.nine;
                            break;
                        case "10":
                            r = Card.Rank.ten;
                            break;
                        case "11":
                            r = Card.Rank.queen;
                            break;
                        case "12":
                            r = Card.Rank.jack;
                            break;
                        default:
                            r = Card.Rank.king;
                            break;
                    }
                    Card.Suit s;
                    switch (w[3])
                    {
                        case "clubs":
                            s = Card.Suit.clubs;
                            break;
                        case "diamonds":
                            s = Card.Suit.diamonds;
                            break;
                        case "hearts":
                            s = Card.Suit.hearts;
                            break;
                        default:
                            s = Card.Suit.spades;
                            break;                    
                    }
                    Image<Bgr, Byte> template = new Image<Bgr, Byte>(template_name);
                    Image<Bgr, Byte> corner_template = new Image<Bgr, byte>("CornerTemplates/" + w[1] + "_" + w[2] + "_" + w[3] + "." + w[4]);
                    allCards[i++] = new Card(r, s, template, corner_template);
                }
            }
        }
        /// <summary>
        /// Funtion that retrieves the rectangles tha contain a card in a poker table image
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private List<MCvBox2D> findRectangleContainingCard(Image<Bgr, Byte> img)
        {
            Image<Bgr, Byte> img2 = img.Clone();
            //Convert the image to grayscale
            Image<Gray, Byte> gray = img.Convert<Gray, Byte>();

            double cannyThreshold = 180.0;
            double cannyThresholdLinking = 120.0;

            Image<Gray, Byte> cannyEdges = gray.Canny(cannyThreshold, cannyThresholdLinking);
            //ibOriginal.Image = cannyEdges; 
            LineSegment2D[] lines = cannyEdges.HoughLinesBinary(
                        1, //Distance resolution in pixel-related units
                        Math.PI / 45.0, //Angle resolution measured in radians.
                        20, //threshold
                        30, //min Line width
                        10 //gap between lines
                        )[0]; //Get the lines from the first channel
            //region Find rectangles
            List<MCvBox2D> boxList = new List<MCvBox2D>();
            double img_area = img.Size.Width * img.Size.Height;
            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    //if (contours.Area > 4500 && contours.Area < 5000) //only consider contours with area greater than 4500 and less than 5000
                    if ((contours.Area / img_area) > 0.005 && (contours.Area/img_area)< 0.0072) 
                    {
                        if (currentContour.Total == 4) //The contour has 4 vertices.
                        {
                            #region determine if all the angles in the contour are within the range of [80, 100] degree
                            bool isRectangle = true;
                            Point[] pts = currentContour.ToArray();
                            LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                            for (int i = 0; i < edges.Length; i++)
                            {
                                double angle = Math.Abs(edges[(i + 1) % edges.Length].GetExteriorAngleDegree(edges[i]));
                                if (angle < 80 || angle > 100)
                                {
                                    isRectangle = false;
                                    break;
                                }
                            }
                            #endregion
                            if (isRectangle) boxList.Add(currentContour.GetMinAreaRect());
                        }
                    }
                }
            List<MCvBox2D> newBoxList = new List<MCvBox2D>();
            double x_region_min = Double.MaxValue;
            double x_region_max = Double.MinValue;
            double y_region_min = Double.MaxValue;
            double y_region_max = Double.MinValue;
            double comp = 0.0;
            foreach (MCvBox2D box in boxList)
            {
                //Adjusting the card rectangles detected to be parallel to the axes
                PointF[] vertices = box.GetVertices();
                double xMax = (vertices[0].X + vertices[3].X) / 2.0;
                double xMin = (vertices[1].X + vertices[2].X) / 2.0;
                double yMax = (vertices[0].Y + vertices[1].Y) / 2.0;
                double yMin = (vertices[2].Y + vertices[3].Y) / 2.0;
                //Calculating the central region containing cards
                if (x_region_min > xMin) x_region_min = xMin;
                if (y_region_min > yMin) y_region_min = yMin;
                if (x_region_max < xMax) x_region_max = xMax;
                if (y_region_max < yMax) y_region_max = yMax;

                PointF center = new PointF((float)((xMax + xMin) / 2.0), (float)((yMax + yMin) / 2.0));
                SizeF sizef = new SizeF((float)(xMax - xMin), (float)(yMax - yMin));
                comp = xMax - xMin;
                MCvBox2D boxApprox = new MCvBox2D(center, sizef, 0);
                img2.Draw(box, new Bgr(Color.DarkBlue), 2);
                newBoxList.Add(boxApprox);
            }   
            //para garantir que a área detetada pode conter todas as 5 cartas comunitárias
            x_region_max = x_region_min + 4 * 10 + comp * 5;
            PointF center_region = new PointF((float)((x_region_max + x_region_min) / 2.0), (float)((y_region_max + y_region_min) / 2.0));
            SizeF sizef_region = new SizeF((float)(x_region_max - x_region_min), (float)(y_region_max - y_region_min));
            tableCardRegion = new MCvBox2D(center_region, sizef_region, 0);

            if (DEBUG)
            {
                if(x_region_min != Double.MaxValue)
                    CvInvoke.cvShowImage("região cartas comunitárias", cropImage(img, tableCardRegion));
                CvInvoke.cvShowImage("Retangulos detetados", img2);
            }
            if (x_region_min != Double.MaxValue)  comunitaryCardRegion = cropImage(img, tableCardRegion);
            return newBoxList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public Image<Bgr, Byte> detectTableCards(Image<Bgr, Byte> img) 
        {
            emptyComunitaryCards();
            List<MCvBox2D> boxlist = findRectangleContainingCard(img);
            //Retrieve all rectangles containing a card
            Image<Bgr, Byte> RectangleImage = img.Clone();
            //Averaging the size of those rectangles
            double x_average = 0.0, y_average = 0.0;
            int rect_number = 0;
            foreach (MCvBox2D box in boxlist)
            {
                x_average += box.size.Width;
                y_average += box.size.Height;
                rect_number++;
                RectangleImage.Draw(box, new Bgr(Color.DarkBlue), 2);
                Image <Bgr, Byte> rectangleImage = cropImage(img, box);
                //if (DEBUG) CvInvoke.cvShowImage("retangulo encontrado " + rect_number, rectangleImage);
            }
            x_average /= (double)rect_number;
            y_average /= (double)rect_number;
            //Defining the size for the full card templates
            Size tmp_new_size = new Size((int)x_average, (int)y_average);
            //load the templates from disk to memory
            loadTemplates();
            Image<Bgr, Byte> auxImage = comunitaryCardRegion.Clone();
            Image<Bgr, Byte> auxImage2 = comunitaryCardRegion.Clone();
            //for all card templates
            foreach (Card card in allCards)
            {
                //###########################FULL TEMPLATE MATCHING####################################
                //resize the template
                card.template.Resize(tmp_new_size.Width, tmp_new_size.Height, INTER.CV_INTER_CUBIC);
                //Template match the template with the region containing the comunitary cards
                Image<Gray, float> comparationImage = auxImage.MatchTemplate(card.template, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
                double[] min, max;
                Point[] pointMin, pointMax;
                comparationImage.MinMax(out min, out max, out pointMin, out pointMax);
                //If the max correlation exceds some minimum value
                if (max[0] > MIN_TMPLT_MATCHING)
                {
                    Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(card.template.Width, card.template.Height));
                    //mark the card in the image
                    comunitaryCardRegion.Draw(rect, new Bgr(Color.Red), 2);
                    //Fill the card region with black
                    auxImage.Draw(rect, new Bgr(Color.Black), -1);
                    //print and save the card detected
                    Console.WriteLine("detected card: " + card.ToString());
                    comunitaryCards.Add(card);
                }

                //###########################CORNER TEMPLATE MATCHING##################################
                card.cornerTemplate.Resize((int)(tmp_new_size.Width * (15.0 / 56.0)), (int)(tmp_new_size.Height * (36.0 / 79.0)), INTER.CV_INTER_CUBIC);
                //Template match the template with the region containing the comunitary cards
                Image<Gray, float> comparationCornerImage = auxImage2.MatchTemplate(card.cornerTemplate, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
                comparationCornerImage.MinMax(out min, out max, out pointMin, out pointMax);
                //If the max correlation exceds some minimum value
                if (max[0] > MIN_TMPLT_MATCHING)
                {
                    Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(card.cornerTemplate.Width, card.cornerTemplate.Height));
                    //mark the card in the image
                    comunitaryCardRegion.Draw(rect, new Bgr(Color.DarkGreen), 2);
                    //Fill the card region with black
                    auxImage.Draw(rect, new Bgr(Color.Black), -1);
                    //print and save the card detected
                    Console.WriteLine("Corner detected card: " + card.ToString());
                    comunitaryCardsMethod2.Add(card);
                }
            }
            CvInvoke.cvShowImage("cartas detetadas", comunitaryCardRegion);
            return comunitaryCardRegion;
        }
        /// <summary>
        /// Function that retrieves the cropped image within a specific rectangle
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        private Image<Bgr, Byte> cropImage(Image<Bgr, Byte> img, MCvBox2D cropBox)
        {
            Rectangle cropArea = new Rectangle((int)(cropBox.center.X - cropBox.size.Width / 2), (int)(cropBox.center.Y - cropBox.size.Height / 2), (int)cropBox.size.Width, (int)cropBox.size.Height);
            Bitmap bmpImage = img.ToBitmap();
            Bitmap bmpCrop = img.ToBitmap().Clone(cropArea, bmpImage.PixelFormat);
            return new Image<Bgr, Byte>(bmpCrop);
        }        
        /// <summary>
        /// Getter of all card templates
        /// </summary>
        /// <returns></returns>
        public Card[] getAllCards()
        {
            return allCards;
        }
        /// <summary>
        /// Funtion tha emptyes the comunitary cards
        /// </summary>
        private void emptyComunitaryCards()
        {
            comunitaryCards = new List<Card>();
            comunitaryCardsMethod2 = new List<Card>();
        }
        /// <summary>
        /// Getter for the comunitary cards
        /// </summary>
        /// <returns></returns>
        public List<Card> getComunitaryCards()
        {
            return comunitaryCards;
        }
        /// <summary>
        /// Function used to store de card corner templates of all cards
        /// </summary>
        public void storeCornerTemplates() 
        {
            string[] template_paths = Directory.GetFiles("templates");
            foreach (string tmp_name in template_paths) 
            {
                //loading the full card template
                string[] w1 = tmp_name.Split('\\', '_', '.');
                if (w1[w1.Length - 1].Equals("png"))
                {
                    Image<Bgr, Byte> template = new Image<Bgr, byte>(tmp_name);
                    PointF center = new PointF((float)9.5, (float)22.0);
                    SizeF size = new SizeF((float)15.0, (float)36.0);
                    MCvBox2D cropBox = new MCvBox2D(center, size, 0);
                    string[] w = tmp_name.Split('\\');
                    Console.WriteLine("CornerTemplates/" + w[1]);
                    cropImage(template, cropBox).Save("CornerTemplates/" + w[1]);
                }
            }
        }
    }    
}
