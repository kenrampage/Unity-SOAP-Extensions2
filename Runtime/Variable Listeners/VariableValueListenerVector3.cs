using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to a ScriptableVariable and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Vector3")]
    public class VariableValueListenerVector3 : VariableValueListenerGeneric<Vector3>
    {
        #region Inspector

        [Tooltip("Variable asset to observe for value changes.")]
        [SerializeField] private Vector3Variable _variable;
        protected override ScriptableVariable<Vector3> Variable => _variable;

        [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Vector3>.ValueResponse[] ValueResponses => _valueResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Vector3>.ValueResponse
        {
            [Tooltip("Expected value that must match for this response to invoke.")]
            [SerializeField] private Vector3 _value;
            public override Vector3 Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Vector3> _response;
            public override UnityEvent<Vector3> Response => _response;

            [Tooltip("Optional Vector2 event using XY components.")]
            [SerializeField] public UnityEvent<Vector2> _vector2Response;

            [Tooltip("Optional string event using Vector3 value.")]
            [SerializeField] public UnityEvent<string> _stringResponse;

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
