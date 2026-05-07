using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener String")]
    public class EventValueListenerString : EventValueListenerGeneric<string>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<string>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<string>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventString _scriptableEvent = null;
            public override ScriptableEvent<string> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<string>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<string>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private string _value;
            public override string Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<string> _response = null;
            public override UnityEvent<string> Response => _response;
        }
        #endregion
    }
}
