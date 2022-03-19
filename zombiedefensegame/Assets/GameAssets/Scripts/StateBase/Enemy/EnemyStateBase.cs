using UnityEngine;

namespace TheyAreComing
{
    public abstract class EnemyStateBase : IStateBase
    {
        protected readonly EnemyCharacterSettings CharacterSettings;
        protected readonly EnemyAnimationController EnemyAnimationController;
        protected readonly Transform EnemyTrans;
        protected readonly Transform PlayerTrans;
        protected readonly EnemyStateManager StateManager;

        protected EnemyStateBase(EnemyStateManager stateManager)
        {
            StateManager = stateManager;
            PlayerTrans = EnemyManager.Player.transform;
            CharacterSettings = stateManager.EnemyBase.enemySettings;
            EnemyTrans = stateManager.EnemyBase.transform;
            EnemyAnimationController = stateManager.EnemyAnimationController;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);
    }
}