using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a FloatVariable.
    /// Each response entry exposes a float event and a convenience int event (value rounded to nearest integer).
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Float")]
    public class VariableListenerFloat : VariableListenerGeneric<float>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<float>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<float>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private FloatVariable _variable;
            public override ScriptableVariable<float> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<float> _response;
            public override UnityEvent<float> Response => _response;

            [Tooltip("Optional integer event using rounded value.")]
            [SerializeField] public UnityEvent<int> _intResponse;

            [Tooltip("Optional string event using float value.")]
            [SerializeField] public UnityEvent<string> _stringResponse;

            public override void Invoke(float value)
            {
                base.Invoke(value);
                _intResponse?.Invoke(Mathf.RoundToInt(value));
                _stringResponse?.Invoke(value.ToString());
            }
        }

        #endregion
    }
}
