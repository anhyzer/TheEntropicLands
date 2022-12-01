using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands.Commands
{
    internal class OlcCommand : Command
    {
        public OlcCommand() : base()
        {
            this.Name = "olc";
            this.SecondaryName = "OLC";
        }
        public override bool Execute(Player player)
        {
            if (!this.HasSecondWord())
            {
                //player.Olc();
            }
            else
            {
                player.OutputMessage("\nUnable to initiate OLC mode.");
            }
            return false;
        }
    }
}
