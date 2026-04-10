using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Bool")]
    public class VariableValueListenerBool : VariableValueListenerGeneric<bool>
    {
        [SerializeField] private BoolVariable _variable;
        protected override ScriptableVariable<bool> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<bool>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<bool>.ValueResponse
        {
            [SerializeField] private bool _value;
            public override bool Value => _value;

            [SerializeField] private BoolUnityEvent _response;
            public override UnityEvent<bool> Response => _response;
        }

        [System.Serializable]
        public class BoolUnityEvent : UnityEvent<bool> { }
    }
}
