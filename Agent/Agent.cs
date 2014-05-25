using Emgu.CV;
using Emgu.CV.Structure;
using MouseManipulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAPI.Agent
{
    class Agent
    {
        private string name;
        public Agent(string name) 
        {
            this.name = name;
        }
        //Do not change the signature of this function!!!
        public void AgentEventHandler(PokerAPI.CardDetector cd, PokerAPI.Player[] players, double probabilities, bool myTurn, IntPtr hWnd, Image<Bgr, byte> img) 
        {
            //Perform the action only when is my turn to play
            if (myTurn)
            {
                List<PokerAPI.Card> holeCards = cd.getHoleCards(), boardCards = cd.getComunitaryCards();
                int myStack = players[0].getStack();

                //Setting the PokerAPI to automatically add chips when we run out of money
                cd.autoAddChips = true;

                //Associating a virtual mouse object to the pokerstars game window
                VirtualMouse1 vr = new VirtualMouse1(hWnd);
                //cd.printComunitaryCards();
                //Deffinig our AI strategy
                if (myStack >= 100)
                {
                    if (probabilities > 0.85)
                    {
                        vr.allIn();
                    }
                    else if (probabilities > 0.6)
                    {
                        vr.raise();
                    }
                    else if (probabilities >= 0.4)
                    {
                        //The call button is not allways visible
                        if(cd.callButtonVisible(img)) vr.call();
                        else vr.fold();
                    }
                    else vr.fold();
                }
                else
                {
                    if(probabilities > 0.9)
                    {
                        vr.allIn();
                    }
                    else if (probabilities >= 0.8)
                    {
                        vr.raise();
                    }
                    if (probabilities >= 0.7)
                    {
                        //The call button is not allways visible
                        if (cd.callButtonVisible(img)) vr.call();
                        else vr.fold();
                    }
                }
                //moving the mouse to other place after performing the move
                vr.moveToRamdomPlace();
            }
        }
    }
}
