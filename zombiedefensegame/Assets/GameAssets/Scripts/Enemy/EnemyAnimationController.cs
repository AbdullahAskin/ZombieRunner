using UnityEngine;

namespace TheyAreComing
{
    public class EnemyAnimationController : MonoBehaviour
    {
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int DeathType = Animator.StringToHash("DeathType");
        private Animator _animator;
        private Animator animator => _animator ? _animator : _animator = GetComponentInChildren<Animator>();

        public void ToggleWalk(bool bind)
        {
            animator.SetBool(Walk, bind);
            animator.SetBool(Idle, !bind);
        }

        public void OnDeath()
        {
            var deathType = Random.Range(0, 3);
            animator.SetFloat(DeathType, deathType);
            animator.SetTrigger(Death);
        }

        public void SetTrigger(int animId)
        {
            animator.SetTrigger(animId);
        }
    }
}