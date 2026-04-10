using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a QuaternionVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Quaternion")]
    public class VariableListenerQuaternion : VariableListenerGeneric<Quaternion>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Quaternion>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Quaternion>.VariableResponse
        {
            [SerializeField] private QuaternionVariable _variable;
            public override ScriptableVariable<Quaternion> Variable => _variable;

            [SerializeField] private QuaternionUnityEvent _response;
            public override UnityEvent<Quaternion> Response => _response;
        }

        [System.Serializable]
        public class QuaternionUnityEvent : UnityEvent<Quaternion> { }
    }
}
