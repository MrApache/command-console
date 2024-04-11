using System;

namespace RB.Console
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string command)
            :base($"Command '{command}' not found")
        {}
    }
}