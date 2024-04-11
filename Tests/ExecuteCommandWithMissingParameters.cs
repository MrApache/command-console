using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RB.Console.Tests
{
    public class ExecuteCommandWithMissingParameters
    {
        [Test]
        public void Test()
        {
            LogAssert.ignoreFailingMessages = true;
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Example>();
            Console.Init();
            Assert.IsFalse(Console.Execute(Example.TEST_COMMAND_WITH_PARAMS, "test runner"));
            Console.Reset();
        }
    }
}