using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Generic base for value-matching variable listener components. Observes a single
    /// <see cref="ScriptableVariable{TValue}"/> and, on each change, evaluates every
    /// configured <see cref="ValueResponse"/> entry — invoking ALL whose expected value
    /// matches the incoming value, in array order.
    /// <para>
    /// <b>Difference from <c>VariableListenerGeneric</c>:</b> this class listens to one
    /// variable and dispatches to value-specific responses (switch-case style), whereas
    /// <c>VariableListenerGeneric</c> supports multiple variable sources each with their
    /// own unconditional response.
    /// </para>
    /// <para>
    /// <b>Subclass pattern:</b><br/>
    /// 1. Declare a <c>[SerializeField]</c> field for the concrete SOAP variable type and
    ///    override <see cref="Variable"/> to return it.<br/>
    /// 2. Declare a <c>[SerializeField] ValueResponse[] _valueResponses</c> field typed to
    ///    the subclass's own <c>ValueResponse</c> and override <see cref="ValueResponses"/>.<br/>
    /// 3. Nest a <c>new class ValueResponse</c> overriding <see cref="ValueResponse.Value"/>
    ///    and <see cref="ValueResponse.Response"/> with serialized backing fields.
    /// </para>
    /// <para>
    /// <b>Override <see cref="IsMatch"/> to customise matching</b> (e.g. range checks
    /// or approximate float equality) without changing invocation or lifecycle logic.
    /// </para>
    /// </summary>
    public abstract class VariableValueListenerGeneric<TValue> : VariableListenerBase
    {
        #region Inspector

        /// <summary>
        /// The SOAP variable asset to observe. Override with a serialized concrete
        /// typed field (e.g. <c>BoolVariable</c>) in each subclass.
        /// </summary>
        protected abstract ScriptableVariable<TValue> Variable { get; }

        /// <summary>
        /// The value-response pairs to evaluate on each variable change. Override
        /// with a serialized field typed to the subclass's own <c>ValueResponse</c>
        /// so Unity serializes the correct concrete type.
        /// </summary>
        protected abstract ValueResponse[] ValueResponses { get; }

        #endregion

        #region Runtime

        private Action<TValue> _handler;

        #endregion

        #region Registration

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

        #endregion

        #region Matching

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

        /// <summary>
        /// Determines whether the expected value of a response entry matches the
        /// incoming variable value. Uses default equality by default.
        /// Override to implement custom matching such as range checks or
        /// approximate float comparisons.
        /// </summary>
        protected virtual bool IsMatch(TValue expected, TValue current)
        {
            return EqualityComparer<TValue>.Default.Equals(expected, current);
        }

        #endregion

        #region Introspection

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

        #endregion

        #region Nested Types

        /// <summary>
        /// Base pairing of an expected value and its response event.
        /// The virtual properties return default by default — concrete subclasses must
        /// override both with serialized backing fields to provide actual data.
        /// <para>
        /// Unity only serializes fields, not properties, so each subclass must declare
        /// its own <c>[SerializeField]</c> fields and expose them via the overrides.
        /// The properties exist solely as a polymorphic access path for the base class logic.
        /// </para>
        /// </summary>
        [Serializable]
        public class ValueResponse
        {
            /// <summary>
            /// The value that must match the incoming variable value for this response to fire.
            /// Override with a <c>[SerializeField]</c> field of the concrete type in each subclass.
            /// </summary>
            public virtual TValue Value { get; }

            /// <summary>
            /// Event invoked when <see cref="Value"/> matches the incoming variable value.
            /// Override with a <c>[SerializeField] UnityEvent&lt;TValue&gt;</c> field in each subclass.
            /// </summary>
            public virtual UnityEvent<TValue> Response { get; }
        }

        #endregion
    }
}
