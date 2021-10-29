using System;
using Sys = Cosmos.System;

namespace DogOS.Shell.Commands.Power
{
    class Reboot : BaseCommand
    {
        public Reboot(string[] command_values) : base(command_values)
        {
            Description = Utils.Ini.Read(Kernel.lang_file, "Reboot", "description");
        }

        public override ReturnInfo Execute()
        {
            Kernel.running = false;
            Console.Clear();
            Console.WriteLine(Utils.Ini.Read(Kernel.lang_file, "Reboot", "execute_text").Replace("{OS_NAME}", Kernel.os_name));
            Console.Beep();
            Sys.Power.Reboot();
            return new ReturnInfo(this, ReturnCode.Ok);
        }
    }
}
