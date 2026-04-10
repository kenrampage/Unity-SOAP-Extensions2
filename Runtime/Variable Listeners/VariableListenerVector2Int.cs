using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a Vector2IntVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Vector2Int")]
    public class VariableListenerVector2Int : VariableListenerGeneric<Vector2Int>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Vector2Int>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Vector2Int>.VariableResponse
        {
            [SerializeField] private Vector2IntVariable _variable;
            public override ScriptableVariable<Vector2Int> Variable => _variable;

            [SerializeField] private Vector2IntUnityEvent _response;
            public override UnityEvent<Vector2Int> Response => _response;
        }

        [System.Serializable]
        public class Vector2IntUnityEvent : UnityEvent<Vector2Int> { }
    }
}
