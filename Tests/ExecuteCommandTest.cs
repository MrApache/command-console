using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class ExecuteCommandTest
    {
        [Test]
        public void Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Example>();
            Console.Init();
            Assert.IsTrue(Console.Execute(Example.TEST_COMMAND_WITH_PARAMS, "test runner", 42));
            Console.Reset();
        }
    }
}
