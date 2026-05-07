using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener Quaternion")]
    public class EventValueListenerQuaternion : EventValueListenerGeneric<Quaternion>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<Quaternion>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<Quaternion>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventQuaternion _scriptableEvent = null;
            public override ScriptableEvent<Quaternion> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<Quaternion>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<Quaternion>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private Quaternion _value;
            public override Quaternion Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Quaternion> _response = null;
            public override UnityEvent<Quaternion> Response => _response;
        }
        #endregion
    }
}
