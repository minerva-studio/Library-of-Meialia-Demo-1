using Amlos.Core;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Amlos.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public SpawnList spawnPoints;

        private void Start()
        {

        }

        public void SpawnAll()
        {
            foreach (var spawnPoint in spawnPoints.point)
            {
                Spawn(spawnPoint);
            }
        }

        private void Spawn(SpawnPoint spawnPoint)
        {
            for (int i = 1; i <= spawnPoint.count; i++)
            {
                GameObject enemy = Instantiate(spawnPoint.enemyPrefab);
                enemy.transform.position = GetSpawnPoint(spawnPoint);
            }
        }

        private Vector3 GetSpawnPoint(SpawnPoint spawnPoint)
        {
            switch (spawnPoint.spawnType)
            {
                case SpawnPoint.SpawnType.coordinated:
                    return spawnPoint.spawnPoint;
                //return Simulation.GetModel<Map>().GetTileCoordinate(spawnPoint.spawnPoint);
                case SpawnPoint.SpawnType.range:
                    Vector2 vector;
                    int trail = 0;
                    do
                    {
                        var x = UnityEngine.Random.Range(-spawnPoint.radius, spawnPoint.radius);
                        var y = UnityEngine.Random.Range(-spawnPoint.radius, spawnPoint.radius);
                        vector = new Vector2(x, y);
                        trail++;
                        Debug.Log("Finding coordinate");
                    } while (trail < 100&&vector.magnitude < spawnPoint.radius);
                    return (Vector3)vector + spawnPoint.spawnPoint;
                default:
                    break;
            }
            return spawnPoint.spawnPoint;
        }
    }
}