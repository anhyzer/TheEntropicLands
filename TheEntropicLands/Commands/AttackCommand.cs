using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class AttackCommand : Command
    {
        public AttackCommand() : base()
        {
            this.Name = "kill";
            this.SecondaryName = "ki";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.Attack(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}
