using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class Socket : IItem
    {
        private string _name, _type, _damageType;
        private float _weight, _value;
        public new string Name { get => _name; set => _name = value; }
        public new string Type { get => _type; set => _type = value; }
        public new string DamageType { get => _damageType; set => _damageType = value; }
        public new float Weight { get => _weight; set => _weight = value; }
        public new float Value { get => _value; set => _value = value; }

        public int HitRoll => throw new NotImplementedException();

        public bool Wearable => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public string LongName => throw new NotImplementedException();

        public bool IsContainer => throw new NotImplementedException();

        public Socket(string name, float weight, float value, string type, string damageType)
        {
            Name = name;
            Weight = weight;
            Value = value;
            DamageType = damageType;
            Type = type;
        }

        public void AddDecorator(IItem decorator)
        {
            throw new NotImplementedException();
        }

        public void Add(IItem itemName)
        {
            throw new NotImplementedException();
        }

        public IItem Remove(string itemName)
        {
            throw new NotImplementedException();
        }
    }
}
