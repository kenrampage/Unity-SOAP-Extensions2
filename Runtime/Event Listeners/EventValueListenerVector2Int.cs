using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to ScriptableEvents and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Event Value Listener Vector2Int")]
    public class EventValueListenerVector2Int : EventValueListenerGeneric<Vector2Int>
    {
        #region Inspector

        [Tooltip("Event-response entries to evaluate when events are raised.")]
        [SerializeField] private EventValueResponse[] _eventResponses = null;
        protected override EventValueListenerGeneric<Vector2Int>.EventValueResponse[] EventValueResponses => _eventResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class EventValueResponse : EventValueListenerGeneric<Vector2Int>.EventValueResponse
        {
            [Tooltip("ScriptableEvent source for this response entry.")]
            [SerializeField] private ScriptableEventVector2Int _scriptableEvent = null;
            public override ScriptableEvent<Vector2Int> ScriptableEvent => _scriptableEvent;

            [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
            [SerializeField] private ValueResponse[] _valueResponses = null;
            public override EventValueListenerGeneric<Vector2Int>.ValueResponse[] ValueResponses => _valueResponses;
        }

        [System.Serializable]
        public new class ValueResponse : EventValueListenerGeneric<Vector2Int>.ValueResponse
        {
            [Tooltip("Expected event payload value for this response.")]
            [SerializeField] private Vector2Int _value;
            public override Vector2Int Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Vector2Int> _response = null;
            public override UnityEvent<Vector2Int> Response => _response;

            [Tooltip("Optional Vector2 event converted from Vector2Int value.")]
            [SerializeField] public UnityEvent<Vector2> _vector2Response = null;

            [Tooltip("Optional string event using Vector2Int value.")]
            [SerializeField] public UnityEvent<string> _stringResponse = null;

            public override void Invoke(Vector2Int value)
            {
                base.Invoke(value);
                _vector2Response?.Invoke(new Vector2(value.x, value.y));
                _stringResponse?.Invoke(value.ToString());
            }
        }
        #endregion
    }
}
