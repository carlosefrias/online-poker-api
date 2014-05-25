using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAPI.database
{
    public class GameStats
    {
        private string time { get; set; }
        private int StartValue { get; set; }
        private int EndValue { get; set; }
        private int EarnedValue { get; set; }
        private int NumRaises { get; set; }

        public GameStats(string time, int StartValue, int EndValue, int EarnedValue, int NumRaises) 
        {
            this.time = time;
            this.StartValue = StartValue;
            this.EndValue = EndValue;
            this.EarnedValue = EarnedValue;
            this.NumRaises = NumRaises;
        }
        public string getTime() { return this.time; }
        public int getEarnedValue() { return this.EarnedValue; }
        public int getStartValue() { return this.StartValue; }
        public int getEndValue() { return this.EndValue; }
        public int getNumRaises() { return this.NumRaises;}

        public override string ToString() 
        {
            return "Game register:\nTime: " + time + "\nStart value: "
                + StartValue + "\nEnd value: " + EndValue + "\nEarned value: " + EarnedValue
                + "\nNumber of raises: " + NumRaises;
        }
    }
}
