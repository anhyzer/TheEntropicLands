using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class LookCommand : Command
    {
        public LookCommand() : base()
        {
            this.Name = "look";
            this.SecondaryName = "l";
        }
        public override bool Execute(Player player)
        {
            if (!this.HasSecondWord())
            {
                player.Look();
            }
            else
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}

