using System;
using LIACC.Poker;
using LIACC.Poker.Cards;
using LIACC.Poker.Rules;
using Action = LIACC.Poker.Action;

namespace PokerLibTest
{
    class Program
    {
        static byte[] GetFirstPlayerNoLimit(byte seat, byte numPlayers)
        {
            var otherSeat = (byte) ((seat == numPlayers - 1) ? 0 : seat - 1);
            return new[] {seat, otherSeat, otherSeat, otherSeat};
        }

        /// <summary>
        /// Criar e correr um jogo de Poker
        /// </summary>
        static void Example1()
        {
            //Criar um objeto do tipo Game, no inicio de cada jogo, com as regras do jogo atual.
            Game game = new Game();

            //escolher tipo de jogo: limit ou nolimit
            game.BettingType = BettingType.NoLimit;

            //número de cartas de mesa que são adicionadas em cada ronda. no limit é sempre:
            game.NumBoardCards = new byte[] {0, 3, 1, 1};

            //mesmo para cartas do jogador
            game.NumHoleCards = 2;

            //número máximo de raises em cada ronda. no no-limit texas hold'em não há limite... vamos colocar o máximo possível
            game.MaxRaises = new byte[] {255,255,255,255};

            //tamanho dos raises em cada ronda. não interessa para o no-limit poker
            game.RaiseSize = new int[]{};

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
            game.NumPlayers = 5;

            //escolher a estrutura de blinds por assento. 
            //neste exemplo os jogadores na posicao 1 e na posicao 2 pagaram respetivamente 10 e 20.
            //nota que os valores sao sempre inteiros (considera a representacao em centimos de dolar)
            game.Blind[1] = 10;
            game.Blind[2] = 20;

            //indicar qual dos jogadores é o primeiro a jogar em cada ronda.
            //fiz uma função para auxiliar a atribuição para o no limit poker.
            //neste caso o primeiro jogador seria o que está na posição 2.
            game.FirstPlayer = GetFirstPlayerNoLimit(2,game.NumPlayers);

            //preencher os valores iniciais de dinheiro de cada jogador. preencher em centimos
            //jogador 0 tem 10 euros, jogador 1 tem 15 euros, jogador 2 tem 5 euros, jogador 3 tem 9.50 e jogador 4 tem 2 euros
            game.Stack = new[] {1000, 1500, 500, 950, 200};

            /////////////////////////
            //// NOTA: O jogo está agora configurado. Podes iniciar a simulação!
            ////////////////////////

            //Inicar um state para o jogo. O argumento handid (1337) é só um identificador para o estado, podes colocar o que quiseres
            //o argumento viewingPlayer (2) indica a posição na mesa do teu bot
            MatchState matchState = new MatchState(new State(1337, game), 2);
            
            //para modificar o estado do jogo é necessário ocorrer ações
            //as acções são executadas sequencialmente, e o estado do jogo é automáticamente modificado
            //não é necessário indicar quem realizou a ação

            //raise de 2 euros
            matchState.state.doAction(game, new Action(ActionType.Raise,200));

            //fold
            matchState.state.doAction(game, new Action(ActionType.Fold, 0));

            //call
            matchState.state.doAction(game, new Action(ActionType.Call, 0));

            //é possível verificar se determinada ação é válida
            //o segundo argumento booleano indica se a acção será corrigida para o valor mais próximo,
            //isto é, se a acção não for possível (ex: o valor exceder a stack do jogador), o valor da
            //acção é corrigido para o valor mais próximo possível
            Action action = new Action(ActionType.Raise, 500);
            matchState.state.IsValidAction(game, true, action);

            //deves preencher informação sobre as cartas assim que as detetares:
            //exemplo, as cartas do jogador 2 são Ás de ouros e Reis de Paus
            matchState.state.HoleCards[2, 0] = LIACC.Poker.Cards.Card.MakeCard("Ad"); // ou Cards.Card.AceDiamonds.Value
            matchState.state.HoleCards[2, 1] = LIACC.Poker.Cards.Card.MakeCard("Kc"); // ou Cards.Card.KingClubs.Value
            
            //preencher também as cartas em cada ronda
            //exemplo de River round
            matchState.state.BoardCards[0] = LIACC.Poker.Cards.Card.TwoClubs.Value;
            matchState.state.BoardCards[1] = LIACC.Poker.Cards.Card.TwoDiamonds.Value;
            matchState.state.BoardCards[2] = LIACC.Poker.Cards.Card.TwoHearts.Value;
            matchState.state.BoardCards[3] = LIACC.Poker.Cards.Card.TwoSpades.Value;
            matchState.state.BoardCards[4] = LIACC.Poker.Cards.Card.AceSpades.Value;

            //os seguintes dados do estado do jogo são disponibilizados
            // The hand's unique identifier
            uint a = matchState.state.HandId;

            // Largest bet so far, including all previous rounds
            int b = matchState.state.MaxSpent;
            
            // Minimum number of chips a player must have spend in total to raise
            // only used for noLimitBetting games
            int c = matchState.state.MinNoLimitRaiseTo;

            // Spent[ p ] gives the total amount put into the pot by player p
            int[] d = matchState.state.Spent;

            //Action[ r,i ] gives the i'th action in round r
            Action[,] e = matchState.state.Action;

            // ActingPlayer[ r,i ] gives the player who made action i in round r
            // we can always figure this out from the actions taken, but it's
            // easier to just remember this in multiplayer (because of folds)
            byte[,] f = matchState.state.ActingPlayer;

            // NumActions[ r ] gives the number of actions made in round r
            byte[] g = matchState.state.NumActions;

            // current round: a value between 0 and game.numRounds-1
            // a showdown is still in numRounds-1, not a separate round
            var h = matchState.state.Round;

            // Indicates if the game is over
            bool i = matchState.state.Finished;

            //PlayerFolded[ p ] is non-zero if and only player p has folded
            bool[] j =  matchState.state.PlayerFolded;

        }

