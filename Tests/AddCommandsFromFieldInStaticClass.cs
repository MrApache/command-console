using NUnit.Framework;

namespace RB.Console.Tests
{
    public class AddCommandsFromFieldInStaticClass
    {
        [Test]
        public void Test()
        {
            Console.Init();

            Assert.IsTrue(Console.CommandExists(FieldExample.FIELD_COMMAND));
            Assert.IsTrue(Console.Execute(FieldExample.FIELD_COMMAND));

            Console.Reset();
        }
    }
}