using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Value Listener Color")]
    public class VariableValueListenerColor : VariableValueListenerGeneric<Color>
    {
        [SerializeField] private ColorVariable _variable;
        protected override ScriptableVariable<Color> Variable => _variable;

        [SerializeField] private ValueResponse[] _valueResponses;
        protected override VariableValueListenerGeneric<Color>.ValueResponse[] ValueResponses => _valueResponses;

        [System.Serializable]
        public new class ValueResponse : VariableValueListenerGeneric<Color>.ValueResponse
        {
            [SerializeField] private Color _value;
            public override Color Value => _value;

            [SerializeField] private ColorUnityEvent _response;
            public override UnityEvent<Color> Response => _response;
        }

        [System.Serializable]
        public class ColorUnityEvent : UnityEvent<Color> { }
    }
}
