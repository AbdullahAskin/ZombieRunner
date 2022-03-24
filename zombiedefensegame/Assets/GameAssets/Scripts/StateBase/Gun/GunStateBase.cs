using UnityEngine;

namespace TheyAreComing
{
    public abstract class GunStateBase : IStateBase
    {
        protected GunGuide GunGuide;
        protected GunManager GunManager;
        protected PlayerStateManager StateManager;

        protected GunStateBase(PlayerStateManager stateManager)
        {
            StateManager = stateManager;
            GunManager = stateManager.GunManager;
            GunGuide = stateManager.GunGuide;
        }

        protected Transform AimPivotTrans => GunManager.aimPivotTrans;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}