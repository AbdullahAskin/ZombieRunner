using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class SpawnManager : MonoBehaviour
    {
        private static Tween _updateSpawnablesTween;
        [SerializeField] private List<SpawnerBase> spawnerBases;
        private Vector3 PlayerPos => GameManager.Player.transform.position;

        public void ToggleSpawn(bool bind)
        {
            if (bind) _updateSpawnablesTween = DOVirtual.DelayedCall(.5f, UpdateSpawnables).SetLoops(-1);
            else _updateSpawnablesTween?.Kill();
        }

        private void UpdateSpawnables()
        {
            foreach (var spawnerBase in spawnerBases)
            {
                spawnerBase.RemoveCheck();

                var lastSpawnPos = spawnerBase.LastSpawnPos;
                var distance = Vector3.Distance(lastSpawnPos, PlayerPos);
                var spawnDistance = spawnerBase.TargetDistance;
                if (lastSpawnPos.z > PlayerPos.z || spawnDistance > distance) continue;

                spawnerBase.SpawnObject();
            }
        }
    }
}