using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener GameObject")]
    public class EventValueListenerGameObject : EventValueListenerGeneric<GameObject>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<GameObject>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<GameObject>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventGameObject _scriptableEvent = null;
            public override ScriptableEvent<GameObject> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<GameObject>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<GameObject>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private GameObject _value;
            public override GameObject Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<GameObject> _response = null;
            public override UnityEvent<GameObject> Response => _response;
        }
        #endregion
    }
}
