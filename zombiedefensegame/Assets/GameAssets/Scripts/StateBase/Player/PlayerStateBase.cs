using UnityEngine;

namespace TheyAreComing
{
    public abstract class PlayerStateBase : IStateBase
    {
        protected PlayerStateManager playerStateManager;
        protected PlayerSplineManager splineManager;

        protected PlayerStateBase(PlayerStateManager playerStateManager, PlayerSplineManager splineManager)
        {
            this.playerStateManager = playerStateManager;
            this.splineManager = splineManager;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);
    }
}