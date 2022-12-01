using System.Collections;
using System.Collections.Generic;
using System;

namespace TheEntropicLands
{
    public class Room
    {
        private readonly Dictionary<string, Door> _doors;

        private readonly Dictionary<string, NPC> _npcs;

        private string _tag;
        public string Tag { get { return _tag; } set { _tag = value; } }

        private ItemContainer _itemContainer;
        public void Drop(IItem item)
        {
            _itemContainer.Add(item);
        }
        public IItem Remove(string itemName)
        {
            return _itemContainer.Remove(itemName);
        }

        private IRoomDelegate _delegate;
        public IRoomDelegate Delegate
        {
            set
            {
                _delegate = value;
                if (value != null)
                {
                    _delegate.ContainingRoom = this;
                    _delegate.ContainingRoomExits = _doors;
                    _delegate.ContainingRoomNPCS = _npcs;
                }
            }
            get
            {
                return _delegate;
            }
        }
        public Room() : this("No Tag") { }
        // Designated Constructor
        public Room(string tag)
        {
            Delegate = null;
            _doors = new Dictionary<string, Door>();
            _npcs = new Dictionary<string, NPC>();
            this.Tag = tag;
            _itemContainer = new ItemContainer("", 0, 0, "container");

        }
        public void SetNPC(string npcName, NPC npc) 
        {
            _npcs[npcName] = npc;
        }
        public NPC GetNPC(string NPCName)
        {
            if (this.Delegate == null)
            {
                NPC npc = null;
                _npcs.TryGetValue(NPCName, out npc);
                return npc;
            }
            else
            {
                return Delegate.GetNPC(NPCName);
            }
        }
        public string GetNPCS()
        {
            if (Delegate == null)
            {
                string npcNames = "";
                Dictionary<string, NPC>.KeyCollection keys = _npcs.Keys;
                foreach (string npcName in keys)
                {
                    NPC npc = GetNPC(npcName);
                    if (npc.HealthPoints > 0)
                    {

                        npcNames += "\n\n" + npcName + " is here, staring at you with dead eyes.";
                    }
                }
                return npcNames;
            }
            else
            {
                return Delegate.GetNPCS();
            }
        }
        public void SetExit(string exitName, Door door)
        {
            if (door != null)
            {
                _doors[exitName] = door;
            }
            else
            {
                _doors.Remove(exitName);
            }
        }
        public Door GetExit(string exitName)
        {
            if (this.Delegate == null)
            {
                Door door = null;
                _doors.TryGetValue(exitName, out door);
                return door;
            }
            else
            {
                return Delegate.GetExit(exitName);
            }
        }
        public string GetExits()
        {
            if (Delegate == null)
            {
                string exitNames = "exits:";
                Dictionary<string, Door>.KeyCollection keys = _doors.Keys;
                foreach (string exitName in keys)
                {
                    Door door = GetExit(exitName);
                    if (door.IsClosed)
                    {

                        exitNames += " (" + exitName + ")";
                    }
                    else 
                    {
                        exitNames += " " + exitName;
                    }
                }
                return exitNames;
            }
            else
            {
                return Delegate.GetExits();
            }
        }
        public string Description()
        {
            if (Delegate == null)
            {
                return this.Tag + "\n\n" + this.GetExits() + this.GetNPCS() + "\n" + this._itemContainer.Description;
            }
            else
            {
                return Delegate.Description();
            }
        }
        public bool HasExit(string direction) 
        {

            return false;
        }
    }
    public class TrapRoom : IRoomDelegate
    {
        private readonly string unlockWord;
        public Room ContainingRoom { set; get; }

        private Dictionary<string, Door> _containingRoomExits;

        private Dictionary<string, Item> _containingRoomItems;

        private Dictionary<string, NPC> _containingRoomNPCS;
        public Dictionary<string, Door> ContainingRoomExits { set { _containingRoomExits = value; } } //copy to prevent unauthorized access
        public Dictionary<string, Item> ContainingRoomItems { set { _containingRoomItems = value;} }

