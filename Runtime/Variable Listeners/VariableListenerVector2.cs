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

            [Tooltip("Optional Vector2Int event using rounded components.")]
            [SerializeField] public UnityEvent<Vector2Int> _vector2IntResponse;

            [Tooltip("Optional string event using Vector2 value.")]
            [SerializeField] public UnityEvent<string> _stringResponse;

            public override void Invoke(Vector2 value)
            {
                base.Invoke(value);
                _vector2IntResponse?.Invoke(new Vector2Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y)));
                _stringResponse?.Invoke(value.ToString());
            }
        }
        #endregion
    }
}
