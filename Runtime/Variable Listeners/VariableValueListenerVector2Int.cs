using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to a ScriptableVariable and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Vector2Int")]
    public class VariableValueListenerVector2Int : VariableValueListenerGeneric<Vector2Int>
    {
        #region Inspector

        [Tooltip("Variable asset to observe for value changes.")]
        [SerializeField] private Vector2IntVariable _variable;
        protected override ScriptableVariable<Vector2Int> Variable => _variable;

        [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Vector2Int>.ValueResponse[] ValueResponses => _valueResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Vector2Int>.ValueResponse
        {
            [Tooltip("Expected value that must match for this response to invoke.")]
            [SerializeField] private Vector2Int _value;
            public override Vector2Int Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Vector2Int> _response;
            public override UnityEvent<Vector2Int> Response => _response;
        }
        #endregion
    }
}
