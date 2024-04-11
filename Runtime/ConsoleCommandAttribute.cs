using System;

namespace RB.Console
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ConsoleCommandAttribute : Attribute
    {
        private readonly string _commandName;

        public ConsoleCommandAttribute(string name)
        {
            _commandName = name;
        }

        public string CommandName
        {
            get
            {
                return _commandName;
            }
        }
    }
}