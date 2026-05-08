using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// A listener for a QuaternionVariable.
    /// </summary>
    [AddComponentMenu("Ken Rampage/Addons/SOAP/Listeners/Variable Listener Quaternion")]
    public class VariableListenerQuaternion : VariableListenerGeneric<Quaternion>
    {
        #region Inspector

        [Tooltip("Variable-response entries to subscribe and invoke.")]
        [SerializeField] private VariableResponse[] _variableResponses;
        protected override VariableListenerGeneric<Quaternion>.VariableResponse[] VariableResponses => _variableResponses;

        #endregion

        #region Nested Types


        [System.Serializable]
        public new class VariableResponse : VariableListenerGeneric<Quaternion>.VariableResponse
        {
            [Tooltip("Variable asset source for this response entry.")]
            [SerializeField] private QuaternionVariable _variable;
            public override ScriptableVariable<Quaternion> Variable => _variable;

            [Tooltip("UnityEvent invoked when this variable changes.")]
            [SerializeField] private UnityEvent<Quaternion> _response;
            public override UnityEvent<Quaternion> Response => _response;

            [Tooltip("Optional Vector3 event using Euler angles.")]
            [SerializeField] public UnityEvent<Vector3> _vector3Response;

            [Tooltip("Optional string event using Quaternion value.")]
            [SerializeField] public UnityEvent<string> _stringResponse;

            public override void Invoke(Quaternion value)
            {
                base.Invoke(value);
                _vector3Response?.Invoke(value.eulerAngles);
                _stringResponse?.Invoke(value.ToString());
            }
        }
        #endregion
    }
}
