using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class RemoveCommandsTest
    {
        [Test]
        public void RemoveCommands()
        {
            GameObject gameObject = new GameObject();
            var example = gameObject.AddComponent<Example>();

            Console.Init();
            Console.RemoveCommands(example);

            Assert.IsFalse(Console.CommandExists(Example.TEST_COMMAND_WITH_PARAMS));
            Assert.IsFalse(Console.CommandExists(Example.FOO_COMMAND));
            Assert.IsFalse(Console.CommandExists(Example.BAR_COMMAND));
            Assert.IsFalse(Console.CommandExists(Example.LOAD_COMMANDS));
            Assert.IsFalse(Console.CommandExists(Example.TEST_METHOD_A));

            Console.Reset();
        }
    }
}
