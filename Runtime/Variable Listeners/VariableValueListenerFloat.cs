using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Float")]
    public class VariableValueListenerFloat : VariableValueListenerGeneric<float>
    {
        [SerializeField] private FloatVariable _variable;
        protected override ScriptableVariable<float> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<float>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<float>.ValueResponse
        {
            [SerializeField] private float _value;
            public override float Value => _value;

            [SerializeField] private FloatUnityEvent _response;
            public override UnityEvent<float> Response => _response;
        }

        [System.Serializable]
        public class FloatUnityEvent : UnityEvent<float> { }
    }
}
