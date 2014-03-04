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
    class Card
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
    }
}