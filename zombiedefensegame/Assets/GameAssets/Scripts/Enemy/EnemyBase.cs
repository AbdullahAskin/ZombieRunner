using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour
    {
        public Transform skinTrans;
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptable;
        private EnemyStateManager _enemyStateManager;
        public EnemyCharacterSettings EnemyCharacterSettings => characterSettingsScriptable.EnemyCharacterSettings;

        public EnemyStateManager stateManager =>
            _enemyStateManager ? _enemyStateManager : _enemyStateManager = GetComponent<EnemyStateManager>();

        public bool IsAlive { get; set; } = true;

        private void Awake()
        {
            EnemyManager.Add(this);
        }

        private void OnDisable()
        {
            EnemyManager.Remove(this);
        }

        public void Death()
        {
            EnemyManager.Remove(this);
        }
    }
}