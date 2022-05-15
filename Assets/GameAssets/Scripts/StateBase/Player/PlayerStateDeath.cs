using DG.Tweening;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateDeath : PlayerStateBase
    {
        private GameService _gameService;

        public PlayerStateDeath(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        public GameService GameService => _gameService
            ? _gameService
            : _gameService = ServiceManager.GetService<GameService>();

        public override void EnterState()
        {
            GameService.NotifyGameStateChange(GameState.Fail);
            StateManager.Player.OnDeath();
            Movement.SetSpeed(0f, 0f);
            AnimationController.ToggleDeath(true);
        }

        public override void ExitState()
        {
            AnimationController.ToggleDeath(false);
        }

        public override void UpdateState()
        {
        }
    }
}