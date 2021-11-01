using System.Collections.Generic;
using UnityEngine;

namespace Amlos
{
    [CreateAssetMenu(fileName = "Level", menuName = "Amlos/Level")]
    public class Level : ScriptableObject
    {
        public string roomName;
        public bool canAttack = true;

        [Header("Gates")]
        public List<Gate> gatesInfo = new List<Gate>();
        public int startGateId;

        [Header("Map Prefab")]
        public GameObject levelMap;
        [Header("Spawn Points")]
        public SpawnList spawnPoints;
    }
}