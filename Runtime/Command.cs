using System;
using System.Reflection;
using UnityEngine;

namespace RB.Console
{
    public readonly struct Command
    {
        private readonly object _target;
        private readonly string _name;
        private readonly MethodInfo _method;
        private readonly bool _hasParams;
        private readonly ParameterInfo[] _params;

        public Command(object target, string name, MethodInfo method)
        {
            _target = target;
            _name = name;
            _method = method;
            _params = _method.GetParameters();
            _hasParams = _params.Length > 0;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public bool HasParams
        {
            get
            {
                return _hasParams;
            }
        }

        public ParameterInfo[] Params
        {
            get
            {
                return _params;
            }
        }

        public bool Execute(params object[] parameters)
        {
            if(parameters.Length < _params.Length)
            {
                Debug.LogError($"Not enough parameters. Excepted: {_params.Length}, actual: {parameters.Length}.");
                return false;
            }
            else if (parameters.Length > _params.Length)
            {
                TrimExcessParameters(ref parameters, _params.Length);
                Debug.LogWarning("Excess parameters were passed to the command.");
            }

            if (!TryConvertParams(parameters))
            {
                return false;
            }

            _method.Invoke(_target, parameters);
            return true;
        }

        public override bool Equals(object obj)
        {
            Command command = (Command)obj;
            return _target == command._target
                && _name == command._name
                && _method == command._method
                && _hasParams == command._hasParams;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_target, _name, _method, _hasParams);
        }

        private bool TryConvertParams(object[] parameters)
        {
            for (int i = 0; i < _params.Length; i++)
            {
                if(parameters[i].GetType().IsAssignableFrom(_params[i].ParameterType))
                {
                    parameters[i] = Convert.ChangeType(parameters[i], _params[i].ParameterType);
                }
                else
                {
                    Debug.LogError($"Actual type does not match expected type. Excepted: {_params[i].ParameterType.Name}, actual: {parameters[i].GetType().Name}");
                    return false;
                }
            }

            return true;
        }

        private void TrimExcessParameters(ref object[] parameters, int maxParamsCount)
        {
            var result = new object[maxParamsCount];
            Array.Copy(parameters, 0, result, 0, maxParamsCount);
            parameters = result;
        }
    }
}