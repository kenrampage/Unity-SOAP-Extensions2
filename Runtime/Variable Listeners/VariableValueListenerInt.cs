using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Int")]
    public class VariableValueListenerInt : VariableValueListenerGeneric<int>
    {
        [SerializeField] private IntVariable _variable;
        protected override ScriptableVariable<int> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<int>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<int>.ValueResponse
        {
            [SerializeField] private int _value;
            public override int Value => _value;

            [SerializeField] private IntUnityEvent _response;
            public override UnityEvent<int> Response => _response;
        }

        [System.Serializable]
        public class IntUnityEvent : UnityEvent<int> { }
    }
}
