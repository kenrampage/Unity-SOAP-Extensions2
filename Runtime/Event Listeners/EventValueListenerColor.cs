using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener Color")]
    public class EventValueListenerColor : EventValueListenerGeneric<Color>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<Color>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<Color>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventColor _scriptableEvent = null;
            public override ScriptableEvent<Color> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<Color>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<Color>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private Color _value;
            public override Color Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Color> _response = null;
            public override UnityEvent<Color> Response => _response;
        }
        #endregion
    }
}
