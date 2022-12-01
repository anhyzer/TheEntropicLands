using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{ 
    public class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
            this.Name = "inventory";
            this.SecondaryName = "inv";
        }
        public override bool Execute(Player player)
        {
            if (!this.HasSecondWord())
            {
                player.OutputMessage(player.Inventory());
            }
            else
            {
                player.OutputMessage("\nHuh?\n");
            }
            return false;
        }
    }
}
