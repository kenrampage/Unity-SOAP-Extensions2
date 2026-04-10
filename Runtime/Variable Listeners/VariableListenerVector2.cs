using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a Vector2Variable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Vector2")]
    public class VariableListenerVector2 : VariableListenerGeneric<Vector2>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Vector2>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Vector2>.VariableResponse
        {
            [SerializeField] private Vector2Variable _variable;
            public override ScriptableVariable<Vector2> Variable => _variable;

            [SerializeField] private Vector2UnityEvent _response;
            public override UnityEvent<Vector2> Response => _response;
        }

        [System.Serializable]
        public class Vector2UnityEvent : UnityEvent<Vector2> { }
    }
}
