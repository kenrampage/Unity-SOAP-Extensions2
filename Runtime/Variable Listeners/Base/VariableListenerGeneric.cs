using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Generic base for variable listener components. Subscribes to one or more
    /// <see cref="ScriptableVariable{TValue}"/> assets and invokes a paired
    /// <see cref="UnityEvent{TValue}"/> whenever any of them change.
    /// <para>
    /// <b>Why abstract:</b> Unity cannot add open-generic types as components, and the
    /// base <see cref="VariableResponse"/> has no concrete serialized fields. Concrete
    /// subclasses (e.g. <c>VariableListenerBool</c>) provide the typed variable and event.
    /// </para>
    /// <para>
    /// <b>Subclass pattern:</b><br/>
    /// 1. Declare a <c>[SerializeField] private VariableResponse[] _variableResponses</c> field
    ///    typed to the subclass's own <c>VariableResponse</c> so Unity serializes the concrete type.<br/>
    /// 2. Override <see cref="VariableResponses"/> to return that field — this satisfies the
    ///    abstract contract so base-class logic can drive subscription and invocation.<br/>
    /// 3. Nest a <c>new class VariableResponse</c> that overrides <see cref="VariableResponse.Variable"/>
    ///    and <see cref="VariableResponse.Response"/> with serialized backing fields for the
    ///    concrete SOAP variable type and a <c>UnityEvent&lt;TValue&gt;</c>.
    /// </para>
    /// <para>
    /// <b>Override <see cref="InvokeResponse"/> to add extra events</b> (e.g. a convenience
    /// float output on an int listener) without changing the base subscription logic.
    /// </para>
    /// </summary>
    public abstract class VariableListenerGeneric<TValue> : VariableListenerBase
    {
        #region Inspector

        /// <summary>
        /// Returns the array of variable-response pairs configured in the Inspector.
        /// Must be implemented by each concrete subclass, returning its own serialized
        /// <c>VariableResponse[]</c> field so Unity serializes the correct concrete type.
        /// </summary>
        protected abstract VariableResponse[] VariableResponses { get; }

        #endregion

        #region Runtime

        private readonly Dictionary<ScriptableVariable<TValue>, Action<TValue>> _handlers =
            new Dictionary<ScriptableVariable<TValue>, Action<TValue>>();

        #endregion

        #region Registration

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

        #endregion

        #region Invocation

        /// <summary>
        /// Invokes the response for a single variable-response pair.
        /// Override in subclasses to fire additional events (e.g. a float output
        /// alongside the primary int event) without duplicating subscription logic.
        /// </summary>
        protected virtual void InvokeResponse(VariableResponse response, TValue value)
        {
            response.Response?.Invoke(value);
        }

        /// <summary>
        /// Immediately invokes all configured responses using each variable's current value.
        /// Called automatically when <c>InvokeOnSubscribe</c> is enabled.
        /// </summary>
        protected void InvokeCurrentValues()
        {
            foreach (var response in VariableResponses)
            {
                if (response.Variable != null)
                    InvokeResponse(response, response.Variable.Value);
            }
        }

        #endregion

        #region Introspection

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

        #endregion

        #region Nested Types

        /// <summary>
        /// Base pairing of a variable source and its response event.
        /// The virtual properties return null by default — concrete subclasses must
        /// override both with serialized backing fields to provide actual data.
        /// <para>
        /// Unity only serializes fields, not properties, so each subclass must declare
        /// its own <c>[SerializeField]</c> fields and expose them via the overrides.
        /// The properties exist solely as a polymorphic access path for the base class logic.
        /// </para>
        /// </summary>
        [Serializable]
        public class VariableResponse
        {
            /// <summary>
            /// The SOAP variable asset to observe. Override with a concrete typed variable
            /// field (e.g. <c>BoolVariable</c>) in each subclass.
            /// </summary>
            public virtual ScriptableVariable<TValue> Variable { get; }

            /// <summary>
            /// Event invoked when the variable's value changes.
            /// Override with a <c>[SerializeField] UnityEvent&lt;TValue&gt;</c> field in each subclass.
            /// </summary>
            public virtual UnityEvent<TValue> Response { get; }
        }

        #endregion
    }
}
