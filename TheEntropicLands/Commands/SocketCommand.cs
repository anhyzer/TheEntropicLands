using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class SocketCommand : Command
    {
        public SocketCommand() : base()
        {
            this.Name = "socket";
        }
        public override bool Execute(Player player)
        {
            if (this.HasThirdWord())
            {
                player.Socket(this.SecondWord, this.ThirdWord);
                this.SecondWord = null;
                this.ThirdWord = null;
            }
            else
            {
                player.OutputMessage("\nSocket what");
            }
            return false;

        }
    }
}
