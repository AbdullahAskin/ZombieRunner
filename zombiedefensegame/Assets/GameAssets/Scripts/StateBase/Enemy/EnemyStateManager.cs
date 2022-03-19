using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(EnemyBase))]
    public class EnemyStateManager : StateManager
    {
        public EnemyCharacterSettingsScriptableObject characterSettingsScriptableObject;
        private EnemyAnimationController _enemyAnimationController;
        private EnemyBase _enemyBase;

        public EnemyAnimationController EnemyAnimationController => _enemyAnimationController
            ? _enemyAnimationController
            : _enemyAnimationController = GetComponent<EnemyAnimationController>();

        public EnemyBase EnemyBase => _enemyBase
            ? _enemyBase
            : _enemyBase = GetComponent<EnemyBase>();

        private void Start()
        {
            GetStateBases<EnemyStateBase>(this);
            SwitchState<EnemyStateIdle>(0);
        }
    }
}