using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to a ScriptableVariable and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener String")]
    public class VariableValueListenerString : VariableValueListenerGeneric<string>
    {
        #region Inspector

        [Tooltip("Variable asset to observe for value changes.")]
        [SerializeField] private StringVariable _variable;
        protected override ScriptableVariable<string> Variable => _variable;

        [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<string>.ValueResponse[] ValueResponses => _valueResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<string>.ValueResponse
        {
            [Tooltip("Expected value that must match for this response to invoke.")]
            [SerializeField] private string _value;
            public override string Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<string> _response;
            public override UnityEvent<string> Response => _response;
        }
        #endregion
    }
}
