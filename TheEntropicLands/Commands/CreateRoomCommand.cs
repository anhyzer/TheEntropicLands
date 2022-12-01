using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class CreateRoomCommand : Command
    {
        public CreateRoomCommand() : base() 
        {
            this.Name = "rcreate";
        }
        public override bool Execute(Player player)
        {
            if (this.HasSecondWord())
            {
                player.CreateRoom(this.SecondWord);
            }
            else 
            {
                player.OutputMessage("\nCreate what room?\n");
            }
            return false;
        }
    }
}
