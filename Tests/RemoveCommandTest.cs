using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class RemoveCommandTest 
    {
        [Test]
        public void RemoveCommands()
        {
            AddCommands();
            Console.RemoveCommand(Example.TEST_COMMAND_WITH_PARAMS);
            Console.RemoveCommand(Example.FOO_COMMAND);
            Console.RemoveCommand(Example.BAR_COMMAND);

            Assert.IsFalse(Console.CommandExists(Example.TEST_COMMAND_WITH_PARAMS));
            Assert.IsFalse(Console.CommandExists(Example.FOO_COMMAND));
            Assert.IsFalse(Console.CommandExists(Example.BAR_COMMAND));
            Assert.IsTrue(Console.CommandExists(Example.LOAD_COMMANDS));
            Assert.IsTrue(Console.CommandExists(Example.TEST_METHOD_A));
            Console.Reset();
        }

        public void AddCommands()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Example>();
            Console.Init();
        }
    }
}
