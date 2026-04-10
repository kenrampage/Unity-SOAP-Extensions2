using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a ColorVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Color")]
    public class VariableListenerColor : VariableListenerGeneric<Color>
    {
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Color>.VariableResponse[] VariableResponses => _variableResponses;

        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Color>.VariableResponse
        {
            [SerializeField] private ColorVariable _variable;
            public override ScriptableVariable<Color> Variable => _variable;

            [SerializeField] private ColorUnityEvent _response;
            public override UnityEvent<Color> Response => _response;
        }

        [System.Serializable]
        public class ColorUnityEvent : UnityEvent<Color> { }
    }
}
