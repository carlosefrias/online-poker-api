using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAPI
{
    /// <summary>
    /// Class that represents a playing card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Ranks for the playing cards
        /// </summary>
        public enum Rank { douce, three, four, five, six, seven, eight, nine, ten, queen, jack, king, ace};
        /// <summary>
        /// Suits of the paying cards
        /// </summary>
        public enum Suit { clubs, diamonds, hearts, spades }
        private Rank rank { get; set; }
        private Suit suit { get; set; }
        public Image<Bgr, Byte> template {get; set;}
        public Image<Bgr, Byte> cornerTemplate {get; set;}
        /// <summary>
        /// Constructor of Card class
        /// </summary>
        /// <param name="r"></param>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public Card(Rank r, Suit s, Image<Bgr, Byte> t, Image<Bgr,Byte> ct) {
            this.rank = r;
            this.suit = s;
            this.template = t;
            this.cornerTemplate = ct;
        }
        /// <summary>
        /// ToString function of Card class
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return this.rank + " of " + this.suit;
        }
        public string toStringShort() 
        {
            string s = "";
            switch (this.rank) 
            {  
                case Rank.ace:
                    s += "A";
                    break;
                case Rank.douce:
                    s += 2;
                    break;
                case Rank.three:
                    s += 3;
                    break;
                case Rank.four:
                    s += 4;
                    break;
                case Rank.five:
                    s += 5;
                    break;
                case Rank.six:
                    s += 6;
                    break;
                case Rank.seven:
                    s += 7;
                    break;
                case Rank.eight:
                    s += 8;
                    break;
                case Rank.nine:
                    s += 9;
                    break;
                case Rank.ten:
                    s += "T";
                    break;
                case Rank.jack:
                    s += "J";
                    break;
                case Rank.queen:
                    s += "Q";
                    break;
                default:
                    s += "K";
                    break;
            }
            switch (this.suit) 
            {
                case Suit.clubs:
                    s += "c";
                    break;
                case Suit.diamonds:
                    s += "d";
                    break;
                case Suit.hearts:
                    s += "h";
                    break;
                case Suit.spades:
                    s += "s";
                    break;
            }
            return s;
        }
    }
}