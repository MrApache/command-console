using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class AddCommandsOnRuntimeTest
    {
        [Test]
        public void AddCommandsTest()
        {
            GameObject gameObject = new GameObject();
            var example = gameObject.AddComponent<Example>();
            Console.RegisterCommands(example);
            Assert.IsTrue(Console.CommandExists(Example.TEST_COMMAND_WITH_PARAMS));
            Assert.IsTrue(Console.CommandExists(Example.FOO_COMMAND));
            Assert.IsTrue(Console.CommandExists(Example.BAR_COMMAND));
            Assert.IsTrue(Console.CommandExists(Example.LOAD_COMMANDS));
            Assert.IsTrue(Console.CommandExists(Example.TEST_METHOD_A));
            Console.Reset();
        }
    }
}