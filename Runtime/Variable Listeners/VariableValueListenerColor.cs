using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to a ScriptableVariable and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Color")]
    public class VariableValueListenerColor : VariableValueListenerGeneric<Color>
    {
        #region Inspector

        [Tooltip("Variable asset to observe for value changes.")]
        [SerializeField] private ColorVariable _variable;
        protected override ScriptableVariable<Color> Variable => _variable;

        [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Color>.ValueResponse[] ValueResponses => _valueResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Color>.ValueResponse
        {
            [Tooltip("Expected value that must match for this response to invoke.")]
            [SerializeField] private Color _value;
            public override Color Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<Color> _response;
            public override UnityEvent<Color> Response => _response;

            [Tooltip("Optional string event using Color.ToString().")]
            [SerializeField] public UnityEvent<string> _stringResponse;

            public override void Invoke(Color value)
            {
                base.Invoke(value);
                _stringResponse?.Invoke(value.ToString());
            }
        }
        #endregion
    }
}
