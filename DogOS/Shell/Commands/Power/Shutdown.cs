using System;
using Sys = Cosmos.System;

namespace DogOS.Shell.Commands.Power
{
    public class Shutdown : BaseCommand
    {
        public Shutdown(string[] command_values) : base(command_values)
        {
            Description = Utils.Ini.Read(Kernel.lang_file, "Shutdown", "description");
        }

        public override ReturnInfo Execute()
        {
            Kernel.running = false;
            Console.Clear();
            Console.WriteLine(Utils.Ini.Read(Kernel.lang_file, "Shutdown", "execute_text").Replace("{OS_NAME}", Kernel.os_name));
            Console.Beep();
            Sys.Power.Shutdown();
            return new ReturnInfo(this, ReturnCode.Ok);
        }
    }
}
