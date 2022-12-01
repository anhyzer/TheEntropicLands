using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    internal class BackCommand : Command
    {
        public BackCommand() : base()
        {
            this.Name = "back";
        }
        public override bool Execute(Player player)
        {
            if (!this.HasSecondWord()) 
            {
                player.Back();
            }
            else 
            {
                player.OutputMessage("Error going back.");
            }
            return false;
        }
    }
}
