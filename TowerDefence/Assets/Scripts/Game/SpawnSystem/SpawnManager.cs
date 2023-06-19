using System.Collections;
using UnityEngine;

namespace Game.Spawning
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnersRoot;

        public static Transform ProjectilesRoot;
        [SerializeField]
        private Transform projectilesRoot;
        
        [SerializeField]
        [Tooltip("in seconds")]
        private float spawnStartDelay;
        
        [SerializeField]
        [Tooltip("in seconds")]
        private float spawnInterval;

        [SerializeField]
        private int spawnCountPerWave;
        
        private Spawner[] spawners;
        private SpawnData spawnData;
        private readonly System.Random random = new();

        private void Start()
        {
            ProjectilesRoot = projectilesRoot;
            
            spawners = spawnersRoot.GetComponentsInChildren<Spawner>();
            spawnData = BuildSpawnData();
            StartCoroutine(SpawnWaves());
        }

        private SpawnData BuildSpawnData()
        {
            var tower = FindObjectOfType<Tower>();
            var towerTransform = tower.transform;
            return new SpawnData(towerTransform);
        }

        private IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(spawnStartDelay);
            while (true)
            {
                if (GameManager.Instance.GameOver)
                {
                    yield break;
                }
                
                SpawnWave();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnWave()
        {
            for (int i = 0; i < spawnCountPerWave; i++)
            {
                var spawnerIndex = random.Next(0, spawners.Length - 1);
                spawners[spawnerIndex].Spawn(spawnData);
            }
        }

        private void OnDestroy()
        {
            ProjectilesRoot = null;
        }
    }
}