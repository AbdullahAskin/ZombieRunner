using UnityEngine;

namespace TheyAreComing
{
    public abstract class AnimationControllerBase : MonoBehaviour
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private Animator _animator;
        protected Animator animator => _animator ? _animator : _animator = GetComponentInChildren<Animator>();

        public void ToggleWalk(bool bind)
        {
            animator.SetBool(Walk, bind);
            animator.SetBool(Idle, !bind);
        }
        
        public void SetTrigger(int animId)=>animator.SetTrigger(animId);

        public void SetBool(int animId,bool state) => animator.SetBool(animId,state);
    }
}