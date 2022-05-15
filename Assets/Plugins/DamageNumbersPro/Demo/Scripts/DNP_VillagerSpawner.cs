using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageNumbersPro.Demo
{
    public class DNP_VillagerSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public Vector3 fromPosition;
        public Vector3 toPosition;

        float nextSpawnTime;

        void Start()
        {
            nextSpawnTime = Time.time + 1f;
            DNP_Enemy.count = 0;
        }

        void FixedUpdate()
        {
            if(Time.time > nextSpawnTime && DNP_Enemy.count < 4)
            {
                SpawnVillager();
            }
        }

        void SpawnVillager()
        {
            nextSpawnTime = Time.time + 2f * Random.value + 3f;

            GameObject newVillager = Instantiate<GameObject>(prefab);
            newVillager.transform.position = Vector3.Lerp(fromPosition, toPosition, Random.value);
            newVillager.SetActive(true);
        }
    }
}