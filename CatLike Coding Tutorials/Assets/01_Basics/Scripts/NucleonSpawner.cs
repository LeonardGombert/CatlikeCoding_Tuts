using System;
using UnityEngine;

namespace CatlikeCoding.Basics.FramesPerSecond
{
    public class NucleonSpawner : MonoBehaviour
    {
        public float timeBetweenSpawns;
        public float spawnDistance;
        public Nucleon[] nucleaonPrefab;

        float timeSinceLastSpawn;

        //use fixedUpdate to keep spawning independent from the frame rate
        void FixedUpdate()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if(timeSinceLastSpawn > timeBetweenSpawns)
            {
                timeSinceLastSpawn -= timeSinceLastSpawn;
                SpawnNucleon();
            }
        }

        private void SpawnNucleon()
        {
            Nucleon prefab = nucleaonPrefab[UnityEngine.Random.Range(0, nucleaonPrefab.Length)];
            Nucleon spawn = Instantiate(prefab);
            spawn.transform.localPosition = UnityEngine.Random.onUnitSphere * spawnDistance;
            spawn.transform.parent = transform;
        }
    }
}