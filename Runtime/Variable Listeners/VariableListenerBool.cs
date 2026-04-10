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
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<bool>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<bool>.VariableResponse
        {
            [SerializeField] private BoolVariable _variable;
            public override ScriptableVariable<bool> Variable => _variable;

            [SerializeField] private BoolUnityEvent _response;
            public override UnityEvent<bool> Response => _response;
        }

        [System.Serializable]
        public class BoolUnityEvent : UnityEvent<bool> { }
    }
}
