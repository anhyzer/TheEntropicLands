using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class EquipmentCommand : Command
    {        
        public EquipmentCommand() : base()
        {
            this.Name = "equipment";
            this.SecondaryName = "eq";
        }
        public override bool Execute(Player player)
        {
            if (!this.HasSecondWord())
            {
                player.OutputMessage(player.Equipment());
            }
            else
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}
