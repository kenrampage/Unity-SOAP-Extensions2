using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a Vector3Variable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Vector3")]
    public class VariableListenerVector3 : VariableListenerGeneric<Vector3>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Vector3>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Vector3>.VariableResponse
        {
            [SerializeField] private Vector3Variable _variable;
            public override ScriptableVariable<Vector3> Variable => _variable;

            [SerializeField] private Vector3UnityEvent _response;
            public override UnityEvent<Vector3> Response => _response;
        }

        [System.Serializable]
        public class Vector3UnityEvent : UnityEvent<Vector3> { }
    }
}
