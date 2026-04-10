using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    public abstract class VariableValueListenerGeneric<TValue> : VariableListenerBase
    {
        protected abstract ScriptableVariable<TValue> Variable { get; }
        protected abstract ValueResponse[] ValueResponses { get; }

        private Action<TValue> _handler;

        protected override void ToggleRegistration(bool toggle)
        {
            var variable = Variable;
            if (variable == null)
                return;

            if (toggle)
            {
                if (_handler != null)
                    return;

                _handler = ProcessValueResponses;
                variable.OnValueChanged += _handler;

                if (_invokeOnSubscribe)
                    ProcessValueResponses(variable.Value);
            }
            else
            {
                if (_handler == null)
                    return;

                variable.OnValueChanged -= _handler;
                _handler = null;
            }
        }

        protected virtual void ProcessValueResponses(TValue value)
        {
            var responses = ValueResponses;
            if (responses == null)
                return;

            foreach (var response in responses)
            {
                if (response == null)
                    continue;

                if (IsMatch(response.Value, value))
                    response.Response?.Invoke(value);
            }
        }

        protected virtual bool IsMatch(TValue expected, TValue current)
        {
            return EqualityComparer<TValue>.Default.Equals(expected, current);
        }

        public override bool ContainsCallToMethod(string methodName)
        {
            var containsMethod = false;
            var responses = ValueResponses;
            if (responses == null)
                return false;

            foreach (var response in responses)
            {
                if (response?.Response == null)
                    continue;

                var count = response.Response.GetPersistentEventCount();
                for (var i = 0; i < count; i++)
                {
                    if (response.Response.GetPersistentMethodName(i) == methodName)
                    {
                        var sb = new StringBuilder();
                        sb.Append($"<color=#f75369>{methodName}()</color>");
                        sb.Append(" is called by: <color=#52d9f5>[Variable Value] </color>");
                        sb.Append(Variable != null ? Variable.name : "(null)");
                        Debug.Log(sb.ToString(), gameObject);
                        containsMethod = true;
                        break;
                    }
                }
            }

            return containsMethod;
        }

        [Serializable]
        public class ValueResponse
        {
            public virtual TValue Value { get; }
            public virtual UnityEvent<TValue> Response { get; }
        }
    }
}
