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
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<int>.VariableResponse[] VariableResponses => _variableResponses;

        protected override void InvokeResponse(VariableListenerGeneric<int>.VariableResponse response, int value)
        {
            response.Response?.Invoke(value);
            if (response is VariableResponse intResponse)
                intResponse.FloatResponse?.Invoke(value);
        }

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<int>.VariableResponse
        {
            [SerializeField] private IntVariable _variable;
            public override ScriptableVariable<int> Variable => _variable;

            [SerializeField] private IntUnityEvent _response;
            public override UnityEvent<int> Response => _response;

            [SerializeField] private FloatUnityEvent _floatResponse;
            public UnityEvent<float> FloatResponse => _floatResponse;
        }

        [System.Serializable]
        public class IntUnityEvent : UnityEvent<int> { }

        [System.Serializable]
        public class FloatUnityEvent : UnityEvent<float> { }
    }
}
