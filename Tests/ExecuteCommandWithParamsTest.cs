using NUnit.Framework;
using UnityEngine;

namespace RB.Console.Tests
{
    public class ExecuteCommandWithParamsTest
    {
        [Test]
        public void Test()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<Example>();
            Console.Init();
            Assert.IsTrue(Console.Execute(Example.BAR_COMMAND));
            Console.Reset();
        }
    }
}