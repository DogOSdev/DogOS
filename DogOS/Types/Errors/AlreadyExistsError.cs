using System;

namespace DogOS.Types.Errors
{
    public class AlreadyExists : Error
    {
        public AlreadyExists(string details) : base("Already Exists", details) {  }
    }
}