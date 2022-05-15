namespace TheyAreComing
{
    public abstract class PlayerStateBase : IStateBase
    {
        protected PlayerAnimationController AnimationController;
        protected PlayerMovement Movement;
        protected PlayerStateManager StateManager;

        protected PlayerStateBase(PlayerStateManager stateManager)
        {
            StateManager = stateManager;
            AnimationController = stateManager.PlayerAnimationController;
            Movement = stateManager.PlayerMovement;
        }

        protected PlayerCharacterSettings CharacterSettings => StateManager.Player.PlayerCharacterSettings;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}