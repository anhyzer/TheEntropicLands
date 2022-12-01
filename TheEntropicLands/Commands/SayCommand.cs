using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class SayCommand : Command
    {
        public SayCommand() : base()
        {
            this.Name = "say";
            this.SecondaryName = "'";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Say(this.Phrase);
                this.Phrase = null;
            }
            else
            {
                player.OutputMessage("\nSay what?\n");
            }
            return false;
        }
    }
}