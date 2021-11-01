using Amlos.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Amlos
{
    //public class GateLayer : MonoBehaviour
    //{
    //    public Tilemap tilemap;
    //    //public Tile tile;

    //    public void OnValidate()
    //    {
    //        tilemap = GetComponent<Tilemap>();
    //    }


    //    public void Awake()
    //    {
    //        Simulation.GetModel<Map>().gateLayer = this;
    //        BoundsInt bounds = tilemap.cellBounds;
    //        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

    //        for (int x = 0; x < bounds.size.x; x++)
    //        {
    //            for (int y = 0; y < bounds.size.y; y++)
    //            {
    //                var tile = allTiles[x + y * bounds.size.x] as GateTile;
    //                if (tile != null)
    //                {
    //                    tile.gameObject.GetComponent<GateScript>().coordinate = new Vector3Int(x, y, 0);
    //                }
    //            }
    //        }
    //    }


    //}
}