using UnityEngine;

namespace TheyAreComing
{
    public class PlayerAnimationController : AnimationControllerBase
    {
        public static readonly int Hit = Animator.StringToHash("Hit");
        public static readonly int Death = Animator.StringToHash("Death");

        public void OnDeath()
        {
            animator.SetTrigger(Death);
            animator.SetLayerWeight(1, 0);
        }
    }
}