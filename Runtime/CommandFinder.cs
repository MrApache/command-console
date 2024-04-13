using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RB.Console
{
    public class CommandFinder
    {
        private const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        private readonly static HashSet<object> _objectsWithCommands;

        static CommandFinder()
        {
            _objectsWithCommands = new HashSet<object>();
        }

        public CommandFinder()
        {
            _objectsWithCommands.TrimExcess();
        }

        public IEnumerable<Command> GetCommands()
        {
            List<Command> commands = new List<Command>();
            GetObjectsWithCommands();

            foreach (object obj in _objectsWithCommands)
            {
                commands.AddRange(GetCommandsFromObject(obj));
            }

            return commands;
        }

        public IEnumerable<Command> GetCommandsFromObject(object target)
        {
            List<Command> commands = new List<Command>();
            IEnumerable<MethodInfo> methods = FindAllMethodsWithAttribute(target);
            foreach (MethodInfo method in methods)
            {
                ConsoleCommandAttribute attrb = method.GetCustomAttribute<ConsoleCommandAttribute>(true);
                commands.Add(new Command(target, attrb.CommandName, method));
            }
            return commands;
        }

        private void GetObjectsWithCommands()
        {
            FindStaticTypes();
            FindAllComponents();
            FindAllReferenceTypeFields();
        }

        private IEnumerable<MethodInfo> FindAllMethodsWithAttribute(object target)
        {
            return target.GetType()
                .GetMethods(FLAGS)
                .Where(m => m.GetCustomAttribute<ConsoleCommandAttribute>(true) != null);
        }

        private void FindStaticTypes()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.IsAbstract && t.IsSealed);

            _objectsWithCommands.AddRange(types);
        }

        private void FindAllComponents()
        {
            UnityEngine.Object[] objects = Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour));
            foreach (MonoBehaviour obj in objects.Cast<MonoBehaviour>())
            {
                _objectsWithCommands.AddRange(obj.GetComponents(typeof(Component)));
            }
        }

        private void FindAllReferenceTypeFields()
        {
            HashSet<object> fieldObjects = new HashSet<object>();

            foreach (object target in _objectsWithCommands)
            {
                IEnumerable<object> objects = target.GetType()
                    .GetFields(FLAGS)
                    .Where(f => !CheckField(f, target))
                    .Select(f => f.GetValue(target));

                fieldObjects.AddRange(objects);
            }

            _objectsWithCommands.AddRange(fieldObjects);
        }

        private bool CheckField(FieldInfo fieldInfo, object target)
        {
            return fieldInfo.FieldType.IsValueType || fieldInfo.GetValue(target) == null;
        }
    }
}