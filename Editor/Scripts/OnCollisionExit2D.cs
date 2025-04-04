using System;
using Unity.VisualScripting;

// namespace Unity.VisualScripting
// {
// #if MODULE_PHYSICS_2D_EXISTS
    /// <summary>
    /// Called when a collider on another object stops touching this object's collider.
    /// </summary>

    [UnitTitle("On Collision Exit 2D")]
    [TypeIcon(typeof(Unity.VisualScripting.OnCollisionExit2D))]
    public sealed class OnCollisionExit2D : CollisionEvent2DUnit
    {
        public override Type MessageListenerType => typeof(UnityOnCollisionExit2DMessageListener);
        protected override string hookName => EventHooks.OnCollisionExit2D;
    }
// #endif
// }
