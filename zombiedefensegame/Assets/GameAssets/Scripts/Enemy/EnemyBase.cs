using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private ParticleSystem spawnParticle;
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

        private void OnEnable()
        {
            spawnParticle.transform.position =
                GameManager.CurvedWorldController.TransformPosition(spawnParticle.transform.position);
            spawnParticle.Play();
        }

        private void OnDisable()
        {
            EnemyManager.Remove(this);
        }

        public void Death()
        {
            GameManager.PlayMenu.IncreaseScore(EnemyCharacterSettings.score);
            EnemyManager.Remove(this);
            Collider.enabled = false;
        }

        public void Disappear()
        {
            SpawnerBase.RemoveSpawn(transform);
            Death();
            Destroy(gameObject);
        }
    }
}