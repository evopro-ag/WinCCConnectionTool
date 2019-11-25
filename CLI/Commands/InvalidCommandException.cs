using System;
using System.Linq;

namespace CLI.Commands
{
    public class InvalidCommandException : Exception
    {
        //Exception which Stops CLI execution and display help text
        public InvalidCommandException(string message) :base(message)
        {

        }
    }
}