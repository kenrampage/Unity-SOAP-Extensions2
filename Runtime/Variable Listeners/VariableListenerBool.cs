using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a BoolVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Bool")]
    public class VariableListenerBool : VariableListenerGeneric<bool>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<bool>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<bool>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private BoolVariable _variable;
            public override ScriptableVariable<bool> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<bool> _response;
            public override UnityEvent<bool> Response => _response;

            [Tooltip("Optional string event using bool value.")]
            [SerializeField] public UnityEvent<string> _stringResponse;

            public override void Invoke(bool value)
            {
                base.Invoke(value);
                _stringResponse?.Invoke(value.ToString());
            }
        }
        #endregion
    }
}
