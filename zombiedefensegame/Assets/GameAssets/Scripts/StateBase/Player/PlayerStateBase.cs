using UnityEngine;

namespace TheyAreComing
{
    public abstract class PlayerStateBase : IStateBase
    {
        protected PlayerStateManager PlayerStateManager;
        protected PlayerSplineManager SplineManager;

        protected PlayerStateBase(PlayerStateManager playerStateManager)
        {
            PlayerStateManager = playerStateManager;
            SplineManager = playerStateManager.PlayerSplineManager;
        }

        protected PlayerCharacterSettings CharacterSettings => PlayerStateManager.Player.PlayerCharacterSettings;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);
    }
}