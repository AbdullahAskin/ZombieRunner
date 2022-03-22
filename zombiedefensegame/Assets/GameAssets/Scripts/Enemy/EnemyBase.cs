using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour
    {
        public Transform skinTrans;
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptable;
        private EnemyStateManager _enemyStateManager;
        public EnemyCharacterSettings EnemyCharacterSettings => characterSettingsScriptable.EnemyCharacterSettings;

        private EnemyStateManager EnemyStateManager =>
            _enemyStateManager ? _enemyStateManager : _enemyStateManager = GetComponent<EnemyStateManager>();


        private void Awake()
        {
            EnemyManager.EnemyBases.Add(this);
        }

        private void OnDisable()
        {
            EnemyManager.EnemyBases.Remove(this);
        }

        public void Death()
        {
            EnemyStateManager.SwitchState<EnemyStateDeath>(0);
            EnemyManager.EnemyBases.Remove(this);
        }
    }
}