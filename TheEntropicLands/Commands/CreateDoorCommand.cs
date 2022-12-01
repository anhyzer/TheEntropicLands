using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class CreateDoorCommand : Command
    {
        public CreateDoorCommand() : base()
        {
            this.Name = "dcreate";
        }
        public override bool Execute(Player player)
        {
            if (this.HasFourthWord())
            {
                //player.CreateDoor(this.SecondWord, this.ThirdWord, this.FourthWord);
            }
            else
            {
                player.OutputMessage("\nCreate what door?\n");
            }
            return false;
        }
    }
}
