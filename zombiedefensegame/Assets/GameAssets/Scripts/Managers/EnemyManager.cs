using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyManager : MonoBehaviour
    {
        public static List<EnemyBase> enemyBases = new List<EnemyBase>();
        private Player _player;
        private Tween _updateEnemyTween;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            ToggleSubscribes(true);
        }

        private void OnDisable()
        {
            ToggleSubscribes(false);
        }

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
            enemyBases.RemoveAll(x => x.transform.position.z > _player.transform.position.z);
        }
    }
}