using System.Collections;
using System.Collections.Generic;
using System;

namespace TheEntropicLands
{
    public class CommandWords
    {
        private readonly Dictionary<string, Command> _commands;

        private readonly static Command[] commandArray =
            {
                new GoCommand(),
                new QuitCommand(),
                new SayCommand(),
                new OpenDoorCommand(),
                new CloseDoorCommand(),
                new LockCommand(),
                new DigCommand(),
                new UnlockCommand(),
                new CreateRoomCommand(),
                new ExamineCommand(),
                new LookCommand(),
                new PickUpCommand(),
                new DropCommand(),
                new ScoreCommand(),
                new EquipmentCommand(),
                new InventoryCommand(),
                new AttackCommand(),
                new SocketCommand(),
                new RemoveCommand(),
                new BackCommand(),
                new QuaffCommand(),
                new WearCommand(),
            };

        public CommandWords() : this(commandArray) {}

        // Designated Constructor
        public CommandWords(Command[] commandList)
        {
            _commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                _commands[command.Name] = command;
                _commands[command.SecondaryName] = command;
            }
            Command help = new HelpCommand(this);
            _commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            _commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = _commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
