using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    internal class QuaffCommand : Command
    {
        public QuaffCommand() : base()
        {
            this.Name = "quaff";
            this.SecondaryName = "qua";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Quaff(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nQuaff what\n");
            }
            return false;
        }
    }
}
