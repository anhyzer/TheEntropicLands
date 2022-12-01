using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class Item : IItem
    {
        private string _name;

        private IItem _decorator; //Decorator Pattern

        private float _weight;

        private float _value;

        private string _type;
        public string Type {  get => _type; set => _type = value; }
        public virtual float Weight { set {  _weight = value; } get { return _weight + (_decorator!=null?_decorator.Weight:0); } }
        public  float Value { set { _value = value; } get { return _value + (_decorator!=null?_decorator.Value:0); } }   
        public string Name { set {  _name = value; } get { return _name; } }
        public string LongName { get { return Name + ", " + (_decorator != null ? _decorator.LongName : ""); } } 
        public virtual int HitRoll { get; set; }
        public virtual string Description { get { return LongName + "weight = " + Weight + ", value = " + Value + "."; } }
        public virtual string DamageType { get; set; }
        public virtual string Equipment { get; }
        public virtual bool IsContainer { get => false; }
        public bool Wearable { get => (_type == "weapon" || _type == "armor"); } 
        public Item() : this("No Name") {   }
        public Item(string name) : this(name, 1f, null) { }
        public Item(string name, float weight, string type) : this(name, weight, 1, null) { }

        //Designated Constructor
        public Item(string name, float weight, float theValue, string type) 
        {
            Name = name;
            Weight = weight;
            Value = theValue;
            _decorator = null;
            _type = type;
        }
        public virtual void Add(IItem item) { }
        public virtual IItem Remove(string itemName) 
        {
            return null;
        }
        public void AddDecorator(IItem decorator) 
        { 
            if(_decorator == null) 
            { 
                _decorator = decorator;
                this.DamageType = _decorator.DamageType;
            }
            else 
            {
                _decorator.AddDecorator(decorator);
            }
        }
    }
    public class ItemContainer : Item //facade?
    {
        private Dictionary<string, IItem> _items = new Dictionary<string, IItem>();

        private Dictionary<string, IItem> _equipment = new Dictionary<string, IItem>();

        public override bool IsContainer { get => true; }

        public int Count { get { return _items.Count; } }

        public override float Weight
        {
            get
            {
                float totalWeight = 0;
                Dictionary<string, IItem>.ValueCollection items = _items.Values;
                Dictionary<string, IItem>.ValueCollection equipment = _equipment.Values;
                foreach (IItem item in items)
                    foreach (IItem equipmentPieces in equipment)
                    {
                        {
                            totalWeight += item.Weight;
                        }
                    }
                return totalWeight;
            }
        }
        public override string Description
        {
            get
            {
                if (_items.Count != 0)
                {
                    {
                        string itemNames = "\n";
                        Dictionary<string, IItem>.KeyCollection keys = _items.Keys;
                        foreach (string itemName in keys)
                        {
                            itemNames += "A " + itemName + " is here.\n";
                        }
                        return itemNames;
                    }
                }
                else { return null; }
            }
        }
        public override string Equipment
        {
            get
            {
                if (_equipment.Count != 0)
                {
                    {
                        string itemNames = "\n";
                        Dictionary<string, IItem>.KeyCollection keys = _equipment.Keys;
                        foreach (string itemName in keys)
                        {
                            itemNames += "A " + itemName + "\n";
                        }
                        return itemNames;
                    }
                }
                else { return null; }
            }
        }
        public string Inventory
        {
            get
            {
                if (_items.Count != 0)
                {
                    {
                        string itemNames = "\n";
                        Dictionary<string, IItem>.KeyCollection keys = _items.Keys;
                        foreach (string itemName in keys)
                        {
                            itemNames += "A " + itemName + "\n";
                        }
                        return itemNames;
                    }
                }
                else { return null; }
            }
        }
        public ItemContainer(string name, float weight, float theValue, string type) : base(name, weight, theValue, type)
        {
            _items = new Dictionary<string, IItem>();
        }
        public override void Add(IItem item)
        {
            if (item != null)
            {
                _items.TryAdd(item.Name, item);
            }
        }
        public override IItem Remove(string itemName)
        {
            try
            {
                IItem item = _items[itemName];
                if (item != null)
                {
                    _items.Remove(itemName);
                }
                return item;
            }
            catch
            {
                return null;
            }
        }
        public string[] GetAllItems()
        {
            string[] itemsArray = new string[_items.Count];
            Dictionary<string, IItem>.KeyCollection keyCollection = _items.Keys;
            try
            {
                for (int i = 0; i < itemsArray.Length - 1; i++)
                {
                    foreach (string key in keyCollection)
                    {
                        itemsArray[i] = key;
                    }
                }                
            }
            catch { }
            return itemsArray;
        }
    }
}
