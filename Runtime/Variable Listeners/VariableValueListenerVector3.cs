using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Vector3")]
    public class VariableValueListenerVector3 : VariableValueListenerGeneric<Vector3>
    {
        [SerializeField] private Vector3Variable _variable;
        protected override ScriptableVariable<Vector3> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Vector3>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Vector3>.ValueResponse
        {
            [SerializeField] private Vector3 _value;
            public override Vector3 Value => _value;

            [SerializeField] private Vector3UnityEvent _response;
            public override UnityEvent<Vector3> Response => _response;
        }

        [System.Serializable]
        public class Vector3UnityEvent : UnityEvent<Vector3> { }
    }
}
