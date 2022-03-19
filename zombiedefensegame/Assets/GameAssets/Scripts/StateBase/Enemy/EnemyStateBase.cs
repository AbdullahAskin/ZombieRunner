using UnityEngine;

namespace TheyAreComing
{
    public abstract class EnemyStateBase : IStateBase
    {
        protected EnemyCharacterSettings CharacterSettings;
        protected EnemyAnimationController EnemyAnimationController;
        protected Transform EnemyTrans;
        protected Transform PlayerTrans;
        protected EnemyStateManager StateManager;

        protected EnemyStateBase(EnemyStateManager stateManager)
        {
            StateManager = stateManager;
            PlayerTrans = EnemyManager.Player.transform;
            CharacterSettings = stateManager.characterSettingsScriptableObject.GetCharacterSettings();
            EnemyTrans = stateManager.EnemyBase.transform;
            EnemyAnimationController = stateManager.EnemyAnimationController;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void OnCollisionEnter(Collision collision);
    }
}