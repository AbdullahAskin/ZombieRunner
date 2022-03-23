namespace TheyAreComing
{
    public abstract class PlayerStateBase : IStateBase
    {
        protected PlayerAnimationController AnimationController;
        protected PlayerSplineManager SplineManager;
        protected PlayerStateManager StateManager;

        protected PlayerStateBase(PlayerStateManager stateManager)
        {
            StateManager = stateManager;
            AnimationController = stateManager.PlayerAnimationController;
            SplineManager = stateManager.PlayerSplineManager;
        }

        protected PlayerCharacterSettings CharacterSettings => StateManager.Player.PlayerCharacterSettings;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}