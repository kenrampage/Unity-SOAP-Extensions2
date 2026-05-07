using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Generic base for value-matching event listener components. Extends the SOAP
    /// <c>EventListenerGeneric&lt;TValue&gt;</c> to add value filtering: when a
    /// <see cref="ScriptableEvent{TValue}"/> fires, every <see cref="ValueResponse"/> whose
    /// expected value matches the event payload is invoked, in array order.
    /// <para>
    /// <b>Difference from plain event listeners:</b> instead of one unconditional response
    /// per event, each event entry holds a list of value-response pairs evaluated like a
    /// switch-case — multiple branches can fire for the same event raise.
    /// </para>
    /// <para>
    /// <b>Subclass pattern:</b><br/>
    /// 1. Declare a <c>[SerializeField] EventValueResponse[] _eventResponses</c> field typed
    ///    to the subclass's own <c>EventValueResponse</c> and override <see cref="EventValueResponses"/>.<br/>
    /// 2. Nest a <c>new class EventValueResponse</c> overriding <see cref="EventValueResponse.ScriptableEvent"/>
    ///    and <see cref="EventValueResponse.ValueResponses"/> with serialized backing fields.<br/>
    /// 3. Nest a <c>new class ValueResponse</c> overriding <see cref="ValueResponse.Value"/>
    ///    and <see cref="ValueResponse.Response"/> with serialized backing fields.
    /// </para>
    /// <para>
    /// <b>Override <see cref="IsMatch"/> to customise matching</b> without changing
    /// invocation or subscription logic.
    /// </para>
    /// </summary>
    public abstract class EventValueListenerGeneric<TValue> : EventListenerGeneric<TValue>
    {
        #region Inspector

        /// <summary>
        /// The event-response pairs configured in the Inspector. Override with a serialized
        /// field typed to the subclass's own <c>EventValueResponse</c> so Unity serializes
        /// the correct concrete type.
        /// </summary>
        protected abstract EventValueResponse[] EventValueResponses { get; }

        /// <summary>
        /// Satisfies the base class contract by returning <see cref="EventValueResponses"/>.
        /// Allows the base SOAP subscription logic to drive all event wiring without
        /// needing to know about value filtering.
        /// </summary>
        protected override EventResponse<TValue>[] EventResponses => EventValueResponses;

        #endregion

        #region Matching

        /// <summary>
        /// Determines whether a value response's expected value matches the event payload.
        /// Uses default equality by default. Override to implement custom matching such
        /// as range checks or approximate float comparisons.
        /// </summary>
        protected virtual bool IsMatch(TValue expected, TValue current)
        {
            return EqualityComparer<TValue>.Default.Equals(expected, current);
        }

        #endregion

        #region Invocation

        protected override void InvokeResponse(ScriptableEvent<TValue> eventRaised, EventResponse<TValue> eventResponse, TValue param, bool debug)
        {
            if (eventResponse is not EventValueResponse valueEventResponse)
                return;

            var didInvoke = false;
            var valueResponses = valueEventResponse.ValueResponses;
            if (valueResponses == null)
                return;

            foreach (var valueResponse in valueResponses)
            {
                if (valueResponse == null)
                    continue;

                if (!IsMatch(valueResponse.Value, param))
                    continue;

                valueResponse.Response?.Invoke(param);
                didInvoke = true;
            }

            if (didInvoke && debug)
                Debug(eventRaised);
        }

        #endregion

        #region Introspection

        public override bool ContainsCallToMethod(string methodName)
        {
            var containsMethod = false;
            foreach (var eventResponse in EventValueResponses)
            {
                var valueResponses = eventResponse.ValueResponses;
                if (valueResponses == null)
                    continue;

                foreach (var valueResponse in valueResponses)
                {
                    if (valueResponse?.Response == null)
                        continue;

                    var registeredListenerCount = valueResponse.Response.GetPersistentEventCount();
                    for (var i = 0; i < registeredListenerCount; i++)
                    {
                        if (valueResponse.Response.GetPersistentMethodName(i) == methodName)
                        {
                            var sb = new StringBuilder();
                            sb.Append($"<color=#f75369>{methodName}()</color>");
                            sb.Append(" is called by: <color=#f75369>[Event Value] </color>");
                            sb.Append(eventResponse.ScriptableEvent.name);
                            UnityEngine.Debug.Log(sb.ToString(), gameObject);
                            containsMethod = true;
                            break;
                        }
                    }
                }
            }

            return containsMethod;
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Base pairing of a ScriptableEvent source and its nested value-response list.
        /// The virtual properties return null by default — concrete subclasses must
        /// override both with serialized backing fields to provide actual data.
        /// <para>
        /// Unity only serializes fields, not properties, so each subclass must declare
        /// its own <c>[SerializeField]</c> fields and expose them via the overrides.
        /// The properties exist solely as a polymorphic access path for the base class logic.
        /// </para>
        /// </summary>
        [Serializable]
        public class EventValueResponse : EventResponse<TValue>
        {
            /// <summary>
            /// The SOAP event asset to subscribe to. Override with a serialized concrete
            /// typed field (e.g. <c>ScriptableEventBool</c>) in each subclass.
            /// </summary>
            public override ScriptableEvent<TValue> ScriptableEvent { get; }

            /// <summary>
            /// Value-response pairs evaluated when this event fires. Override with a
            /// <c>[SerializeField] ValueResponse[]</c> field typed to the subclass's own
            /// <c>ValueResponse</c> so Unity serializes the correct concrete type.
            /// </summary>
            public virtual ValueResponse[] ValueResponses { get; }
        }

        /// <summary>
        /// Base pairing of an expected payload value and its response event.
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
            /// The value that must match the event payload for this response to fire.
            /// Override with a <c>[SerializeField]</c> field of the concrete type in each subclass.
            /// </summary>
            public virtual TValue Value { get; }

            /// <summary>
            /// Event invoked when <see cref="Value"/> matches the event payload.
            /// Override with a <c>[SerializeField] UnityEvent&lt;TValue&gt;</c> field in each subclass.
            /// </summary>
            public virtual UnityEvent<TValue> Response { get; }
        }

        #endregion
    }
}
