using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.OCR;
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
        private bool DEBUG = false;
        private bool fullTemplateMatching = false;

        private const double MIN_TMPLT_MATCHING = 0.90;
        private const double MIN_TMPLT_MATCHING_CORNER = 0.97;
        private const double MIN_TMPLT_MATCHING_DEALER = 0.97;

        private Card[] allCards = new Card[52];
        public MCvBox2D tableCardRegion = new MCvBox2D();
        private MCvBox2D[] holeCardsRegions = new MCvBox2D[9];
        private MCvBox2D[] dealerPossibleLocations = new MCvBox2D[9];
        private MCvBox2D[] players = new MCvBox2D[9];
        public int[] playerStacks = new int[9];
        private MCvBox2D potRegion = new MCvBox2D();
        private Image<Bgr, Byte> comunitaryCardRegion;
        private Image<Bgr, Byte> holeRegion;
        private Image<Bgr, Byte> dealerChipTemplate;
        private List<Card> comunitaryCardsM1 = new List<Card>();
        private List<Card> comunitaryCardsM2 = new List<Card>();
        private List<Card> holeCards = new List<Card>();
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public CardDetector(){
            //Setting the pot default position
            potRegion = new MCvBox2D(new PointF(515, 246), new SizeF(150, 22), 0);
            loadPositions();
            //load the templates from disk to memory
            loadTemplates();
        }
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
                    dealerChipTemplate = new Image<Bgr, byte>("DealerTemplate/dealer.png");
                }
            }
        }
        /// <summary>
        /// Method that loads from disc the saved locations os the table cards, hole cards of every player, player's stacks and buttons.
        /// </summary>
        private void loadPositions() 
        {
            try
            {
                using (StreamReader sr = new StreamReader("TextFiles/tableCardsRegion.txt"))
                {
                    string line = sr.ReadToEnd();
                    string[] words = line.Split(' ');
                    tableCardRegion = makeBox(int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]), int.Parse(words[3]));
                }
                readFile("TextFiles/holeCardsRegion.txt", holeCardsRegions, 9);
                readFile("TextFiles/playersStacks.txt", players, 9);
                readFile("TextFiles/dealerPositions.txt", dealerPossibleLocations, 9);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Method that reads the rectangles from the text files
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="boxes"></param>
        /// <param name="n"></param>
        private void readFile(string filename, MCvBox2D[] boxes, int n)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                for (int i = 0; i < n; i++)
                {
                    line = sr.ReadLine();
                    string[] words = line.Split(' ');
                    boxes[i] = makeBox(int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]), int.Parse(words[3]));
                }
            }
        }
        /// <summary>
        /// Method that converts to a MCvBox 2D the rectangle
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private MCvBox2D makeBox(int x, int y, int width, int height) 
        {
            PointF center = new PointF(x + width / 2, y + height / 2);
            return new MCvBox2D(center, new SizeF(width, height), 0);
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
            //tableCardRegion = new MCvBox2D(center_region, sizef_region, 0);

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
            emptyCards();            
            Size tmp_new_size = new Size(56, 79);
            comunitaryCardRegion = cropImage(img, tableCardRegion);
            Image<Bgr, Byte> auxImage = comunitaryCardRegion.Clone();
            //for all card templates
            foreach (Card card in allCards)
            {
                double[] min, max;
                Point[] pointMin, pointMax;
                if (fullTemplateMatching)
                {
                    //###########################FULL TEMPLATE MATCHING####################################
                    //Template match the template with the region containing the comunitary cards
                    Image<Gray, float> comparationImage = auxImage.MatchTemplate(card.template, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
                    comparationImage.MinMax(out min, out max, out pointMin, out pointMax);
                    //If the max correlation exceds some minimum value
                    comparationImage.Dispose();
                    if (max[0] > MIN_TMPLT_MATCHING)
                    {
                        Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(card.template.Width, card.template.Height));
                        //mark the card in the image
                        comunitaryCardRegion.Draw(rect, new Bgr(Color.Red), 2);
                        //Fill the card region with black
                        auxImage.Draw(rect, new Bgr(Color.Black), -1);
                        //print and save the card detected
                        //Console.WriteLine("detected card: " + card.ToString());
                        comunitaryCardsM1.Add(card);
                    }
                }
                else
                {
                    //###########################CORNER TEMPLATE MATCHING##################################
                    //card.cornerTemplate.Resize((int)(tmp_new_size.Width * (15.0 / 56.0)), (int)(tmp_new_size.Height * (36.0 / 79.0)), INTER.CV_INTER_CUBIC);
                    //Template match the corner template with the region containing the comunitary cards
                    Image<Gray, float> comparationCornerImage = auxImage.MatchTemplate(card.cornerTemplate, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
                    comparationCornerImage.MinMax(out min, out max, out pointMin, out pointMax);
                    comparationCornerImage.Dispose();
                    //If the max correlation exceds some minimum value
                    if (max[0] > MIN_TMPLT_MATCHING_CORNER)
                    {
                        Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(card.cornerTemplate.Width, card.cornerTemplate.Height));
                        //mark the card in the image
                        comunitaryCardRegion.Draw(rect, new Bgr(Color.DarkGreen), 2);
                        //Fill the card region with black
                        auxImage.Draw(rect, new Bgr(Color.Black), -1);
                        //print and save the card detected
                        //Console.WriteLine("Communitary card detected: " + card.ToString());
                        comunitaryCardsM2.Add(card);
                    }
                }
                //HOLE CARDS DETECTION
                holeRegion = cropImage(img, holeCardsRegions[0]);
                //Template match the corner template with the region containing the hole cards
                Image<Gray, float> comparationHoleImage = holeRegion.MatchTemplate(card.cornerTemplate, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
                comparationHoleImage.MinMax(out min, out max, out pointMin, out pointMax);
                //If the max correlation exceds some minimum value
                if (max[0] > MIN_TMPLT_MATCHING_CORNER)
                {
                    Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(card.cornerTemplate.Width, card.cornerTemplate.Height));
                    //mark the card in the image
                    holeRegion.Draw(rect, new Bgr(Color.DarkGreen), 2);
                    //Fill the card region with black
                    holeRegion.Draw(rect, new Bgr(Color.Black), -1);
                    //print and save the card detected
                    //Console.WriteLine("hole card detected: " + card.ToString());
                    holeCards.Add(card);
                }
            }
            auxImage.Dispose();
            if (DEBUG)
            {
                CvInvoke.cvShowImage("cartas mão detetadas", holeRegion);
                holeRegion.Dispose();
                CvInvoke.cvShowImage("cartas comunitarias detetadas", comunitaryCardRegion);
            }
            return comunitaryCardRegion;
        }

        public int findDealer(Image<Bgr, Byte> img) 
        {
            Image<Bgr, Byte> region;
            double[] min, max;
            Point[] pointMin, pointMax;
            for (int i = 0; i < dealerPossibleLocations.Length; i++)
            {
                region = cropImage(img, dealerPossibleLocations[i]);
                //Template match the template with the region containing the comunitary cards
                Image<Gray, float> comparationImage = region.MatchTemplate(dealerChipTemplate, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
                comparationImage.MinMax(out min, out max, out pointMin, out pointMax);
                //If the max correlation exceds some minimum value
                comparationImage.Dispose();
                if (max[0] > MIN_TMPLT_MATCHING_DEALER)
                {
                    Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(dealerChipTemplate.Width, dealerChipTemplate.Height));
                    //mark the card in the image
                    region.Draw(rect, new Bgr(Color.Red), 2);
                    return i;
                }
            }
            return -1;
        }
        public bool buttonsVisible(Image<Bgr, Byte> img) 
        {
            Image<Bgr, Byte> region;
            double[] min, max;
            Point[] pointMin, pointMax;
            MCvBox2D box = new MCvBox2D(new PointF(646, 662), new SizeF(311, 106), 0);
            region = cropImage(img, box);
            Image<Bgr, Byte> buttonTemplate = new Image<Bgr, Byte>("ButtonsTemplate/buttons.png");
            //Template match the template with the region containing the comunitary cards
            Image<Gray, float> comparationImage = region.MatchTemplate(buttonTemplate, Emgu.CV.CvEnum.TM_TYPE.CV_TM_CCOEFF_NORMED);
            comparationImage.MinMax(out min, out max, out pointMin, out pointMax);
            //If the max correlation exceds some minimum value
            comparationImage.Dispose();
            if (max[0] > MIN_TMPLT_MATCHING_DEALER)
            {
                Rectangle rect = new Rectangle(new Point(pointMax[0].X, pointMax[0].Y), new Size(buttonTemplate.Width, buttonTemplate.Height));
                //mark the card in the image
                region.Draw(rect, new Bgr(Color.Red), 2);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Function that retrieves the cropped image within a specific rectangle
        /// </summary>
        /// <param name="img"></param>
        /// <param name="cropArea"></param>
        /// <returns></returns>
        public Image<Bgr, Byte> cropImage(Image<Bgr, Byte> img, MCvBox2D cropBox)
        {
            Image<Bgr, Byte> image = null;
            try
            {
                Rectangle cropArea = new Rectangle((int)(cropBox.center.X - cropBox.size.Width / 2), (int)(cropBox.center.Y - cropBox.size.Height / 2), (int)cropBox.size.Width, (int)cropBox.size.Height);
                Bitmap bmpImage = img.ToBitmap();
                Bitmap bmpCrop;
                bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
                image = new Image<Bgr, Byte>(bmpCrop);
                bmpCrop.Dispose();
                bmpImage.Dispose();
            }
            catch (Exception e) 
            {
                Console.WriteLine("Execpção: OutOfMemoryException");
            }
            return image;
        }
        /// <summary>
        /// Function to read each player stack value
        /// </summary>
        /// <param name="img"></param>
        public void readPlayerStacks(Image<Bgr, Byte> img)
        {
            int player = 0;
            foreach (MCvBox2D box in players)
            {
                Image<Bgr, Byte> image = cropImage(img, box);
                if (image != null)
                {
                    image = image.Resize(3.0, INTER.CV_INTER_CUBIC);
                    if (DEBUG) CvInvoke.cvShowImage("player: " + player, image);
                    string[] recong = OCR(image);
                    image.Dispose();
                    if (recong[0].Contains("AllIn"))
                    {
                        if (DEBUG) Console.WriteLine("Player " + player + ": " + "All In");
                        playerStacks[player] = 0;
                    }
                    else if (recong[0].Contains("DeFora"))
                    {
                        if (DEBUG) Console.WriteLine("Player " + player + ": " + "De Fora");
                    }
                    else
                    {
                        try
                        {
                            if (DEBUG)
                            {
                                Console.WriteLine("Player " + player + ": " + int.Parse(recong[1]));
                            }
                            playerStacks[player] = int.Parse(recong[1]);
                        }
                        catch (Exception e)
                        {
                            if (DEBUG)
                                Console.WriteLine("Player " + player + ": " + recong[1]);
                        }
                    }
                    player++;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private string[] OCR(Image<Bgr, Byte> image)
        {
            Tesseract tess = new Tesseract("tessdata", "eng", Tesseract.OcrEngineMode.OEM_DEFAULT);
            tess.Recognize(image);
            //Console.WriteLine(tess.GetText());
            string[] retorno = new string[2];
            retorno[0] = tess.GetText();
            tess.SetVariable("tessedit_char_whitelist", ".0123456789");
            tess.Recognize(image);
            //Console.WriteLine(tess.GetText());
            retorno[1] = tess.GetText();
            retorno[0] = clean(retorno[0]);
            retorno[1] = clean(retorno[1]);
            return retorno;
        }
        private string clean(string s) 
        {
            string r = "";
            for (int i = 0; i < s.Length; i++) 
            {
                if (!". ,:;!*+'?".Contains(s.ElementAt(i)) && s.ElementAt(i)!= '\n')
                    r += s.ElementAt(i);
            }
            return r;
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
        private void emptyCards()
        {
            comunitaryCardsM1 = new List<Card>();
            comunitaryCardsM2 = new List<Card>();
            holeCards = new List<Card>();
        }
        /// <summary>
        /// Getter for the comunitary cards
        /// </summary>
        /// <returns></returns>
        public List<Card> getComunitaryCards()
        {
            return comunitaryCardsM2;
        }
        public List<Card> getHoleCards() 
        {
            return holeCards;
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
        public void printComunitaryCards() 
        {
            foreach (Card card in comunitaryCardsM2) 
            {
                Console.WriteLine(card.ToString());
            }
        }
    }    
}
