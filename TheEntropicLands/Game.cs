using System;

namespace TheEntropicLands
{
    public class Game //Game Loop Pattern
    {
        private readonly Player _player;
        private readonly Parser _commandParser;
        private bool _playing;
        private GameClock _clock;
        public int Time { get => _clock.TimeInGame; }
        public Game()
        {
            _clock = new GameClock(30000);
            _playing = false;
            _commandParser = new Parser(new CommandWords());
            _player = new Player(GameWorld.Instance().Entrance, "Djet", Player.RACE.HUMAN);
            NotificationCenter.Instance.AddObserver("GameClockTick", GameClockTick);
        }
        public void GameClockTick(Notification notification)
        {
            //impelements 24-hour clock using GameClock object
            _clock.TimeInGame++;
            _player.Regen(); //couple regen with time
            if(_clock.TimeInGame == 24)
            {
                _clock.TimeInGame = 0; // 0 == midnight
            }           
            //_player.OutputMessage("\nTick!\n");
        }
        /**
     *  Main play routine.  Loops until end of play.
     */
        public void Play()
        {
            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            bool finished = false;
            while (!finished)
            {
                Console.Write(_player.Name + ":\n"); //temporary hacky way to do prompt coloring 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_player.HealthPoints);
                Console.ResetColor();
                Console.Write("hp/");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(_player.ManaPoints);
                Console.ResetColor();
                Console.Write("mp/");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(_player.Currency);
                Console.ResetColor();
                Console.Write("gold/" + _clock.TimeInGame + "\n\n");
                Command command = _commandParser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    Console.WriteLine("\nHuh?\n");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }
        //Methods for TODO: game persistence
        public void Save() { }
        public void Load() { }
        public void Start()
        {
            _playing = true;
            _player.OutputMessage(Welcome());
        }
        public void End()
        {
            Save();
            _playing = false;
            _player.OutputMessage(Goodbye());
        }
        public string Welcome()
        {
            return "Welcome to the Entropic Lands!\n\n" + _player.CurrentRoom.Description();
        }
        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }
    }
}
