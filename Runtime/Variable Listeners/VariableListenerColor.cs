using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a ColorVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Color")]
    public class VariableListenerColor : VariableListenerGeneric<Color>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Color>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Color>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private ColorVariable _variable;
            public override ScriptableVariable<Color> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
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
