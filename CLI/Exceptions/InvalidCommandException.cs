using System;

namespace CLI.Exceptions
{
    public class InvalidCommandException : Exception
    {
        //Exception which Stops CLI execution and display help text
        public InvalidCommandException(string message) :base(message)
        {

        }
    }
}