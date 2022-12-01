using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class ScoreCommand : Command
    {
        public ScoreCommand() : base() 
        {
            this.Name = "score";
            this.SecondaryName = "sc";
        }
        public override bool Execute(Player player)
        {
            if (this.Name == "score") 
            {
                player.Score();
            }
            else 
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}
