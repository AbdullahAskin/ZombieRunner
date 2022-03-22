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
        protected float CurrentSpeed;

        protected EnemyStateBase(EnemyStateManager stateManager)
        {
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
            var step = CurrentSpeed * Time.fixedDeltaTime;
            EnemyTrans.position = Vector3.MoveTowards(EnemyTrans.position, PlayerTrans.position, step);
        }
    }
}