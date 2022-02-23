namespace DogOS.Shell
{
    public enum CommandResultCode
    {
        Success = 0,
        Error = 1,
        CriticalError = 2
    }

    public class CommandResult
    {
        public CommandResultCode Code { get; protected set; }
        public Types.Error Error { get; protected set; }

        public dynamic Result { get; protected set; }

        protected CommandResult(CommandResultCode code)
        {
            Code = code;
            Result = null;
            Error = null;
        }

        protected CommandResult(CommandResultCode code, dynamic result)
        {
            Code = code;
            Result = result;
            Error = null;
        }

        protected CommandResult(CommandResultCode code, Types.Error error)
        {
            Code = code;
            Error = error;
            Result = null;
        }

        public bool IsError()
        {
            return Error != null;
        }

        public static CommandResult Success()
        {
            return new CommandResult(CommandResultCode.Success);
        }

        public static CommandResult Success(dynamic result)
        {
            return new CommandResult(CommandResultCode.Success, result);
        }

        public static CommandResult Failure(Types.Error error)
        {
            return new CommandResult(CommandResultCode.Error, error);
        }

        public static CommandResult Critical(Types.Error error)
        {
            return new CommandResult(CommandResultCode.CriticalError, error);
        }
    }
}