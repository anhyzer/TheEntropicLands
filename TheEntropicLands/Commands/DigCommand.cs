using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class DigCommand : Command
    {
        public DigCommand() : base()
        {
            this.Name = "dig";
        }
        public override bool Execute(Player player)
        {
            if (this.HasThirdWord())
            {
                //player.Dig(this.SecondWord, this.ThirdWord, this.FourthWord, this.FifthWord);
            }
            else
            {
                //player.Dig(this.SecondWord, this.ThirdWord, this.FourthWord);
                player.OutputMessage("\nDig what?\n");
            }
            return false;
        }
    }
}
