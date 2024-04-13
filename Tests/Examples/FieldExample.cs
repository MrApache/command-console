using UnityEngine;

namespace RB.Console.Tests
{
    public class FieldExample
    {
        public const string FIELD_COMMAND = "field_command";
        
        [ConsoleCommand(FIELD_COMMAND)]
        public void Foo()
        {
            Debug.Log("Field command");
        }
    }
}