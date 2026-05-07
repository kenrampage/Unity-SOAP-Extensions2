using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a Vector3Variable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Vector3")]
    public class VariableListenerVector3 : VariableListenerGeneric<Vector3>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Vector3>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Vector3>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private Vector3Variable _variable;
            public override ScriptableVariable<Vector3> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<Vector3> _response;
            public override UnityEvent<Vector3> Response => _response;
        }
        #endregion
    }
}
