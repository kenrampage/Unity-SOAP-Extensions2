using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to a ScriptableVariable and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Bool")]
    public class VariableValueListenerBool : VariableValueListenerGeneric<bool>
    {
        #region Inspector

        [Tooltip("Variable asset to observe for value changes.")]
        [SerializeField] private BoolVariable _variable;
        protected override ScriptableVariable<bool> Variable => _variable;

        [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<bool>.ValueResponse[] ValueResponses => _valueResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<bool>.ValueResponse
        {
            [Tooltip("Expected value that must match for this response to invoke.")]
            [SerializeField] private bool _value;
            public override bool Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<bool> _response;
            public override UnityEvent<bool> Response => _response;
        }
        #endregion
    }
}
