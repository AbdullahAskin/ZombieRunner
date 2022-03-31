using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour
    {
        public Transform skinTrans;
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptable;
        private Collider _collider;
        private EnemyStateManager _enemyStateManager;

        private Collider Collider => _collider ? _collider : _collider = GetComponent<Collider>();
        public EnemyCharacterSettings EnemyCharacterSettings => characterSettingsScriptable.EnemyCharacterSettings;

        public EnemyStateManager StateManager =>
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
            Collider.enabled = false;
        }
    }
}