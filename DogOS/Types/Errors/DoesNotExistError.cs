using System;

namespace DogOS.Types.Errors
{
    public class DoesNotExist : Error
    {
        public DoesNotExist(string details) : base("Does not exist", details) {  }
    }
}