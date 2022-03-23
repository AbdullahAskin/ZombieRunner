using UnityEngine;

namespace TheyAreComing
{
    public class EnemyAnimationController : AnimationControllerBase
    {
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int DeathType = Animator.StringToHash("DeathType");
        private static readonly int AttackMirror = Animator.StringToHash("AttackMirror");

        public void OnDeath()
        {
            var deathType = Random.Range(0, 3);
            animator.SetFloat(DeathType, deathType);
            animator.SetTrigger(Death);
        }

        public void TriggerAttack(bool mirror)
        {
            SetBool(AttackMirror, mirror);
            SetTrigger(Attack);
        }
    }
}