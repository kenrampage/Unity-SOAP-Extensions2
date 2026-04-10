using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Component")]
    public class VariableValueListenerComponent : VariableValueListenerGeneric<Component>
    {
        [SerializeField] private ComponentVariable _variable;
        protected override ScriptableVariable<Component> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Component>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Component>.ValueResponse
        {
            [SerializeField] private Component _value;
            public override Component Value => _value;

            [SerializeField] private ComponentUnityEvent _response;
            public override UnityEvent<Component> Response => _response;
        }

        [System.Serializable]
        public class ComponentUnityEvent : UnityEvent<Component> { }
    }
}
