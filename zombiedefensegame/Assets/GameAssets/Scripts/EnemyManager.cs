using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
	public class EnemyManager : MonoBehaviour
	{
		public static List<EnemyBase> enemyBases = new List<EnemyBase>();
		public Player player;
		private Tween _updateEnemyTween;
		
		private void Awake() => ToggleSubscribes(true);
		private void OnDisable() => ToggleSubscribes(false);

		private void ToggleSubscribes(bool bind)
		{
			if (bind)
			{
				enemyBases = new List<EnemyBase>();
				_updateEnemyTween = DOVirtual.DelayedCall(.4f, UpdateEnemy).SetLoops(-1);
			}
			else
			{
				_updateEnemyTween?.Kill();
				enemyBases.Clear();
			}
		}

		private void UpdateEnemy()
		{
			enemyBases.RemoveAll(x => x.transform.position.z > player.transform.position.z);
		}

	}
}
