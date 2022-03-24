using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public abstract class EnemyStateBase : IStateBase
    {
        private readonly Transform _enemySkinTrans;
        protected readonly EnemyAnimationController EnemyAnimationController;
        protected readonly Transform EnemyTrans;
        protected readonly Transform PlayerTrans;
        protected readonly EnemyStateManager StateManager;
        private float _currentSpeed;

        protected EnemyStateBase(EnemyStateManager stateManager)
        {
            StateManager = stateManager;
            PlayerTrans = GameManager.Player.transform;
            EnemyTrans = stateManager.EnemyBase.transform;
            _enemySkinTrans = stateManager.EnemyBase.skinTrans;
            EnemyAnimationController = stateManager.EnemyAnimationController;
        }

        protected EnemyCharacterSettings CharacterSettings =>
            StateManager.EnemyBase.EnemyCharacterSettings;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();

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

        protected Tween SetSpeed(float from, float to, float duration)
        {
            return DOVirtual.Float(from, to, duration, x => _currentSpeed = x).SetEase(Ease.Linear);
        }

        protected Tween SetRelativeSpeed(float target, float duration)
        {
            return DOVirtual.Float(_currentSpeed, target, duration, x => _currentSpeed = x).SetEase(Ease.Linear);
        }
    }
}