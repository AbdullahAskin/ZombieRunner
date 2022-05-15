using DG.Tweening;

namespace TheyAreComing
{
    public class EnemyStateAttack : EnemyStateBase
    {
        private Tween _movementTween;

        public EnemyStateAttack(EnemyStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            var isMirroredAttack = Player.Position.x > EnemyTrans.position.x;
            EnemyAnimationController.TriggerAttack(isMirroredAttack);
            _movementTween = DOTween.Sequence()
                .Append(SetRelativeSpeed(CharacterSettings.Speed + 3f, .5f))
                .Append(SetRelativeSpeed(3f, 2f));
        }

        public override void ExitState()
        {
            _movementTween?.Kill();
        }

        public override void UpdateState()
        {
            Move();
            Rotate();
        }
    }
}