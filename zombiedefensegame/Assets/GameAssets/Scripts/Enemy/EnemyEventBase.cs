using UnityEngine;

namespace TheyAreComing
{
    public class EnemyEventBase : MonoBehaviour
    {
        [SerializeField] private float attackEffectDistance;
        private EnemyBase _enemyBase;
        private EnemyBase EnemyBase => _enemyBase ? _enemyBase : _enemyBase = GetComponentInParent<EnemyBase>();

        public void OnAttack()
        {
            var damage = EnemyBase.EnemyCharacterSettings.damage;
            var player = GameManager.Player;
            var dis = Vector3.Distance(EnemyBase.transform.position, player.transform.position);
            if (dis < attackEffectDistance && EnemyBase.IsAlive) player.PlayerCollisionManager.OnDamage(damage);
        }
    }
}