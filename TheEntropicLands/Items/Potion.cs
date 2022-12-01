using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    internal class Potion : Item
    {
        private int _uses;
        private int _healingPower;
        private int _manaPower;        
        public int HealingPower { get { return _healingPower; } set { _healingPower = value;} }
        public int ManaPower { get { return _manaPower; } set { _manaPower = value;} }
        public int Uses { get { return _uses; } set { _uses = value; } }
        public Potion(string name, float weight, float value, string type, int uses, int healingPower, int manaPower) : base() 
        {
            Name = name;
            Weight = weight;
            Value = value;
            Type = type;
            Uses = uses;
            HealingPower = healingPower;
            ManaPower = manaPower;
        }
    }
}
