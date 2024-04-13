using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RB.Console
{
    public static class Console
    {
        private readonly static Dictionary<string, Command> _commands;
        private readonly static CommandFinder _finder;

        static Console()
        {
            _commands = new Dictionary<string, Command>();
            _finder = new CommandFinder();
            SceneManager.activeSceneChanged += (_,_) => Init();
            Init();
        }

        public static IEnumerable<Command> Commands
        {
            get
            {
                return _commands.Values;
            }
        }

        public static void Init()
        {
            Reset();
            RegisterCommands(_finder.GetCommands());
        }

        public static void RegisterCommands<T>(T target)
        {
            RegisterCommands(_finder.GetCommandsFromObject(target));
        }

        public static void RemoveCommands<T>(T target)
        {
            RemoveCommands(_finder.GetCommandsFromObject(target));
        }

        private static void RegisterCommands(IEnumerable<Command> commands)
        {
            foreach (Command command in commands)
            {
                _commands[command.Name] = command;
            }
        }

        private static void RemoveCommands(IEnumerable<Command> commands)
        {
            foreach (Command command in commands)
            {
                _commands.Remove(command.Name);
            }
        }

        public static bool RemoveCommand(string command)
        {
            return _commands.Remove(command);
        }

        public static void Reset()
        {
            _commands.Clear();
        }

        public static bool Execute(string command, params object[] parameters)
        {
            if (_commands.ContainsKey(command))
            {
                return _commands[command].Execute(parameters);
            }
            else
            {
                Debug.LogError($"Command '{command}' not found");
                return false;
            }
        }

        public static bool CommandExists(string command)
        {
            return _commands.ContainsKey(command);
        }
    }
}