using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Quaternion")]
    public class VariableValueListenerQuaternion : VariableValueListenerGeneric<Quaternion>
    {
        [SerializeField] private QuaternionVariable _variable;
        protected override ScriptableVariable<Quaternion> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Quaternion>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Quaternion>.ValueResponse
        {
            [SerializeField] private Quaternion _value;
            public override Quaternion Value => _value;

            [SerializeField] private QuaternionUnityEvent _response;
            public override UnityEvent<Quaternion> Response => _response;
        }

        [System.Serializable]
        public class QuaternionUnityEvent : UnityEvent<Quaternion> { }
    }
}
