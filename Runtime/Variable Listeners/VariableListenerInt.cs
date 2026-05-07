using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for an IntVariable.
    /// Each response entry exposes an int event and a convenience float event.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Int")]
    public class VariableListenerInt : VariableListenerGeneric<int>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<int>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Invocation


        protected override void InvokeResponse(VariableListenerGeneric<int>.VariableResponse response, int value)
        {
            response.Response?.Invoke(value);
            if (response is VariableResponse intResponse)
                intResponse.FloatResponse?.Invoke(value);
        }

        #endregion

        #region Nested Types

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<int>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private IntVariable _variable;
            public override ScriptableVariable<int> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<int> _response;
            public override UnityEvent<int> Response => _response;

            [Tooltip("Optional float event converted from int value.")]
            [SerializeField] private UnityEvent<float> _floatResponse;
            public UnityEvent<float> FloatResponse => _floatResponse;
        }

        #endregion
    }
}
