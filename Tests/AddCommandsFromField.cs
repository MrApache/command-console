using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class AddCommandsFromField
    {
        [Test]
        public void Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Example>();

            Console.Init();

            Assert.IsTrue(Console.CommandExists(FieldExample.FIELD_COMMAND));
            Assert.IsTrue(Console.Execute(FieldExample.FIELD_COMMAND));

            Console.Reset();
        }
    }
}
