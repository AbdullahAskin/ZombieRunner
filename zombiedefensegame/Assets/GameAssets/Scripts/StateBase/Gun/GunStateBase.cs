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

        protected Transform AimPivotTrans => GunManager.currentGun.aimPivotTrans;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);
    }
}