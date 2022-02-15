using System;

namespace DogOS.Types.Errors
{
    public class NotEnoughArguments : Error
    {
        public NotEnoughArguments(string details) : base("Not enough arguments", details) {  }
    }
}