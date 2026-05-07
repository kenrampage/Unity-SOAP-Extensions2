using System.Threading;
using UnityEngine;

namespace KenRampage.Addons.SOAP.Listeners
{
    /// <summary>
    /// Base lifecycle class for all listener components in this extensions package.
    /// Manages subscription lifetime and drives the subscribe/unsubscribe cycle
    /// via <see cref="ToggleRegistration"/>.
    /// <para>
    /// <b>Binding modes:</b><br/>
    /// - <c>UNTIL_DESTROY</c>: subscribes on <c>Awake</c>, unsubscribes on <c>OnDestroy</c>.<br/>
    /// - <c>UNTIL_DISABLE</c>: subscribes on <c>OnEnable</c>, unsubscribes on <c>OnDisable</c>.
    /// </para>
    /// <para>
    /// <b>Invoke on subscribe:</b> when enabled, the listener immediately fires its
    /// configured responses using the current variable/event value at subscription time,
    /// so downstream objects always start in the correct state.
    /// </para>
    /// <para>
    /// <b>Disable after subscribing:</b> when enabled, the GameObject is deactivated
    /// immediately after subscribing. Useful for listeners that only need to run once
    /// at startup and should not remain active.
    /// </para>
    /// </summary>
    public abstract class VariableListenerBase : MonoBehaviour
    {
        #region Types

        /// <summary>
        /// Defines when this listener subscribes and unsubscribes its handlers.
        /// </summary>
        protected enum Binding
        {
            /// <summary>Subscribe on Awake, unsubscribe on OnDestroy. Persists through enable/disable cycles.</summary>
            UNTIL_DESTROY,
            /// <summary>Subscribe on OnEnable, unsubscribe on OnDisable. Respects the active state of the GameObject.</summary>
            UNTIL_DISABLE
        }

        #endregion

        #region Inspector

        [Tooltip("Controls when this listener subscribes and unsubscribes.")]
        [SerializeField] protected Binding _binding = Binding.UNTIL_DESTROY;
        [Tooltip("If enabled, invokes listener responses once immediately after subscribing.")]
        [SerializeField] protected bool _invokeOnSubscribe = false;
        [Tooltip("If enabled, disables this GameObject right after subscription is set up.")]
        [SerializeField] protected bool _disableAfterSubscribing = false;

        #endregion

        #region Runtime

        protected readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Called by the lifecycle methods to subscribe (<c>true</c>) or unsubscribe (<c>false</c>)
        /// all configured handlers. Implemented by each generic subclass.
        /// </summary>
        protected abstract void ToggleRegistration(bool toggle);

        /// <summary>
        /// Returns true if any configured response on this listener invokes the named method.
        /// Used by editor tooling to trace method call sites across the scene.
        /// </summary>
        public abstract bool ContainsCallToMethod(string methodName);

        #endregion

        #region Unity Lifecycle

        protected virtual void Awake()
        {
            if (_binding == Binding.UNTIL_DESTROY)
                ToggleRegistration(true);

            gameObject.SetActive(!_disableAfterSubscribing);
        }

        protected virtual void OnEnable()
        {
            if (_binding == Binding.UNTIL_DISABLE)
                ToggleRegistration(true);
        }

        protected virtual void OnDisable()
        {
            if (_binding == Binding.UNTIL_DISABLE)
            {
                ToggleRegistration(false);
                _cancellationTokenSource.Cancel();
            }
        }

        protected virtual void OnDestroy()
        {
            if (_binding == Binding.UNTIL_DESTROY)
            {
                ToggleRegistration(false);
                _cancellationTokenSource.Cancel();
            }
        }

        #endregion
    }
}
