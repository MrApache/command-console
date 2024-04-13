using UnityEngine;

namespace RB.Console.Tests
{
    public class Example : MonoBehaviour
    {
        public const string TEST_COMMAND_WITH_PARAMS = "test_command";
        public const string FOO_COMMAND = "foo_command";
        public const string BAR_COMMAND = "bar_command";
        public const string LOAD_COMMANDS = "load_commands";
        public const string TEST_METHOD_A = "test_method_a";

        private FieldExample _example = new FieldExample();

        [ConsoleCommand(TEST_COMMAND_WITH_PARAMS)]
        public static void Test(string message, int num)
        {
            Debug.Log($"{message}, {num}");
        }

        [ConsoleCommand(FOO_COMMAND)]
        public void Foo()
        {
            Debug.Log("foo_command");
        }

        [ConsoleCommand(BAR_COMMAND)]
        public void Bar()
        {
            Debug.Log("Bar");
        }

        [ConsoleCommand(LOAD_COMMANDS)]
        private static void Init()
        {
            Console.Init();

        }

        [ConsoleCommand(TEST_METHOD_A)]
        private static void AnotherTest()
        {
            Debug.Log("another test method");
        }
    }
}