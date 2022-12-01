using System.Collections.Generic;
using System;

namespace TheEntropicLands
{
    public class Player : ICharacter
    {
        public enum RACE {  HUMAN, UNDEAD };
        private string _vuln;
        private int _hitroll;
        private int _maxHealth;
        private string _name;
        private string _description;
        private string _damageType;
        private RACE _race;
        private int _healthPoints;
        private int _maxMana;
        private int _manaPoints;
        private int _movePoints;
        private int _constitution;
        private int _intelligence;
        private int _wisdom;
        private int _strength;
        private int _currency;
        private int _dexterity;
        private int _level;
        private int _lives;
        private Room _entrance = null;
        private Room _currentRoom = null;
        private Room _underworld = null;
        private Stack<Room> _rooms = new Stack<Room>();
        private List<ICharacterState> _states;
        private ItemContainer _inventory;
        private ItemContainer _equipment;
        //Properties of Player
        public int HealthPoints { get => _healthPoints; set { _healthPoints = value; } }
        public int MaxHealth { get => _maxHealth; set { _maxHealth = value; } }
        public int ManaPoints { get => _manaPoints; set { _manaPoints = value; } }
        public int MaxMana { get => _maxMana; set { _maxMana = value; } }
        public int MovePoints { get => _movePoints; set { _movePoints = value; } }
        public int Currency { get => _currency; set { _currency = value; } }
        public int Strength { get => _strength; set { _strength = value; } }
        public int Lives { get => _lives; set { _lives = value; } }
        public int Intelligence { get => _intelligence; set { _intelligence = value; } }
        public int Wisdom { get => _wisdom; set { _wisdom = value; } }
        public int Constitution { get => _constitution; set { _constitution = value; } }
        public int Dexterity { get => _dexterity; set { _dexterity = value; } }
        public int Level { get => _level; set { _level = value; } }
        public Room Entrance { get => _entrance; set { _entrance = value; } }
        public Room CurrentRoom { get => _currentRoom; set { _currentRoom = value; } }
        public Room Underworld { get => _underworld; set { _underworld = value; } }
        public RACE RaceName { get => _race; set { _race = value; } }
        public string Vuln { get => _vuln; set { _vuln = value; } }
        public string Name { get => _name; set { _name = value; } }
        public int HitRoll { get => _hitroll; set { _hitroll = value; } }
        public string DamageType { get => _damageType; set { _damageType = value; } }
        public string Description { get => _description; set { _description = value; } }
        public List<ICharacterState> States { get => _states; }
        //Designated Constructor
        public Player(Room room, string name, RACE raceName)
        {
            Name = name;
            RaceName = raceName;
            Dexterity = 20;
            Strength = 20;
            MaxHealth = 100 + ((Constitution * (Strength/10)));
            MaxMana = 100 + ((Intelligence * (Wisdom / 10)));
            MovePoints = 100;
            Constitution = 20;
            Intelligence = 20;
            Wisdom = 20;
            HitRoll = 5;
            Lives = 3;
            DamageType = null;
            Vuln = null;
            Currency = 10000;
            HealthPoints = 100 + ((Constitution * (Strength / 10)));
            ManaPoints = 100 + ((Intelligence * (Wisdom / 10)));
            CurrentRoom = room;
            _equipment = new ItemContainer("", 0, 0, "container");
            _inventory = new ItemContainer("", 0, 0, "container");
            _states = new List<ICharacterState>();
            States.Add(new AliveState());
            States.Add(new HungryState());
        }
        //Character methods
        public void Give(IItem item)
        {
            _inventory.Add(item);
        }
        public void Dies() 
        {
            this.WarningMessage("\nYou have been reimprisoned.\n");
            this.HealthPoints = MaxHealth / 3;
            this.CurrentRoom = Entrance;
            this.Lives -= 1;
            Door door = CurrentRoom.GetExit("west");
            door.Close();
            door.Lock();
            if(this.Lives == 0) 
            {
                this.WarningMessage("You Lose.");
                this.CurrentRoom = GameWorld.Underworld;
            }
        }
        public void Back() 
        { 
            if(_rooms.Count > 0) 
            {
                this.CurrentRoom = _rooms.Pop(); //hacky
                this.Look();
            }
            else 
            {
                this.OutputMessage("\nThere is nowhere to go back to.\n");
            }
        }
        public void DropAll()
        {
            string[] inventoryArray = _inventory.GetAllItems();
            foreach (string item in inventoryArray)
            {
                if (item != null)
                {
                    this.Drop(item);
                }
            }
        }
        public void OutputMessage(string message) { Console.WriteLine(message); }
        public void SuccessMessage(string message) { Console.ForegroundColor = ConsoleColor.Cyan; OutputMessage(message); Console.ResetColor(); }
        public void WarningMessage(string message) { Console.ForegroundColor = ConsoleColor.Red; OutputMessage(message); Console.ResetColor(); }
        public void VictoryMessage(string message) { Console.ForegroundColor = ConsoleColor.Green; OutputMessage(message); Console.ResetColor(); }


