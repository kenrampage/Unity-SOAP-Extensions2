using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener GameObject")]
    public class VariableValueListenerGameObject : VariableValueListenerGeneric<GameObject>
    {
        [SerializeField] private GameObjectVariable _variable;
        protected override ScriptableVariable<GameObject> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<GameObject>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<GameObject>.ValueResponse
        {
            [SerializeField] private GameObject _value;
            public override GameObject Value => _value;

            [SerializeField] private GameObjectUnityEvent _response;
            public override UnityEvent<GameObject> Response => _response;
        }

        [System.Serializable]
        public class GameObjectUnityEvent : UnityEvent<GameObject> { }
    }
}
