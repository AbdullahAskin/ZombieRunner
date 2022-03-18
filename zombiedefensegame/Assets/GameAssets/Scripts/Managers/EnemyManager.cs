using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyManager : MonoBehaviour
    {
        public static List<EnemyBase> EnemyBases = new List<EnemyBase>();
        private static Player _player;
        private Tween _updateEnemyTween;

        public static Player Player => _player
            ? _player
            : _player = FindObjectOfType<Player>();

        private void Awake()
        {
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
                EnemyBases = new List<EnemyBase>();
                _updateEnemyTween = DOVirtual.DelayedCall(.4f, UpdateEnemy).SetLoops(-1);
            }
            else
            {
                _updateEnemyTween?.Kill();
                EnemyBases.Clear();
            }
        }

        private void UpdateEnemy()
        {
            EnemyBases.RemoveAll(x => x.transform.position.z > Player.transform.position.z);
        }
    }
}