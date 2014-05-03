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
using LIACC.Poker;
using LIACC.Poker.Cards;
using LIACC.Poker.Rules;
using Action = LIACC.Poker.Action;

namespace PokerAPI
{
    public partial class Testing : Form
    {
        //Member objects
        private bool pokerStars = true;
        private Capture capture = null;
        private Image<Bgr, Byte> imgOriginal;
        private IntPtr hWnd;
        private Label[] playersStackLabel = new Label[9];
        private CardDetector cd = new CardDetector();
        private int[] oldValues = new int[9];
        private Player[] players = new Player[9];
        private Random random;
        private double probabilties = 0.0;

        public Testing()
        {
            InitializeComponent();
            hWnd = WinGetHandle("No Limit Hold'em");
            playersStackLabel[0] = Player0Label;
            playersStackLabel[1] = Player1Label;
            playersStackLabel[2] = Player2Label;
            playersStackLabel[3] = Player3Label;
            playersStackLabel[4] = Player4Label;
            playersStackLabel[5] = Player5Label;
            playersStackLabel[6] = Player6Label;
            playersStackLabel[7] = Player7Label;
            playersStackLabel[8] = Player8Label;
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(i, 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += processFrameAndUpdateGUI;
        }
        private void Form1_FormClosed(Object sender, FormClosedEventArgs e) 
        {
            if (capture != null) 
            {
                capture.Dispose();
            }
        }
        private void processFrameAndUpdateGUI(Object sender, EventArgs args) {
            string stringplayer;
            for (int i = 0; i < playersStackLabel.Length; i++) 
            {
                if (i != 0)
                {
                    stringplayer = "Player " + i + ": ";
                }
                else
                    stringplayer = "Me: ";
                playersStackLabel[i].Text = stringplayer + players[i].getStack();
            }
            List<Card> holeCards = cd.getHoleCards();
            if (holeCards.ToArray().Length == 2)
            {
                HandCard1Label.Text = holeCards.ElementAt(0).toStringShort();
                HC1pictureBox.Image = holeCards.ElementAt(0).cornerTemplate.ToBitmap();
                HandCard2Label.Text = holeCards.ElementAt(1).toStringShort();
                HC2pictureBox.Image = holeCards.ElementAt(1).cornerTemplate.ToBitmap();
            }
            else 
            {
                HandCard1Label.Text = "HC1";
                HandCard2Label.Text = "HC2";
            }
            List<Card> CCards = cd.getComunitaryCards();
            int number = CCards.ToArray().Length;
            if (number >= 3)
            {
                CominitaryCard1Label.Text = CCards.ElementAt(0).toStringShort();
                CC1pictureBox.Image = CCards.ElementAt(0).cornerTemplate.ToBitmap();
                ComunitaryCard2Label.Text = CCards.ElementAt(1).toStringShort();
                CC2pictureBox.Image = CCards.ElementAt(1).cornerTemplate.ToBitmap();
                ComunitaryCard3Label.Text = CCards.ElementAt(2).toStringShort();
                CC3pictureBox.Image = CCards.ElementAt(2).cornerTemplate.ToBitmap();                
                if(number == 3)
                {
                    ComunitaryCard4label.Text = "CC4";
                    CC4pictureBox.Image = null;
                    ComunitaryCard5.Text = "CC5";
                    CC5pictureBox.Image = null;
                }
                if (number == 4) 
                { 
                    ComunitaryCard4label.Text = CCards.ElementAt(3).toStringShort();
                    CC4pictureBox.Image = CCards.ElementAt(3).cornerTemplate.ToBitmap();
                    ComunitaryCard5.Text = "CC5";
                    CC5pictureBox.Image = null;
                }
                if (number == 5)
                {
                    ComunitaryCard5.Text = CCards.ElementAt(4).toStringShort();
                    CC5pictureBox.Image = CCards.ElementAt(4).cornerTemplate.ToBitmap();
                }
            }
            else 
            {
                CominitaryCard1Label.Text = "CC1";
                CC1pictureBox.Image = null;
                ComunitaryCard2Label.Text = "CC2";
                CC2pictureBox.Image = null;
                ComunitaryCard3Label.Text = "CC3";
                CC3pictureBox.Image = null;
                ComunitaryCard4label.Text = "CC4";
                CC4pictureBox.Image = null;
                ComunitaryCard5.Text = "CC5";
                CC5pictureBox.Image = null;
            }
            handProbLabel.Text = "" + probabilties;
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
            if (img.Size != new Size(1016,728))
            {
                img = img.Resize(1016, 728, INTER.CV_INTER_CUBIC);
            }
            cd = new CardDetector();
            cd.detectTableCards(img);
            Console.WriteLine(cd.findDealer(img));
            //ibOriginal.Image = imgOriginal;
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
            printPlayerStacks(cd.playerStacks);
            Console.WriteLine("Dealer: " + cd.findDealer(img));
            Console.WriteLine("Buttons: " + cd.buttonsVisible(img));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadTextButton_Click(object sender, EventArgs e)
        {
            VirtualMouse1 vr = new VirtualMouse1(hWnd);
            //vr.fold();
            //vr.call();
            //vr.raise();
            //vr.raise(14);
            //vr.raiseMin();
            //vr.raiseOneHalf();
            //vr.raisePot();
            //vr.allIn();
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
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            DialogResult result = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (result == DialogResult.OK)
            {
                //ibOriginal.Image = new Image<Bgr, Byte>(openFileDialog1.FileName);
                //imgOriginal = new Image<Bgr, Byte>(ibOriginal.Image.Bitmap);
            }
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
            Configure form = new Configure(pictureBox, "TextFiles/buttons.txt", 4);
            form.Controls.Add(pictureBox);
            form.Size = pictureBox.Size;
            form.ShowDialog();
        }

        private void runAgent_Click(object sender, EventArgs e)
        {
            Thread oThread = new Thread(new ThreadStart(this.RunThread));
            oThread.Start();
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
        private void printPlayerStacks(int[] players) 
        {
            Console.Write("{");
            for(int i = 0; i < players.Length - 1; i++)
            {
                Console.Write("" + players[i] + ",");
            }
            Console.Write("" + players[players.Length - 1] + "}\n");
        }
        private int printState(int cardNumber) 
        {
            switch (cardNumber) 
            {
                case 0:
                    Console.WriteLine("Estado: Pre-flop");
                    appendTextBox("Estado: Pre-flop");
                    return 0;
                case 3:
                    Console.WriteLine("Estado: Flop");
                    appendTextBox("Estado: Flop");
                    return 1;
                case 4:
                    Console.WriteLine("Estado: Turn");
                    appendTextBox("Estado: Turn");
                    return 2;
                case 5:
                    Console.WriteLine("Estado: River");
                    appendTextBox("Estado: River");
                    return 3;
                default:
                    return -1;
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (ibOriginal.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                saveFileDialog.FilterIndex = 1;

                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string filename = saveFileDialog.FileName;
                    Console.WriteLine(filename);
                    ibOriginal.Image.Save(filename);
                }
            }*/
        }

        // This method that will be called when the thread is started
        public void RunThread()
        {
            //Importante!!!!!! Inicializar o random apenas uma vez em toda a aplicação!!!
            random = new Random();

            bool myTurn = false, first = true, newGame = false;
            int dealer = -1, secondCount = 0, estado = -1, lastPlayerToMove = 0;
            Game game = new Game();
            MatchState matchState = new MatchState(new State(1337, game), 0);
            //See if changes ocurred in the players stacks
            bool changes = false, runningAgent = false ;
            while (true)
            {
                //Capturing the image
                Image<Bgr, byte> img = captureImage();
                //If is the first image
                if (first)
                {
                    //Detect the comunitary cards
                    cd.detectTableCards(img);
                    estado = printState(cd.getComunitaryCards().ToArray().Length);
                    cd.printComunitaryCards();
                    myTurn = cd.buttonsVisible(img);
                    dealer = cd.findDealer(img);
                }
                //Read the player stacks
                cd.readPlayerStacks(img);
                for (int i = 0; i < players.Length; i++)
                {
                    //Setting the player stacks in the player objects
                    players[i].setStack(cd.playerStacks[i]);
                }
                printPlayerStacks(cd.playerStacks);
                //If it's not the first image
                if (!first)
                {
                    //For all players
                    for (int i = 0; i < players.Length; i++)
                    {
                        //If changes ocurred in is stack
                        if (players[i].getStack() != oldValues[i])
                        {
                            changes = true;
                            if (oldValues[i] > players[i].getStack())
                            {
                                //A raise ocurred
                                int raise = oldValues[i] - players[i].getStack();
                                //PARA JÁ
                                if (runningAgent)
                                {
                                    int j = lastPlayerToMove;
                                    while (j != i)
                                    {
                                        //A fazer call dos possiveis jogadores que fizeram call ou fold... ver isto...
                                        try
                                        {
                                            matchState.state.doAction(game, new LIACC.Poker.Action(ActionType.Call, 0));
                                        }
                                        catch (IndexOutOfRangeException e) { }
                                        j++;
                                        if (j == 10) j = 0;
                                    }
                                    lastPlayerToMove = i;
                                    try
                                    {
                                        matchState.state.doAction(game, new LIACC.Poker.Action(ActionType.Raise, raise));
                                    }
                                    catch (IndexOutOfRangeException e) { }
                                }
                                Console.WriteLine("Player: " + i + " raised " + raise);
                                appendTextBox("\nPlayer: " + i + " raised " + raise);
                            }
                            Console.WriteLine("Player: " + i + " has now " + players[i].getStack());
                            appendTextBox("\nPlayer: " + i + " has now " + players[i].getStack());
                        }
                    }
                }
                //If any change ocurred in the a player stack
                if (changes)
                {
                    int newdealer = cd.findDealer(img);
                    //Locate the dealer position
                    if (newdealer != -1)
                    {
                        if (newdealer == dealer)
                        {
                            //if the dealer is the same, then it's not a new game
                            newGame = false;
                        }
                        else 
                        {
                            dealer = newdealer;
                            //if the dealer changed then it's a new game
                            newGame = true;
                        }
                        Console.WriteLine("Dealer: " + dealer);
                        appendTextBox("\nDealer: " + dealer);
                    }
                    //Detect the comunitary cards
                    cd.detectTableCards(img);
                    if (runningAgent)
                    {
                        List<Card> holeCards = cd.getHoleCards();
                        bool holeCardsConhecidas = (holeCards.ToArray().Length == 2);
                        //deves preencher informação sobre as cartas assim que as detetares:
                        //exemplo, as cartas do jogador 2 são Ás de ouros e Reis de Paus
                        byte[] fullHand;
                        byte[] holeCards1, boardCards1;
                        if (holeCardsConhecidas)
                        {
                            matchState.state.HoleCards[0, 0] = LIACC.Poker.Cards.Card.MakeCard(holeCards.ElementAt(0).toStringShort());
                            matchState.state.HoleCards[0, 1] = LIACC.Poker.Cards.Card.MakeCard(holeCards.ElementAt(1).toStringShort());
                            fullHand = new byte[] { matchState.state.HoleCards[0, 0], matchState.state.HoleCards[0, 1] };
                            holeCards1 = new byte[] { fullHand[0], fullHand[1] };
                            boardCards1 = new byte[] { };
                        }
                        List<Card> boardCards = cd.getComunitaryCards();
                        if (boardCards.ToArray().Length >= 3 && holeCardsConhecidas)
                        {
                            matchState.state.BoardCards[0] = LIACC.Poker.Cards.Card.MakeCard(boardCards[0].toStringShort());
                            matchState.state.BoardCards[1] = LIACC.Poker.Cards.Card.MakeCard(boardCards[1].toStringShort());
                            matchState.state.BoardCards[2] = LIACC.Poker.Cards.Card.MakeCard(boardCards[2].toStringShort());
                            fullHand = new byte[] { matchState.state.HoleCards[0, 0], matchState.state.HoleCards[0, 1], matchState.state.BoardCards[0], matchState.state.BoardCards[1], matchState.state.BoardCards[2] };
                            holeCards1 = new byte[] { fullHand[0], fullHand[1] };
                            boardCards1 = new byte[] { fullHand[2], fullHand[3], fullHand[4] };
                            if (boardCards.ToArray().Length == 4 || boardCards.ToArray().Length == 5)
                            {
                                matchState.state.BoardCards[3] = LIACC.Poker.Cards.Card.MakeCard(boardCards[3].toStringShort());
                                fullHand = new byte[] { matchState.state.HoleCards[0, 0], matchState.state.HoleCards[0, 1], matchState.state.BoardCards[0], matchState.state.BoardCards[1], matchState.state.BoardCards[2], matchState.state.BoardCards[3] };
                                boardCards1 = new byte[] { fullHand[2], fullHand[3], fullHand[4], fullHand[5] };
                                if (boardCards.ToArray().Length == 5)
                                {
                                    matchState.state.BoardCards[4] = LIACC.Poker.Cards.Card.MakeCard(boardCards[4].toStringShort());
                                    fullHand = new byte[] { matchState.state.HoleCards[0, 0], matchState.state.HoleCards[0, 1], matchState.state.BoardCards[0], matchState.state.BoardCards[1], matchState.state.BoardCards[2], matchState.state.BoardCards[3], matchState.state.BoardCards[4] };
                                    boardCards1 = new byte[] { fullHand[2], fullHand[3], fullHand[4], fullHand[5], fullHand[6] };
                                }
                            }
                            Console.WriteLine("Probabilidade da mão: ");
                            Hand.PrintHand(fullHand);
                            var prob = Hand.Equity(holeCards1, boardCards1, 1, game, random);
                            probabilties = prob;
                            Console.WriteLine(" é " + prob);
                        }
                    }
                    int state = printState(cd.getComunitaryCards().ToArray().Length);
                    if (state == 0 && estado != state) 
                    {
                        //If the actual state is pre-flop and the previous is different than pre-flop, then it's a new game
                        newGame = true;
                        estado = state;
                    }
                    cd.printComunitaryCards();
                    //Detect if it is my turn to play
                    myTurn = cd.buttonsVisible(img);
                    //Here is Where the decision for the movements is taking plae
                    if (myTurn)
                    {
                        Console.WriteLine("my turn to play");
                        appendTextBox("my turn to play");
                        VirtualMouse1 vr = new VirtualMouse1(hWnd);
                        if (state == 0)
                            vr.call();
                        else
                        {
                            if (probabilties > 0.6)
                            {
                                vr.raise();
                            }
                            else if (probabilties > 0.3 && probabilties <= 0.6)
                            {
                                vr.call();
                            }
                            else vr.fold();
                        }
                    }
                }
                if (newGame) 
                {
                    Console.WriteLine("New Game started");
                    appendTextBox("New Game Started");
                    
                    //Criar um objeto do tipo Game, no inicio de cada jogo, com as regras do jogo atual.
                    game = new Game();
                    //escolher tipo de jogo: limit ou nolimit
                    game.BettingType = BettingType.NoLimit;
                    //número de cartas de mesa que são adicionadas em cada ronda. no limit é sempre:
                    game.NumBoardCards = new byte[] { 0, 3, 1, 1 };
                    //mesmo para cartas do jogador
                    game.NumHoleCards = 2;
                    //número máximo de raises em cada ronda. no no-limit texas hold'em não há limite... vamos colocar o máximo possível
                    game.MaxRaises = new byte[] { 255, 255, 255, 255 };
                    //tamanho dos raises em cada ronda. não interessa para o no-limit poker
                    game.RaiseSize = new int[] { };
                    //configurar o baralho. sempre igual
                    game.NumRanks = 13;
                    game.NumSuits = 4;
                    //número de rondas
                    game.NumRounds = 4;
                    /////////////////////////
                    //// NOTA: Até aqui a configuração das regras é fixa! A partir das instruções abaixo, depende 
                    //// das condições iniciais do jogo específico!!
                    ////////////////////////
                    //indicar o numero de joadores
                    //os jogadores são numerados de 0 a N-1
                    //não deves considerar os seats vazios!
                    game.NumPlayers = 9;
                    //escolher a estrutura de blinds por assento. 
                    //neste exemplo os jogadores na posicao 1 e na posicao 2 pagaram respetivamente 10 e 20.
                    //nota que os valores sao sempre inteiros (considera a representacao em centimos de dolar)
                    game.Blind[1] = 10;//TODO: VER ISTO!!!!!!!!!!
                    game.Blind[2] = 20;
                    //indicar qual dos jogadores é o primeiro a jogar em cada ronda.
                    //fiz uma função para auxiliar a atribuição para o no limit poker.
                    //neste caso o primeiro jogador seria o que está na posição 2.
                    game.FirstPlayer = GetFirstPlayerNoLimit(2, game.NumPlayers);
                    //preencher os valores iniciais de dinheiro de cada jogador. preencher em centimos
                    //jogador 0 tem 10 euros, jogador 1 tem 15 euros, jogador 2 tem 5 euros, jogador 3 tem 9.50 e jogador 4 tem 2 euros
                    game.Stack = new[] {players[0].getStack(), players[1].getStack(), players[2].getStack(), players[3].getStack(), players[4].getStack(), players[5].getStack(), players[6].getStack(), players[7].getStack(), players[8].getStack()};

                    //Inicar um state para o jogo. O argumento handid (1337) é só um identificador para o estado, podes colocar o que quiseres
                    //o argumento viewingPlayer (2) indica a posição na mesa do teu bot
                    matchState = new MatchState(new State(1337, game), 0);

                    //para modificar o estado do jogo é necessário ocorrer ações
                    //as acções são executadas sequencialmente, e o estado do jogo é automáticamente modificado
                    //não é necessário indicar quem realizou a ação

                    newGame = false;
                    runningAgent = true;
                }
                //Sleep for one second
                Thread.Sleep(1000);
                secondCount++;
                //To ensure that it does not take long to detect again the table cards
                if (secondCount == 3)
                {
                    secondCount = 0;
                    changes = true;
                }
                else 
                {
                    changes = false;
                }
                //Displaying the image
                //ibOriginal.Image = img;
                first = false;
                //Setting the stack old values
                for (int i = 0; i < cd.playerStacks.Length; i++)
                {
                    oldValues[i] = cd.playerStacks[i];
                }
                //Dipose de image
                img.Dispose();
            }
        }

        private void appendTextBox(string msg) 
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(appendTextBox), new object[] { msg });
                return;
            }
            txtMessage.AppendText(msg+"\n");
        }

        static byte[] GetFirstPlayerNoLimit(byte seat, byte numPlayers)
        {
            var otherSeat = (byte)((seat == numPlayers - 1) ? 0 : seat - 1);
            return new[] { seat, otherSeat, otherSeat, otherSeat };
        }

        private void Player1Label_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}