using System;

namespace DogOS.Shell
{
    public enum CommandResultCode
    {
        Success = 0,
        Error = 1
    }

    public class CommandResult
    {
        public CommandResultCode Code { get; private set; }
        public Types.Error Error { get; private set; }

        public CommandResult(CommandResultCode code, Types.Error error)
        {
            Code = code;
            Error = error;
        }
    }
}