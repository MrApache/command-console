using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class AddCommandsWithoutMonoBehaviour
    {
        [Test]
        public void Test()
        {
            Console.RegisterCommands(this);

            Assert.IsTrue(Console.CommandExists("foo_command"));
            Console.Execute("foo_command");
        }

        [ConsoleCommand("foo_command")]
        public void Foo()
        {
            Debug.Log("Foo command");
        }
    }
}