        /// <summary>
        /// Manipular cartas e calcular probabilidades
        /// </summary>
        static void Example2()
        {
            var game = Game.NoLimitHoldem2P;
            MatchState matchState = new MatchState(new State(0, game), 0);
            matchState.state.HoleCards[0, 0] = LIACC.Poker.Cards.Card.AceDiamonds.Value;
            matchState.state.HoleCards[0, 1] = LIACC.Poker.Cards.Card.KingClubs.Value;
            matchState.state.BoardCards[0] = LIACC.Poker.Cards.Card.TwoClubs.Value;
            matchState.state.BoardCards[1] = LIACC.Poker.Cards.Card.TenSpades.Value;
            matchState.state.BoardCards[2] = LIACC.Poker.Cards.Card.TwoHearts.Value;
            matchState.state.BoardCards[3] = LIACC.Poker.Cards.Card.ThreeDiamonds.Value;
            matchState.state.BoardCards[4] = LIACC.Poker.Cards.Card.AceSpades.Value;

            byte[] fullHand = new byte[]
            {
                matchState.state.HoleCards[0, 0],
                matchState.state.HoleCards[0, 1],
                matchState.state.BoardCards[0],
                matchState.state.BoardCards[1],
                matchState.state.BoardCards[2],
                matchState.state.BoardCards[3],
                matchState.state.BoardCards[4]
            };

            var holeCards = new byte[]
            {
                fullHand[0],
                fullHand[1],
            };

            var boardCards = new byte[]
            {
                fullHand[2],
                fullHand[3],
                fullHand[4],
                fullHand[5],
                fullHand[6],
            };

            //Importante!!!!!! Inicializar o random apenas uma vez em toda a aplicação!!!
            Random random = new Random();

            Console.WriteLine("Probabilidade da mão: ");
            Hand.PrintHand(fullHand);
            var prob = Hand.Equity(holeCards, boardCards, 1, game, random);
            Console.WriteLine(" é " + prob);
            


        }
        /*
        static void Main(string[] args)
        {
            Example1();
            Example2();

        }*/
    }
}