using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace RB.Console.Tests
{
    public class ExecuteNotExistsCommandTest
    {
        [Test]
        public void Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Example>();
            Console.Init();

            LogAssert.ignoreFailingMessages = true;
            Assert.IsFalse(Console.Execute("kafjsfs"));

            Console.Reset();
        }
    }
}
