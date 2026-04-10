using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener String")]
    public class VariableValueListenerString : VariableValueListenerGeneric<string>
    {
        [SerializeField] private StringVariable _variable;
        protected override ScriptableVariable<string> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<string>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<string>.ValueResponse
        {
            [SerializeField] private string _value;
            public override string Value => _value;

            [SerializeField] private StringUnityEvent _response;
            public override UnityEvent<string> Response => _response;
        }

        [System.Serializable]
        public class StringUnityEvent : UnityEvent<string> { }
    }
}
