using System;
using UnityEngine;

namespace Amlos
{
    [CreateAssetMenu(fileName = "Spawn Anchor", menuName = "Amlos/Spawn Anchor")]
    public class SpawnList : ScriptableObject
    {
        public SpawnPoint[] point;

        public void SpawnAll()
        {
            foreach (var spawnPoint in point)
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
                Debug.Log("Spawn enemy");
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
                    do
                    {
                        var x = UnityEngine.Random.Range(-spawnPoint.radius, spawnPoint.radius);
                        var y = UnityEngine.Random.Range(-spawnPoint.radius, spawnPoint.radius);
                        vector = new Vector2(x, y);
                    } while (vector.magnitude < spawnPoint.radius);
                    return (Vector3)vector + spawnPoint.spawnPoint;
                default:
                    break;
            }
            return spawnPoint.spawnPoint;
        }
    }
    [Serializable]
    public class SpawnPoint
    {
        public enum SpawnType
        {
            [InspectorName("exact Position")]
            //Exact Position that assigned
            coordinated,
            [InspectorName("random in radius")]
            //[InspectorName("a random Position that around the assigned position in the radius")]
            range,
            [InspectorName("total random")]
            //[InspectorName("a totally random position")]
            totalRandom
        }

        public GameObject enemyPrefab;
        public Vector3 spawnPoint;
        public SpawnType spawnType;
        public int count;
        public int radius;
    }
}