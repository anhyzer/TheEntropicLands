using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class OpenDoorCommand : Command
    {
        public OpenDoorCommand() : base()
        {
            this.Name = "open";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Open(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nOpen what?\n");
            }
            return false;
        }
    }
}
