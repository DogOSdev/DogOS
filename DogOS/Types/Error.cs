using System;

namespace DogOS.Types
{
    public class Error
    {
        public string Name { get; private set; }
        public string Details { get; private set; }

        public Error(string name, string details)
        {
            Name = name;
            Details = details;
        }

        public override string ToString()
        {
            return $"{Name}: {Details}";
        }
    }
}