        public void Regen()
        {
            if (this.HealthPoints > 0)
            {
                if (this.HealthPoints != this.MaxHealth)
                {
                    this.HealthPoints += ((this.Constitution * (this.Dexterity / 10)) / this.HealthPoints);
                }
                if (this.ManaPoints != this.MaxMana)
                {
                    this.ManaPoints += ((this.Intelligence * (this.Dexterity / 10)) / this.HealthPoints);
                }
                else if (this.HealthPoints > this.MaxHealth)
                {
                    this.HealthPoints = this.MaxHealth;
                }
            }
        }
        public void SayOutputMessage(string message) { Console.Write(message); }
        public IItem TakeAway(string itemName) 
        {
            return _inventory.Remove(itemName);
        }
        //Command methods handled by Player
        public void Attack(string enemyName)
        {
            ICharacter enemy = this.CurrentRoom.GetNPC(enemyName);
            if (enemy != null)
            {
                Combat combat = new Combat(this, enemy);               
                combat.Fight();
            }
            else 
            {
                this.OutputMessage("There is no one to attack.");
            }            
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
                catch (Exception ex) { }
            }
        }
        public bool GetState(string stateName) 
        {
            bool found = false;
            foreach (ICharacterState state in States)
            {
                if (stateName == state.Name)
                {
                    found = true;
                }
            }
            return found;
        }
        public void Close(string direction)
        {
            Door door = CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.CanClose)
                {
                    if (door.IsClosed)
                    {
                        if (direction == "up")
                        {
                            this.OutputMessage("\nThe door above you is already closed.\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nThe door below you is already closed.\n");
                            return;
                        }
                        else
                        {
                            this.OutputMessage("\nThe door to the " + direction + " is already closed.\n");
                        }
                    }
                    else if (door.IsOpen)
                    {
                        door.Close();
                        if (direction == "up")
                        {
                            this.OutputMessage("\nYou close the door above you.\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nYou close the door below you.\n");
                            return;
                        }
                        else
                        {
                            this.OutputMessage("\nYou close the door to the " + direction + ".\n");
                        }
                    }
                }
            }
            else
            {
                if (direction == "up")
                {
                    this.OutputMessage("\nThere is no door above you.\n");
                    return;
                }
                else if (direction == "down")
                {
                    this.OutputMessage("\nThere is no door below you.\n");
                    return;
                }
                this.OutputMessage("\nThere is no door to the " + direction + ".\n");
            }
        }
        public Door CreateDoor(Room roomB, string diretionTo, string directomFrom, bool closeable)
        {
            Door newDoor = Door.CreateDoor(this.CurrentRoom, roomB, diretionTo, directomFrom, closeable);

            return newDoor;
        }
        public Room CreateRoom(string tag)
        {
            Room newRoom = new Room();
            newRoom.Tag = tag;
            this.CurrentRoom = newRoom;
            return newRoom;
        }
        public void Dig(string directionTo, string directionFrom, string newTag, bool closeable)
        {
            Room newRoom = this.CreateRoom(newTag);
            _ = Door.CreateDoor(this.CurrentRoom, newRoom, directionTo, directionFrom, closeable);
            this.CurrentRoom = newRoom;

        }
        public void Drop(string itemName)
        {
            IItem item = _inventory.Remove(itemName);
            if (item != null)
            {
                CurrentRoom.Drop(item);
                OutputMessage("\nYou dropped the " + itemName + "\n");
            }
            else
            {
                OutputMessage("\nThe " + itemName + " is not in your inventory.\n");
            }
        }
        public string Equipment()
        {
            if (_equipment.Count > 0)
            {
                return "Equipped:\n" + _equipment.Equipment;
            }
            else
            {
                return "\nYou are wearing nothing.\n";
            }
        }
        public void Examine(string itemName)
        {
            IItem item = CurrentRoom.Remove(itemName);
            if (item != null)
            {
                OutputMessage("\n" + item.Description + "\n");
                CurrentRoom.Drop(item);
            }
            else
            {
                OutputMessage("\nThe item " + itemName + " is not in the room.\n");
            }
        }
        public string Inventory()
        {
            if (_inventory.Count != 0)
            {
                return "You are carrying:" + _inventory.Inventory;
            }
            else
            {
                return "\nYou are carrying nothing.\n";
            }
        }
        public void Lock(string direction)
        {
            Door door = CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsUnlocked && door.HasLock() && !door.IsOpen)
                {
                    door.Lock();
                    if (direction == "up")
                    {
                        this.OutputMessage("\nYou hear a small click as you lock the door above you.\n");
                        return;
                    }
                    else if (direction == "down")
                    {
                        this.OutputMessage("\nYou hear a small click as you lock the door below you.\n");
                        return;
                    }
                    else
                    {
                        OutputMessage("\nYou hear a small click as you lock the door to the " + direction + ".\n");
                    }
                }
                else if (door.IsLocked)
                {
                    if (direction == "up")
                    {
                        this.OutputMessage("\nThe door above you is already locked..\n");
                        return;
                    }
                    else if (direction == "down")
                    {
                        this.OutputMessage("\nThe door below you is already locked.\n");
                        return;
                    }
                    else
                    {
                        OutputMessage("\nThe door to the " + direction + " is already locked.\n");
                    }
                }
                else
                {
                    if (direction == "up")
                    {
                        this.OutputMessage("\nThe door above you cannot be locked..\n");
                        return;
                    }
                    else if (direction == "down")
                    {
                        this.OutputMessage("\nThe door below you cannot be locked.\n");
                        return;
                    }
                    else
                    {
                        OutputMessage("\nThe door to the " + direction + " cannot be locked.\n");
                    }
                }
            }
            else
            {
                if (direction == "up")
                {
                    this.OutputMessage("\nThere is no door above you.\n");
                    return;
                }
                else if (direction == "down")
                {
                    this.OutputMessage("\nThere is no door below you.\n");
                    return;
                }
                else
                {
                    this.OutputMessage("There is no door to the " + direction + ".\n");
                }
            }
        }
        public void Look() { OutputMessage(this.CurrentRoom.Description()); }
        public void Open(string direction) 
        { 
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.CanClose)
                {
                    if (door.IsOpen)
                    {
                        if (direction == "up")
                        {
                            this.OutputMessage("\nThe door above you is already open.\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nThe door below you is already open.\n");
                            return;
                        }
                        this.OutputMessage("\nThe door to the " + direction + " is already open.\n");
                    }
                    else if (door.IsLocked)
                    {
                        if (direction == "up")
                        {
                            this.OutputMessage("\nThe door above you is locked.\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nThe door below you is locked.\n");
                            return;
                        }
                        else
                        {
                            this.OutputMessage("\nThe door to the " + direction + " is locked.\n");
                        }
                    }
                    else if (door.IsClosed && door.IsUnlocked)
                    {
                        door.Open();
                        if (direction == "up")
                        {
                            this.OutputMessage("\nYou open the door above you.\n");

                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nYou open the door below you.\n");

                        }
                        else
                        {
                            this.OutputMessage("\nYou open the door to the " + direction + ".\n");
                        }
                    }
                }
            }
            else
            {
                if (direction == "up")
                {
                    this.OutputMessage("\nThere is no door above you.\n");
                }
                else if (direction == "down")
                {
                    this.OutputMessage("\nThere is no door below you.\n");

                }
                else
                {
                    this.OutputMessage("\nThere is no door to the " + direction + ".\n");
                }
            }
        }
        public void Pickup(string itemName)
        {
            IItem item = CurrentRoom.Remove(itemName);
            if (item != null) {
                if(item.Weight <= this.Strength)
                {
                    Give(item);
                    OutputMessage("\nYou pick up the " + itemName + ".\n");
                }
                else if (item.Weight > this.Strength)
                {
                    CurrentRoom.Drop(item);
                    OutputMessage("\nThat is too heavy for you to pick up!\n");
                }
            }
            else
            {
                OutputMessage("\nThe item " + itemName + " is not in the room.\n");
            }
        }
        public void PlayerPickedSomethingUp(Notification notification)
        {
            Player player = (Player)notification.Object;            
        }
        public void Quaff(string itemName) //bugged
        {
            Potion potion = null;
            if (potion != null)
            {
                if (potion.Uses >= 1)
                {
                    this.HealthPoints += (potion.HealingPower * (1 / this.HealthPoints));
                    potion.Uses -= 1;
                }
                else
                {
                    this.OutputMessage("That is not a potion!");
                }
                if (potion.Uses >= 1)
                {
                    this.Give(potion);
                }
            }
        }
        public IItem Remove(string itemName)
        {
            IItem item = _equipment.Remove(itemName);
            _inventory.Add(item);
            if (item != null)
            {
                if (item.HitRoll > 0)
                {
                    this.HitRoll -= item.HitRoll;
                }
                if (item.DamageType != "normal")
                {
                    this.DamageType = "normal";
                }
            }            
            return item;
        }
        public void Say(string word)
        {
            Console.WriteLine();
            SayOutputMessage("You say, '");
            Console.ForegroundColor = ConsoleColor.Yellow;
            SayOutputMessage(word);
            Console.ResetColor();
            SayOutputMessage("'\n");
            Console.WriteLine();
            Dictionary<string, Object> userInfo = new Dictionary<string, object>
            {
                ["word"] = word
            };
            Notification notification = new Notification("PlayerSaidAWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }
        public void Score()
        {
            this.OutputMessage("\n" +
              @"//===================================================" + "\n" +
              "||" + this.Name + "                                               \n" +
              "||---------------------------------------------------\n" +
              "||" + this.RaceName + "                                              \n" +
              "||" + this.Description + "                                                   \n" +
              "||" + this.DamageType + "                                             \n" +
              "||                                                   \n" +
              @"\\===================================================" + 
              "\n");                
        }
        public void Socket(string itemToSocket, string socket)
        {
            IItem itemSocket = this.TakeAway(socket);
            IItem itemSocketed = this.TakeAway(itemToSocket);
            if (itemSocket != null)
            {
                if (itemSocketed != null)
                {
                    try
                    {
                        itemSocketed.AddDecorator(itemSocket);
                        this.Give(itemSocketed);
                    }
                    catch (Exception ex) { }
                }
            }
            else 
            {
                this.OutputMessage("\nSocket unsuccessful.\n");
            }
        }
        public void Unlock(string direction)
        {
            Door door = CurrentRoom.GetExit(direction);
            ILockable lock1 = door.InstalledLock;
            IItem item = this.TakeAway(lock1.KeyName);
            this.Give(item);
            if (item != null)
            {
                if (door != null)
                {

                    if (door.IsLocked && door.HasLock() && !door.IsOpen)
                    {

                        door.Unlock();
                        if (direction == "up")
                        {
                            this.OutputMessage("\nYou hear a small click as you unlock the door above you.\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nYou hear a small click as you unlock the door below you.\n");
                            return;
                        }
                        else
                        {
                            OutputMessage("\nYou hear a small click as you unlock the door to the " + direction + ".\n");
                        }
                    }
                    else if (door.IsUnlocked)
                    {
                        if (direction == "up")
                        {
                            this.OutputMessage("\nThe door above you is already unlocked..\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nThe door below you is already unlocked.\n");
                            return;
                        }
                        else
                        {
                            OutputMessage("\nThe door to the " + direction + " is already unlocked.\n");
                        }
                    }
                    else
                    {
                        if (direction == "up")
                        {
                            this.OutputMessage("\nThe door above you cannot be unlocked..\n");
                            return;
                        }
                        else if (direction == "down")
                        {
                            this.OutputMessage("\nThe door below you cannot be unlocked.\n");
                            return;
                        }
                        else
                        {
                            OutputMessage("\nThe door to the " + direction + " cannot be unlocked.\n");
                        }
                    }
                }
                else
                {
                    if (direction == "up")
                    {
                        this.OutputMessage("\nThere is no door above you.\n");
                        return;
                    }
                    else if (direction == "down")
                    {
                        this.OutputMessage("\nThere is no door below you.\n");
                        return;
                    }
                    else
                    {
                        this.OutputMessage("There is no door to the " + direction + ".\n");
                    }
                }
            }
            else 
            {
                this.OutputMessage("\nYou do not have the key.\n");
            }
        }
        public void WalkTo(string direction)
        {
            Room room = this.CurrentRoom;
            _rooms.Push(room);
            Door door = this.CurrentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {
                    Room nextRoom = door.GetRoomOnTheOtherSide(this.CurrentRoom);
                    Notification notification = new Notification("PlayerWillLeaveRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.CurrentRoom = nextRoom;
                    this.OutputMessage("\n" + this.CurrentRoom.Description());
                    notification = new Notification("PlayerEnteredRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                }
                else
                {
                    if (direction == "up")
                    {
                        this.OutputMessage("\nThe door above you is closed.\n");
                    }
                    else if (direction == "down")
                    {
                        this.OutputMessage("\nThe door below you is closed.\n");
                    }
                    else
                    {
                        this.OutputMessage("\nThe door to the " + direction + " is closed.\n");
                    }
                }
            }
            else
            {
                this.OutputMessage("\nThere is no door to the " + direction + ".\n");
            }
        }
        public void Wear(string itemName)
        {
            IItem item = _inventory.Remove(itemName);            
            if(item != null) {
                _equipment.Add(item);
                if (item.HitRoll > 0)
                {
                    this.HitRoll += item.HitRoll;
                }
                string damageType = item.DamageType;
                this.DamageType = damageType;
            }
        }
    }
}