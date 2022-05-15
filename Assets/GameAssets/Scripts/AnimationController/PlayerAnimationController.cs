using UnityEngine;

namespace TheyAreComing
{
    public class PlayerAnimationController : AnimationControllerBase
    {
        public static readonly int Hit = Animator.StringToHash("Hit");
        public static readonly int Death = Animator.StringToHash("Death");

        public void ToggleDeath(bool bind)
        {
            if (bind)
            {
                animator.SetTrigger(Death);
                animator.SetLayerWeight(1, 0);
            }
            else
            {
                animator.SetLayerWeight(1, 1);
            }
        }

        public void ResetAnimatorToWalk()
        {
            animator.Rebind();
            ToggleWalk(true);
        }   
    }
}