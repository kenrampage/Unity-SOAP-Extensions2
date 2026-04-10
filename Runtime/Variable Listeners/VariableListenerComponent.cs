using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a ComponentVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Component")]
    public class VariableListenerComponent : VariableListenerGeneric<Component>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Component>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Component>.VariableResponse
        {
            [SerializeField] private ComponentVariable _variable;
            public override ScriptableVariable<Component> Variable => _variable;

            [SerializeField] private ComponentUnityEvent _response;
            public override UnityEvent<Component> Response => _response;
        }

        [System.Serializable]
        public class ComponentUnityEvent : UnityEvent<Component> { }
    }
}
