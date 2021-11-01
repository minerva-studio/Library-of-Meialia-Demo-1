using Minerva.Module;
using UnityEngine;

namespace Amlos
{
    public class MapLoader : SingletonScript<MapLoader>
    {
        public Transform anchor;
        public GameObject current;

        public void Load(GameObject map)
        {
            DestroyImmediate(current);
            current = Instantiate(map, anchor);
        }
    }
}