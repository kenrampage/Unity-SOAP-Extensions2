using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Listens to a ScriptableVariable and invokes configured UnityEvents for every matching value entry.
    /// If multiple entries match, all matching entries are invoked in array order.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener GameObject")]
    public class VariableValueListenerGameObject : VariableValueListenerGeneric<GameObject>
    {
        #region Inspector

        [Tooltip("Variable asset to observe for value changes.")]
        [SerializeField] private GameObjectVariable _variable;
        protected override ScriptableVariable<GameObject> Variable => _variable;

        [Tooltip("Value-response entries to evaluate. All matching entries will be invoked.")]
        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<GameObject>.ValueResponse[] ValueResponses => _valueResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<GameObject>.ValueResponse
        {
            [Tooltip("Expected value that must match for this response to invoke.")]
            [SerializeField] private GameObject _value;
            public override GameObject Value => _value;

            [Tooltip("UnityEvent invoked when the expected value matches.")]
            [SerializeField] private UnityEvent<GameObject> _response;
            public override UnityEvent<GameObject> Response => _response;

            [Tooltip("Optional string event using GameObject.ToString().")]
            [SerializeField] public UnityEvent<string> _stringResponse;

            public override void Invoke(GameObject value)
            {
                base.Invoke(value);
                _stringResponse?.Invoke(value != null ? value.ToString() : string.Empty);
            }
        }
        #endregion
    }
}
