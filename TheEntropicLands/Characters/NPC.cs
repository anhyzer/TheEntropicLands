using System;
using System.Collections.Generic;
using System.Text;

namespace TheEntropicLands
{
    public class NPC : ICharacter
    {
        private string _vuln;
        private string _damageType;
        private string _name;
        private string _description;
        private List<ICharacterState>_states;
        private string _race;
        private int _healthPoints;
        private int _maxHealth;
        private int _hitRoll;
        private int _manaPoints;
        private int _movePoints;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _strength;
        private int _currency;
        private int _dexterity;
        private int _level;
        private Room _currentRoom = null;
        private ItemContainer _inventory;
        private ItemContainer _equipment;
        //Properties of NPC
        public int HealthPoints { get => _healthPoints; set { _healthPoints = value; } }
        public int MaxHealth { get => _maxHealth; set { _maxHealth = value; } }
        public int ManaPoints { get => _manaPoints; set { _manaPoints = value; } }
        public int MovePoints { get => _movePoints; set { _movePoints = value; } }
        public int Currency { get => _currency; set { _currency = value; } }
        public int Strength { get => _strength; set { _strength = value; } }
        public int Intelligence { get => _intelligence; set { _intelligence = value; } }
        public int Wisdom { get => _wisdom; set { _wisdom = value; } }
        public int Constitution { get => _constitution; set { _constitution = value; } }
        public int Dexterity { get => _dexterity; set { _dexterity = value; } }
        public int Level { get => _level; set { _level = value; } }
        public Room CurrentRoom { get => _currentRoom; set { _currentRoom = value; } }
        public string Vuln { get => _vuln; set { _vuln = value; } }
        public string RaceName { get => _race; set { _race = value; } }
        public string Name { get => _name; set { _name = value; } }
        public List<ICharacterState> States { get => _states;}
        public string Description { get => _description; set { _description = value; } }
        public string DamageType { get => _damageType; set => _damageType = value; }
        public int HitRoll { get => _hitRoll; set => _hitRoll = value; }

        private readonly Dictionary<string, Item> _items;
        public NPC(Room room, string name, string raceName)
        {
            Dexterity = 20;
            Strength = 20;
            HealthPoints = 100;
            MaxHealth = 100;
            MovePoints = 100;
            ManaPoints = 100;
            HitRoll = 10;
            Name = name;
            RaceName = raceName;
            Vuln = "holy"; //temporary hard-code
            DamageType = "vampire";
            _items = new Dictionary<string, Item>();
            _states = new List<ICharacterState>();
            States.Add(new AliveState());

            _inventory = new ItemContainer("", 0f, 0f, "container");
        }
        public void OutputMessage(string message) {  }
        public void SuccessMessage(string message) { }
        public void WarningMessage(string message) { }
        public void VictoryMessage(string message) { }

        //Command methods handled by NPC
        public void Attack(string enemyName)
        {
            ICharacter enemy = this.CurrentRoom.GetNPC(enemyName);
            if (enemy != null)
            {
                PlayerState fightingState = new FightingState(enemy);
                this.States.Add(fightingState);
                fightingState = new FightingState(this);
                enemy.States.Add(fightingState);
            }
            else
            {
            }
        }
        public void Look() { }
        public void Dies() 
        {

        }
        public void DropAll()
        {
            string[] inventoryArray = _inventory.GetAllItems();
            foreach (string item in inventoryArray) 
            {
                Drop(item);
            }                                          
        }
        public void Drop(string itemName)
        {
            IItem item = _inventory.Remove(itemName);
            if (item != null)
            {
                CurrentRoom.Drop(item);
            }
        }
        public void WalkTo(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {
                    Room nextRoom = door.GetRoomOnTheOtherSide(this.CurrentRoom);
                    Notification notification = new Notification("PlayerWillLeaveRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.CurrentRoom = nextRoom;
                    notification = new Notification("PlayerEnteredRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                }
                else 
                {
                    this.Open(direction);
                    Room nextRoom = door.GetRoomOnTheOtherSide(this.CurrentRoom);
                    Notification notification = new Notification("PlayerWillLeaveRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.CurrentRoom = nextRoom;
                    notification = new Notification("PlayerEnteredRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                }
            }
        }
        public void Open(string direction)
        {
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
             if (door.IsClosed && door.IsUnlocked)
                {
                    door.Open();
                }
            }
        }
        public void Close(string direction)
        {
            Door door = CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
            }
        }
        public void Say(string word, Player player)
        {
            Console.WriteLine();
            player.SayOutputMessage(this.Name + " says, '");
            Console.ForegroundColor = ConsoleColor.Yellow;
            player.SayOutputMessage(word);
            Console.ResetColor();
            player.SayOutputMessage("'");
            Console.WriteLine();
            Dictionary<string, Object> userInfo = new Dictionary<string, object>
            {
                ["word"] = word
            };
            Notification notification = new Notification("PlayerSaidAWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }
        public static NPC CreateNPC(Room room, string name, string race)
        {
            NPC npc = new NPC(room, name, race);
            room.SetNPC(name, npc);

            return npc;
        }
        public void Give(IItem item)
        {
            _inventory.Add(item);
        }
        public IItem TakeAway(string itemName) 
        { 
            return _inventory.Remove(itemName);
        }
        public void AddArmor(IItem decorator)
        {
            throw new NotImplementedException();
        }

        public void Wear(IItem itemName)
        {

            _equipment.Add(itemName);
        }

        public IItem Remove(string itemName)
        {
            return _equipment.Remove(itemName);
        }

        public void Fight(ICharacter enemy)
        {
            throw new NotImplementedException();
        }

        public void Sleep()
        {
            throw new NotImplementedException();
        }

        public void Drink(IItem drink)
        {
            throw new NotImplementedException();
        }

        public void Eat(IItem food)
        {
            throw new NotImplementedException();
        }
        public void RemoveState(string stateName)
        {
            if (States.Count > 0)
            {
                try
                {
                    foreach (ICharacterState state in States)
                    {
                        if (state.Name == stateName)
                        {
                            States.Remove(state);
                        }
                    }
                }
                catch ( Exception e) { }
            }
        }
    }
}