using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class WearCommand : Command
    {
        public WearCommand() : base()
        {
            this.Name = "wear";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Wear(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}
