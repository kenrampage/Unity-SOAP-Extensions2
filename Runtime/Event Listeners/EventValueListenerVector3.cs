using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener Vector3")]
    public class EventValueListenerVector3 : EventValueListenerGeneric<Vector3>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<Vector3>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<Vector3>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventVector3 _scriptableEvent = null;
            public override ScriptableEvent<Vector3> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<Vector3>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<Vector3>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private Vector3 _value;
            public override Vector3 Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Vector3> _response = null;
            public override UnityEvent<Vector3> Response => _response;

            [Tooltip("Optional Vector2 event using XY components.")]
            [SerializeField] public UnityEvent<Vector2> _vector2Response = null;

            [Tooltip("Optional string event using Vector3 value.")]
            [SerializeField] public UnityEvent<string> _stringResponse = null;

            public override void Invoke(Vector3 value)
            {
                base.Invoke(value);
                _vector2Response?.Invoke(new Vector2(value.x, value.y));
                _stringResponse?.Invoke(value.ToString());
            }
        }
        #endregion
    }
}
