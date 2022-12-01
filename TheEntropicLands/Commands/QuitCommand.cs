using System.Collections;
using System.Collections.Generic;

namespace TheEntropicLands
{
    public class QuitCommand : Command
    {

        public QuitCommand() : base()
        {
            this.Name = "quit";            
        }
        public override bool Execute(Player player)
        {
            bool answer = true;
            if (this.HasSecondWord())
            {
                player.OutputMessage("\nI cannot quit " + this.SecondWord);
                answer = false;
            }
            return answer;
        }
    }
}
