using UnityEngine;

namespace TheyAreComing
{
	public class EnemyAnimationController : MonoBehaviour
	{
		private static readonly int Idle = Animator.StringToHash("Idle");
		private static readonly int Walk = Animator.StringToHash("Walk");
		private Animator _animator;
		private Animator animator => _animator ? _animator : _animator = GetComponentInChildren<Animator>();

		public void ToggleWalk(bool bind)
		{
			animator.SetBool(Walk, bind);
			animator.SetBool(Idle, !bind);
		}
	}
}
