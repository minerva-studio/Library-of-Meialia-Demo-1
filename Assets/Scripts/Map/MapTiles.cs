using Amlos.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Amlos
{
    public class MapTiles : MonoBehaviour
    {
        public Tilemap floor;
        public Tilemap wall;
        public Tilemap platform;
        public Tilemap objects;


        private void Awake()
        {
            Simulation.GetModel<Map>().tiles = this;
        }

    }
}