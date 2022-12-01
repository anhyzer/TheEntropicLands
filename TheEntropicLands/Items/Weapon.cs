using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class Weapon : Item
    {
        private int _hitRoll;

        private string _damageType;
        public override int HitRoll { get { return _hitRoll; } set { _hitRoll = value; } }
        public override string DamageType { get { return _damageType; } set { _damageType = value; } }
        public Weapon(string name, float weight, float theValue, string type, string damageType) : base(name, weight, theValue, type)
        {
            Type = type;
            DamageType = damageType; 
            Weight = weight;
            Name = name;
            HitRoll = 10; //hardcoded for now
        }
    }
}
