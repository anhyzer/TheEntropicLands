using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class ExamineCommand : Command
    { 
        public ExamineCommand() : base()
        {
            this.Name = "exa";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Examine(this.Phrase);
                this.Phrase = null;
            }

            else
            {
                player.OutputMessage("\nExamine what?\n");
            }
            return false;
        }
    }
}

