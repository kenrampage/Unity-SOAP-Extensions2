using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a Vector2IntVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Vector2Int")]
    public class VariableListenerVector2Int : VariableListenerGeneric<Vector2Int>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Vector2Int>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Vector2Int>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private Vector2IntVariable _variable;
            public override ScriptableVariable<Vector2Int> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<Vector2Int> _response;
            public override UnityEvent<Vector2Int> Response => _response;
        }
        #endregion
    }
}
