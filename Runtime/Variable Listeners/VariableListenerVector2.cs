using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a Vector2Variable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Vector2")]
    public class VariableListenerVector2 : VariableListenerGeneric<Vector2>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Vector2>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Vector2>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private Vector2Variable _variable;
            public override ScriptableVariable<Vector2> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<Vector2> _response;
            public override UnityEvent<Vector2> Response => _response;
        }
        #endregion
    }
}
