using System;

namespace TheEntropicLands
{
    public class GameWorld //Singleton Pattern
    {
        //only instance of world
        private static GameWorld _instance;
        //global access to GameWorld
        public static GameWorld Instance() 
        {
            if (_instance == null)
            { 
                _instance = new GameWorld(); 
            }
            return _instance; 
        }
        private Room _entrance;

        public Room Entrance { get { return _entrance; } }

        private Room _vortex;
        private Room _trigger;
        private Room _victory;
        private Room _trap;
        private static Room _underworld;

        public static Room Underworld { get { return _underworld; } }
        public Room Vortex { get { return _vortex; } }
        private GameWorld()
        {
            _entrance = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerWillEnterRoom", PlayerWillEnterRoom); //subscription 
            NotificationCenter.Instance.AddObserver("PlayerEnteredRoom", PlayerEnteredRoom);
            NotificationCenter.Instance.AddObserver("PlayerPickedUpItem", PlayerPickedUpItem);
        }
        public void PlayerPickedUpItem(Notification notification) 
        { 
            Player player = (Player)notification.Object;
        }
        public void PlayerWillEnterRoom(Notification notification) 
        {
            Player player = (Player)notification.Object;
            if(player.CurrentRoom == _vortex) 
            {
                Door door = player.CurrentRoom.GetExit("vortex");
                if(door != null) 
                {
                    player.CurrentRoom.SetExit("vortex", null);
                }
            }
        }
        public void PlayerEnteredRoom(Notification notification) 
        {
            Player player = (Player)notification.Object;
            player.Entrance = _entrance;  
            if (player.CurrentRoom == _trigger)
            {
                Door door = Door.CreateDoor(_vortex, _trap, "vortex");
                _vortex.SetExit("vortex", door);
                player.SuccessMessage("A new exit has been made!\n");               
            }            
            if(player.CurrentRoom == _victory) 
            {
                player.VictoryMessage("You win!");
            }
        }

        private Room CreateWorld()
        {
            Room hallway = new Room("The hallway continues.. it is dark.");
            Room hallway1 = new Room("The hallway continues.. it is dark.");
            Room hallway2 = new Room("The hallway continues.. it is dark.");
            Room hallway3 = new Room("The hallway continues.. it is dark.");
            Room hallway4 = new Room("The hallway entrance. It is dark.");
            Room hallway5 = new Room("The hallway continues.. it is dark.");
            Room outside = new Room("Outside the tombs.");
            Room theSepulcre = new Room("The center-most room in the tombs. Something strange lies in the middle.");
            Room entrance = new Room("The entrance to the tombs.");
            Room aSmallTomb = new Room("A small tomb.");
            Room aSmallTomb2 = new Room("A small tomb.");
            Room anEerieRoom = new Room("An eerie room with strange, echoing voices.");
            Room tortureChamber = new Room("A cramped torture chamber.");
            Room prisonCell = new Room("A small prison cell.");
            Room behindTheCell = new Room("In a cramped room behind the prison cell.");
            Room nightmare = new Room("The nightmare. Your thoughts are racing and the exit always seems to be just around the corner.\n" +
                "Some partially visible letters have been scraped into the wall: \n" +
                "S.y 'h..p!' t. ge. o.t!");
            Room Hell = new Room("You are trapped in the Underworld.");

            DeadBoltLock entranceLock = new DeadBoltLock();
            Item exitKey = new Item("skeleton key", 0, 0, "key");
            entranceLock.SetKey(exitKey);
            Item cellKey = new Item("cell key", 0, 0, "key");
            DeadBoltLock prisonLock = new DeadBoltLock();
            prisonLock.SetKey(cellKey);

            Door door = Door.CreateDoor(theSepulcre, hallway1, "north", "south", false);

            door = Door.CreateDoor(theSepulcre, hallway4, "south", "north", false);

            door = Door.CreateDoor(hallway3, hallway, "north", "south", false);

            door = Door.CreateDoor(hallway, entrance, "up", "down", true);
            door.InstallLock(entranceLock);
            door.Close();
            door.Lock();

            door = Door.CreateDoor(entrance, outside, "east", "west", false);

            door = Door.CreateDoor(theSepulcre, aSmallTomb, "east", "west", false);

            door = Door.CreateDoor(theSepulcre, aSmallTomb2, "west", "east", false);

            door = Door.CreateDoor(hallway1, hallway2, "north", "south", false);

            door = Door.CreateDoor(hallway2, hallway3, "north", "south", false) ;

            door = Door.CreateDoor(hallway4, hallway5, "south", "north", false);

            door = Door.CreateDoor(hallway5, anEerieRoom, "south", "north", false);

            door = Door.CreateDoor(anEerieRoom, tortureChamber, "west", "east", true);

            door = Door.CreateDoor(anEerieRoom, prisonCell, "east", "west", true);
            door.Close();
            door.InstallLock(prisonLock);
            door.Lock();

            door = Door.CreateDoor(prisonCell, behindTheCell, "east", "west", true);
            door.Close();

            door = Door.CreateDoor(behindTheCell, nightmare, "east", "west", true);
            door.Close();

            _trigger = behindTheCell;
            _victory = outside;
            _vortex = tortureChamber;
            _trap = nightmare;
            _underworld = Hell;
            //set the Delegates
            nightmare.Delegate = new TrapRoom("help!");
            //tortureChamber.Delegate = new EchoRoom();

            // create items
            Potion healingPotion = new Potion("healing potion", .5f, .5f, "healing", 2, 50, 50);
            Potion redPotion = new Potion("red potion", .5f, .5f, "healing", 2, 50, 50);
            Item sarcophagus = new Item("large sarcophagus", 100, 10000, "container");
            Weapon sword = new Weapon("sword", .5f, 10000, "weapon", "normal");
            Socket diamond = new Socket("diamond", .5f, 10000, "socket", "holy");
            Item dagger = new Item("dagger", 1, 10000, "weapon");
            Weapon flail = new Weapon("flail", 2, 10000, "weapon", "holy");
            Weapon dagger2 = new Weapon("dagger", 1, 10000, "weapon", "normal");

            // create NPCs

            NPC vampireLord = NPC.CreateNPC(theSepulcre, "Vlad", "vampire");
            vampireLord.Give(exitKey);
            NPC torturer = NPC.CreateNPC(tortureChamber, "Torturer", "vampire");
            torturer.Give(diamond);
            theSepulcre.Drop(sarcophagus);
            nightmare.Drop(flail);
            behindTheCell.Drop(cellKey);
            aSmallTomb.Drop(sword);
            aSmallTomb2.Drop(dagger);
            aSmallTomb2.Drop(dagger2);
            behindTheCell.Drop(healingPotion);
            tortureChamber.Drop(redPotion);

            return prisonCell;
        }
    }
}