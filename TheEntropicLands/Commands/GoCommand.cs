using System.Collections;
using System.Collections.Generic;

namespace TheEntropicLands
{
    public class GoCommand : Command
    {
        public GoCommand() : base()
        {
            this.Name = "go";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.WalkTo(this.SecondWord);
            }
            else
            {
                player.OutputMessage("\nGo where?");
            }
            return false;
        }
    }
}
