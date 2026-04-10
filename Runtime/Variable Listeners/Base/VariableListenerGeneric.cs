using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    public abstract class VariableListenerGeneric<TValue> : VariableListenerBase
    {
        protected abstract VariableResponse[] VariableResponses { get; }

        private readonly Dictionary<ScriptableVariable<TValue>, Action<TValue>> _handlers =
            new Dictionary<ScriptableVariable<TValue>, Action<TValue>>();

        protected override void ToggleRegistration(bool toggle)
        {
            foreach (var response in VariableResponses)
            {
                if (response.Variable == null) continue;

                if (toggle)
                {
                    if (_handlers.ContainsKey(response.Variable)) continue;
                    var captured = response;
                    Action<TValue> handler = value => InvokeResponse(captured, value);
                    _handlers[response.Variable] = handler;
                    response.Variable.OnValueChanged += handler;
                }
                else
                {
                    if (_handlers.TryGetValue(response.Variable, out var handler))
                    {
                        response.Variable.OnValueChanged -= handler;
                        _handlers.Remove(response.Variable);
                    }
                }
            }

            if (toggle && _invokeOnSubscribe)
                InvokeCurrentValues();
        }

        protected virtual void InvokeResponse(VariableResponse response, TValue value)
        {
            response.Response?.Invoke(value);
        }

        protected void InvokeCurrentValues()
        {
            foreach (var response in VariableResponses)
            {
                if (response.Variable != null)
                    InvokeResponse(response, response.Variable.Value);
            }
        }

        public override bool ContainsCallToMethod(string methodName)
        {
            var containsMethod = false;
            foreach (var response in VariableResponses)
            {
                if (response.Response == null) continue;
                var count = response.Response.GetPersistentEventCount();
                for (var i = 0; i < count; i++)
                {
                    if (response.Response.GetPersistentMethodName(i) == methodName)
                    {
                        var sb = new StringBuilder();
                        sb.Append($"<color=#f75369>{methodName}()</color>");
                        sb.Append(" is called by: <color=#52d9f5>[Variable] </color>");
                        sb.Append(response.Variable != null ? response.Variable.name : "(null)");
                        Debug.Log(sb.ToString(), gameObject);
                        containsMethod = true;
                        break;
                    }
                }
            }

            return containsMethod;
        }

        [Serializable]
        public class VariableResponse
        {
            public virtual ScriptableVariable<TValue> Variable { get; }
            public virtual UnityEvent<TValue> Response { get; }
        }
    }
}
