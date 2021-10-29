using System;
using System.Collections.Generic;
using System.Text;

// Heavily based on Aura OS

namespace DogOS.Shell.Commands
{
    public enum CommandType
    {
        Unknown = -1,
        Files,
        Utils
    }

    public enum ReturnCode
    {
        Exception,
        Error,
        Argument,
        Crash,
        Ok
    }

    public class ReturnInfo
    {
        private BaseCommand command;
        internal BaseCommand Command
        {
            get { return command; }
        }

        private ReturnCode code;
        internal ReturnCode Code
        {
            get { return code; }
        }

        private string info;
        internal string Info
        {
            get { return info; }
        }

        public ReturnInfo(BaseCommand command, ReturnCode code, string info = "Unknown Error.")
        {
            this.command = command;
            this.code = code;
            this.info = info;
        }
    }

    public class BaseCommand
    {
        public CommandType Type { get; set; }

        public string Description { get; set; }

        public string[] CommandValues;

        public BaseCommand(string[] command_values, CommandType type = CommandType.Unknown)
        {
            CommandValues = command_values;
            Type = type;
            Description = "Unknown";
        }

        public virtual ReturnInfo Execute()
        {
            return new ReturnInfo(this, ReturnCode.Argument);
        }

        public virtual ReturnInfo Execute(List<string> arguments)
        {
            return new ReturnInfo(this, ReturnCode.Argument);
        }

        public virtual void PrintHelp()
        {
            Console.WriteLine("No help infomation is available for this command.");
        }

        public bool ContainsCommand(string command)
        {
            foreach (string commandvalue in CommandValues)
            {
                if(commandvalue == command)
                {
                    return true;
                }
            }
            return false;
        }

        public string CommandStarts(string cmd_to_complete)
        {
            foreach (string value in CommandValues)
            {
                if (value.StartsWith(cmd_to_complete))
                {
                    return value;
                }
            }
            return null;
        }
    }
}
