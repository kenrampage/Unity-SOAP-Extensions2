using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a GameObjectVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener GameObject")]
    public class VariableListenerGameObject : VariableListenerGeneric<GameObject>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<GameObject>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<GameObject>.VariableResponse
        {
            [SerializeField] private GameObjectVariable _variable;
            public override ScriptableVariable<GameObject> Variable => _variable;

            [SerializeField] private GameObjectUnityEvent _response;
            public override UnityEvent<GameObject> Response => _response;
        }

        [System.Serializable]
        public class GameObjectUnityEvent : UnityEvent<GameObject> { }
    }
}