        public Dictionary<string, NPC> ContainingRoomNPCS {  set {  _containingRoomNPCS = value;} }

        public TrapRoom() : this("test") { }
        //Designated Constructor
        public TrapRoom(string theWord) 
        {
            unlockWord = theWord;
            NotificationCenter.Instance.AddObserver("PlayerSaidAWord", PlayerSaidAWord); //subscribes to notification
        }
        public Door GetExit(string exitName) 
        {
            return null;
        }
        
        public string GetExits() 
        {
            return "\nA cacophony of eerie laughter fills your ears.\n";
        }
        public string Description() 
        {
            return ContainingRoom.Tag + "\n\n" + ContainingRoom.GetExits();
            
        }
        public void PlayerSaidAWord(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (ContainingRoom == player.CurrentRoom)
            {
                Dictionary<string, Object> userInfo = notification.UserInfo;
                string word = (string)userInfo["word"];
                if (word.Equals(unlockWord)) 
                {
                    ContainingRoom.Delegate = null;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    player.OutputMessage("\nSuccess! You are free.\n");
                    Console.ResetColor();
                    player.OutputMessage("\n" + ContainingRoom.Description());
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    player.OutputMessage("\nAh, ah, ah.. you didn't say the magic word!\n");
                    Console.ResetColor();
                }
            }
        }

        public Item GetItem(string itemName)
        {
            throw new NotImplementedException();
        }

        public string GetItems()
        {
            throw new NotImplementedException();
        }

        public string GetNPCS()
        {
            throw new NotImplementedException();
        }

        public NPC GetNPC(string npcName)
        {
            throw new NotImplementedException();
        }
    }
    public class EchoRoom : IRoomDelegate
    {
        public EchoRoom()
        {
            NotificationCenter.Instance.AddObserver("PlayerSaidAWord", PlayerSaidAWord); //subscribes to notification
        }
        public Room ContainingRoom { get; set; }
        private Dictionary<string, Door> _containingRoomExits { set; get; }
        public Dictionary<string, Door> ContainingRoomExits { set { _containingRoomExits = value; } }
        private Dictionary<string, Item> _containingRoomItems { set; get; }
        public Dictionary<string, Item> ContainingRoomItems { set { _containingRoomItems = value;  } }

        private Dictionary<string, NPC> _containingRoomNPCS;
        public Dictionary<string, NPC> ContainingRoomNPCS { set { _containingRoomNPCS = value; } }

        public string Description()
        {
            return ContainingRoom.Tag + "\n\n" + this.GetExits();
        }
        public Door GetExit(string exitName)
        {
            _containingRoomExits.TryGetValue(exitName, out Door door);
            
            return door;
        }
        public string GetExits()
        { 
            string exitNames = "exits: ";
            Dictionary<string, Door>.KeyCollection keys = _containingRoomExits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }
            return exitNames + "\n"; 
        }
        public string GetNPCS() 
        {
            string NPCNames = "";
            Dictionary<string, Door>.KeyCollection keys = _containingRoomExits.Keys;
            foreach (string NPCName in keys)
            {
                NPCNames += " " + NPCName;
            }
            return NPCNames + "\n";
        }
        public Item GetItem(string itemName)
        {
            Item item = null;
            _containingRoomItems.Remove(itemName);

            return item;
        }
        public string GetItems()
        {
            string itemNames = "Items: ";
            Dictionary<string, Item>.KeyCollection keys = _containingRoomItems.Keys;
            foreach (string itemName in keys)
            {
                itemNames += " " + itemName;
            }
            return itemNames;

        }
        public void PlayerSaidAWord(Notification notification) 
        {
            Player player = (Player)notification.Object;
            if (ContainingRoom == player.CurrentRoom)
            {
                Dictionary<string, Object> userInfo = notification.UserInfo;
                string word = (string)userInfo["word"];
                Console.ForegroundColor = ConsoleColor.Cyan;
                player.OutputMessage("\n" + word + "..." + word + "..." + word + "\n");
                Console.ResetColor();
            }
        }

        public NPC GetNPC(string npcName)
        {
            throw new NotImplementedException();
        }
    }
}