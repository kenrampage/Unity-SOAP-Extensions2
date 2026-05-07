using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a GameObjectVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener GameObject")]
    public class VariableListenerGameObject : VariableListenerGeneric<GameObject>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<GameObject>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<GameObject>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private GameObjectVariable _variable;
            public override ScriptableVariable<GameObject> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<GameObject> _response;
            public override UnityEvent<GameObject> Response => _response;
        }
        #endregion
    }
}
