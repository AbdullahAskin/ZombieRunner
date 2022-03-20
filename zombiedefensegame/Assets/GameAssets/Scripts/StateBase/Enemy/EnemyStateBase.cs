using UnityEngine;

namespace TheyAreComing
{
    public abstract class EnemyStateBase : IStateBase
    {
        protected readonly EnemyAnimationController EnemyAnimationController;
        protected readonly Transform EnemySkinTrans;
        protected readonly Transform EnemyTrans;
        protected readonly Transform PlayerTrans;
        protected readonly EnemyStateManager StateManager;

        protected EnemyStateBase(EnemyStateManager stateManager)
        {
            StateManager = stateManager;
            PlayerTrans = EnemyManager.Player.transform;
            EnemyTrans = stateManager.EnemyBase.transform;
            EnemySkinTrans = stateManager.EnemyBase.skinTrans;
            EnemyAnimationController = stateManager.EnemyAnimationController;
        }

        protected EnemyCharacterSettings CharacterSettings =>
            StateManager.EnemyBase.enemySettingsScriptable.characterSettings;

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);
    }
}