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

        #region Invocation


        protected override void InvokeResponse(VariableListenerGeneric<float>.VariableResponse response, float value)
        {
            response.Response?.Invoke(value);
            if (response is VariableResponse floatResponse)
                floatResponse.IntResponse?.Invoke(Mathf.RoundToInt(value));
        }

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
            [SerializeField] private UnityEvent<int> _intResponse;
            public UnityEvent<int> IntResponse => _intResponse;
        }

        #endregion
    }
}
