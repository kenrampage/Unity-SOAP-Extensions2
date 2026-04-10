using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Vector2")]
    public class VariableValueListenerVector2 : VariableValueListenerGeneric<Vector2>
    {
        [SerializeField] private Vector2Variable _variable;
        protected override ScriptableVariable<Vector2> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Vector2>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Vector2>.ValueResponse
        {
            [SerializeField] private Vector2 _value;
            public override Vector2 Value => _value;

            [SerializeField] private Vector2UnityEvent _response;
            public override UnityEvent<Vector2> Response => _response;
        }

        [System.Serializable]
        public class Vector2UnityEvent : UnityEvent<Vector2> { }
    }
}
