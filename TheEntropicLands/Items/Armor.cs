using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class Armor : ItemContainer
    {
        private Dictionary<string, IItem> _sockets = new Dictionary<string, IItem>();
        public Armor(string name, float weight, float theValue, string type) : base(name, weight, theValue, type)
        {

        }
    }
}
