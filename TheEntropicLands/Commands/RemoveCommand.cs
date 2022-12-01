using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{ 
    public class RemoveCommand : Command
    {
        public RemoveCommand() : base()
        {
            this.Name = "rem";
            this.SecondaryName = "remove";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Remove(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}
