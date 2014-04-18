using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAPI
{
    class Player
    {
        public enum State { onGame, outGame, allIn }
        public enum Move { call, raise, fold}
        private int number, stack;
        private Card[] holeCards = new Card[2];

        private Move lastMove;
        private State presentState;
        private bool dealer, smallBlind, bigBlind;
        /// <summary>
        /// Constructor of class Player
        /// </summary>
        /// <param name="number"></param>
        /// <param name="stack"></param>
        public Player(int number, int stack) 
        {
            this.number = number;
            this.stack = stack;
            this.dealer = false;
            this.smallBlind = false;
            this.bigBlind = false;
        }
        /// <summary>
        /// Raise method
        /// </summary>
        /// <param name="value"></param>
        public void raise(int value) 
        {
            stack -= value;
        }
        /// <summary>
        /// Method to add winned value to his stack
        /// </summary>
        /// <param name="value"></param>
        public void win(int value) 
        {
            stack += value;
        }
        public int getStack() 
        {
            return stack;
        }
        public void setStack(int value)
        {
            this.stack = value;
        }
        public void setHoleCards(Card[] holeCards) 
        {
            this.holeCards = holeCards;
        }
        public Card[] getHoleCards() 
        {
            return holeCards;
        }
        public bool isDealer()
        {
            return this.dealer;
        }
        public void setDealer(bool dealer) 
        {
            this.dealer = dealer;
        }
        public bool isSmallBlind()
        {
            return this.smallBlind;
        }
        public bool isBigBlind()
        {
            return this.bigBlind;
        }
        public void setSmallBlind(bool s)
        {
            this.smallBlind = s;
        }
        public void setBigBlind (bool b)
        {
            this.bigBlind = b;
        }
        public int getNumber() 
        {
            return this.number;
        }
    }
}
