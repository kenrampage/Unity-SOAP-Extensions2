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
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<float>.VariableResponse[] VariableResponses => _variableResponses;

        protected override void InvokeResponse(VariableListenerGeneric<float>.VariableResponse response, float value)
        {
            response.Response?.Invoke(value);
            if (response is VariableResponse floatResponse)
                floatResponse.IntResponse?.Invoke(Mathf.RoundToInt(value));
        }

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<float>.VariableResponse
        {
            [SerializeField] private FloatVariable _variable;
            public override ScriptableVariable<float> Variable => _variable;

            [SerializeField] private FloatUnityEvent _response;
            public override UnityEvent<float> Response => _response;

            [SerializeField] private IntUnityEvent _intResponse;
            public UnityEvent<int> IntResponse => _intResponse;
        }

        [System.Serializable]
        public class FloatUnityEvent : UnityEvent<float> { }

        [System.Serializable]
        public class IntUnityEvent : UnityEvent<int> { }
    }
}
