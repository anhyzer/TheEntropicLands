using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class CloseDoorCommand : Command
    {
        public CloseDoorCommand() : base()
        {
            this.Name = "close";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Close(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nClose what?\n");
            }
            return false;
        }
    }
}
