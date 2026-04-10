using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Vector2Int")]
    public class VariableValueListenerVector2Int : VariableValueListenerGeneric<Vector2Int>
    {
        [SerializeField] private Vector2IntVariable _variable;
        protected override ScriptableVariable<Vector2Int> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Vector2Int>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Vector2Int>.ValueResponse
        {
            [SerializeField] private Vector2Int _value;
            public override Vector2Int Value => _value;

            [SerializeField] private Vector2IntUnityEvent _response;
            public override UnityEvent<Vector2Int> Response => _response;
        }

        [System.Serializable]
        public class Vector2IntUnityEvent : UnityEvent<Vector2Int> { }
    }
}
