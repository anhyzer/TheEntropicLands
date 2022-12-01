using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class UnlockCommand : Command
    {
        public UnlockCommand() : base()
        {
            this.Name = "unlock";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Unlock(this.SecondWord);
            }
            else if(!this.HasSecondWord())
            {
                player.OutputMessage("\nUnlock what?\n");
            }
            return false;
        }
    }
}
