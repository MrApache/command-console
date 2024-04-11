using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RB.Console
{
    public static class Console
    {
        private readonly static Dictionary<string, Command> _commands;
        private const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        static Console()
        {
            _commands = new Dictionary<string, Command>();
            SceneManager.activeSceneChanged += (_,_) => Init();
            Init();
        }

        public static void Init()
        {
            Reset();
            FindStaticMethods();
            FindAttributes();
        }

        public static void RegisterCommands<T>(T target)
        {
            var methods = FindAllMethods(target);
            foreach (var method in methods)
            {
                var attrb = method.GetCustomAttribute<ConsoleCommandAttribute>(true);
                var command = new Command(target, attrb.CommandName, method);
                _commands[command.Name] = command;
            }
        }

        public static void RemoveCommands<T>(T target)
        {
            var methods = FindAllMethods(target);
            foreach (var method in methods)
            {
                var attrb = method.GetCustomAttribute<ConsoleCommandAttribute>(true);
                _commands.Remove(attrb.CommandName);
            }
        }

        public static void RemoveCommand(string command)
        {
            _commands.Remove(command);
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

        public static IEnumerable<Command> GetAllCommands()
        {
            return _commands.Values;
        }

        public static bool CommandExists(string command)
        {
            return _commands.ContainsKey(command);
        }

        private static IEnumerable<MethodInfo> FindAllMethods(object target)
        {
            return target.GetType()
                .GetMethods(FLAGS)
                .Where(m => m.GetCustomAttribute<ConsoleCommandAttribute>(true) != null);
        }

        private static void FindStaticMethods()
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.IsAbstract && t.IsSealed);
            foreach (var type in types)
            {
                RegisterCommands(type);
            }
        }

        private static void FindAttributes()
        {
            var components = GetAllComponents();
            foreach (var component in components)
            {
                RegisterCommands(component);
            }
        }

        private static IEnumerable<Component> GetAllComponents()
        {
            var objects = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour));
            List<Component> components = new List<Component>();
            foreach (MonoBehaviour obj in objects.Cast<MonoBehaviour>())
            {
                components.AddRange(obj.GetComponents(typeof(Component)));
            }
            return components;
        }
    }
}