using System;

namespace DogOS.Types.Errors
{
    public class InvalidOption : Error
    {
        public InvalidOption(string details) : base("Invalid Option", details) {  }
    }
}