using Amlos.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Amlos
{
    public class DestroyableLayer : MonoBehaviour
    {
        public Tilemap tilemap;
        //public Tile tile;

        public void Awake()
        {
            Simulation.GetModel<Map>().destroyableLayer = this;
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    LibraryTile tile = allTiles[x + y * bounds.size.x] as LibraryTile;
                    if (tile != null)
                    {
                        //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                        //Debug.LogError(tile);
                        //Debug.LogError(tile.gameObject);
                        //Debug.LogError(tile.gameObject.GetComponent<DestroyableObejectScript>());
                        tile.gameObject.GetComponent<DestroyableObejectScript>().coordinate = new Vector3Int(x, y, 0);
                    }
                    else
                    {
                        //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                    }
                }
            }
        }


    }
}
