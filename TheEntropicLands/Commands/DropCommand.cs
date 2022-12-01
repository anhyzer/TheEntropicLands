using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    class DropCommand : Command
    {
        public DropCommand() : base()
        {
            this.Name = "drop";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Drop(this.Phrase);
                this.Phrase = null;
            }
            else
            {
                player.OutputMessage("\nDrop what");
            }
            return false;
        }
    }
}
