using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public abstract class EnemyStateBase : IStateBase
    {
        private static float _currentSpeed;
        private readonly Transform _enemySkinTrans;
        protected readonly EnemyAnimationController EnemyAnimationController;
        protected readonly Transform EnemyTrans;
        protected readonly Player Player;
        protected readonly Transform PlayerTrans;
        protected readonly EnemyStateManager StateManager;

        private Tween _movementTween;

        protected EnemyStateBase(EnemyStateManager stateManager)
        {
            Player = EnemyManager.Player;
            StateManager = stateManager;
            PlayerTrans = EnemyManager.Player.transform;
            EnemyTrans = stateManager.EnemyBase.transform;
            _enemySkinTrans = stateManager.EnemyBase.skinTrans;
            EnemyAnimationController = stateManager.EnemyAnimationController;
        }

        protected EnemyCharacterSettings CharacterSettings =>
            StateManager.EnemyBase.EnemyCharacterSettings;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);

        protected void Rotate()
        {
            var targetRotation = Quaternion.LookRotation(PlayerTrans.position - EnemyTrans.position, Vector3.up);
            _enemySkinTrans.rotation = Quaternion.Slerp(_enemySkinTrans.rotation, targetRotation, .3f);
        }

        protected void Move()
        {
            var step = _currentSpeed * Time.fixedDeltaTime;
            EnemyTrans.position = Vector3.MoveTowards(EnemyTrans.position, PlayerTrans.position, step);
        }

        protected void SetSpeed(float from, float to, float duration)
        {
            _movementTween?.Kill();
            _movementTween = DOVirtual.Float(from, to, duration, x => _currentSpeed = x);
        }

        protected void SetRelativeSpeed(float amount, float duration)
        {
            _movementTween?.Kill();
            _movementTween = DOVirtual.Float(_currentSpeed, _currentSpeed + amount, duration, x => _currentSpeed = x);
        }
    }
}