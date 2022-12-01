using System.Collections;
using System.Collections.Generic;
using System;

namespace TheEntropicLands
{
    public class Parser
    {
        private readonly CommandWords _commands;
        public Parser() : this(new CommandWords()){}
        // Designated Constructor
        public Parser(CommandWords newCommands)
        {
            _commands = newCommands;
        }
        public Command ParseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length != 0)
            {
                command = _commands.Get(words[0]);
                if (command != null)
                {
                    if (command.Name == "say" ||
                        command.Name == "'" ||
                        command.Name == "exa" ||
                        command.Name == "get" ||
                        command.Name == "take" ||
                        command.Name == "drop" ||
                        command.Name == "quaff"||
                        command.Name == "qua") 
                    {
                        for (int i = 1; i < words.Length; i++)
                        {
                            if (i == words.Length - 1)
                            {
                                command.Phrase += words[i];
                            }
                            else if (command != null && i != words.Length)
                            {
                                command.Phrase += words[i] + " ";
                            }
                        }
                    }
                    if (words.Length == 2)
                    {
                        command.SecondWord = words[1];
                    }
                    else if (words.Length == 3)
                    {
                        command.SecondWord = words[1];
                        command.ThirdWord = words[2];
                    }
                    else if(words.Length == 4) 
                    {
                        command.SecondWord = words[1];
                        command.ThirdWord = words[2];
                        command.FourthWord = words[3];
                    }
                    else
                    {
                        command.SecondWord = null;
                        command.ThirdWord = null;
                        command.FourthWord = null;
                    }
                }
                /*else
                {
                    // This is debug line of code, should remove for regular execution
                    //Console.WriteLine(">>>Did not find the command " + words[0]);
                }*/
            }
            /*else
            {
                // This is a debug line of code
                Console.WriteLine("No words parsed!");
            }*/
            return command;
        }
        public string Description()
        {
            return _commands.Description();
        }
    }
}