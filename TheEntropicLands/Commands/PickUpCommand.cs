using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class PickUpCommand : Command
    {

            public PickUpCommand() : base()
            {
                this.Name = "take";
                this.SecondaryName = "get";
            }
            public override bool Execute(Player player)
            {
                if (this.HasSecondWord())
                {
                    player.Pickup(this.Phrase);
                    this.Phrase = null;
                }
                else
                {
                    player.OutputMessage("\nGet what?\n");
                }
                return false;
            }
    }
}
