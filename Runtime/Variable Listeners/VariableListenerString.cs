using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a StringVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener String")]
    public class VariableListenerString : VariableListenerGeneric<string>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<string>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<string>.VariableResponse
        {
            [SerializeField] private StringVariable _variable;
            public override ScriptableVariable<string> Variable => _variable;

            [SerializeField] private StringUnityEvent _response;
            public override UnityEvent<string> Response => _response;
        }

        [System.Serializable]
        public class StringUnityEvent : UnityEvent<string> { }
    }
}
