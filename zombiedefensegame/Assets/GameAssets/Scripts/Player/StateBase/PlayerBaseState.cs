using UnityEngine;

namespace TheyAreComing
{
    public abstract class PlayerBaseState
    {
        protected PlayerStateManager stateManager;
        protected PlayerSplineManager splineManager;
        
        public PlayerBaseState(PlayerStateManager stateManager,PlayerSplineManager splineManager)
        {
            this.stateManager = stateManager;
            this.splineManager = splineManager;
        }
        
       public abstract void EnterState(PlayerStateManager playerStateManager);
       public abstract void ExitState(PlayerStateManager playerStateManager);
       public abstract void UpdateState(PlayerStateManager playerStateManager);
       public abstract void OnCollisionEnter(PlayerStateManager playerStateManager,Collision collision);
    } 
}