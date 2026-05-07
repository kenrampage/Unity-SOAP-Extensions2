using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener Int")]
    public class EventValueListenerInt : EventValueListenerGeneric<int>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<int>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<int>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventInt _scriptableEvent = null;
            public override ScriptableEvent<int> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<int>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<int>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private int _value;
            public override int Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<int> _response = null;
            public override UnityEvent<int> Response => _response;
        }
        #endregion
    }
}
