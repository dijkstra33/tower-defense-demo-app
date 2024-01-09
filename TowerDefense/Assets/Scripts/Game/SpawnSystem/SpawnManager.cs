using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game.SpawnSystem
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
        
        [SerializeField]
        private Portal _portalPrefab;

        [SerializeField]
        private float _portalLifetime;
        
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
            var randomSpawners = 
                spawners
                    .OrderBy(_ => random.Next())
                    .Take(spawnCountPerWave)
                    .ToArray();

            foreach (var randomSpawner in randomSpawners)
            {
                randomSpawner.SpawnUnit(spawnData);
                randomSpawner.Spawn(_portalPrefab, portal => portal.Init(_portalLifetime), spawnData);
            }
        }

        private void OnDestroy()
        {
            ProjectilesRoot = null;
        }
    }
}