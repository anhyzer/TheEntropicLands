using System.Collections;
using System.Collections.Generic;

namespace TheEntropicLands
{
    internal class LockCommand : Command
    {
        public LockCommand() : base()
        {
            this.Name = "lock";
            this.SecondaryName = "lo";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Lock(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nLock what?\n");
            }
            return false;
        }
    }
}
