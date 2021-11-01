using UnityEngine;
using UnityEngine.Tilemaps;

namespace Amlos
{
    [CreateAssetMenu(fileName = "Tile", menuName = "Amlos/Library Tile")]
    public class LibraryTile : TileBase
    {

        /// <summary>
        /// The Default Sprite set when creating a new Rule.
        /// </summary>
        public Sprite sprite;
        /// <summary>
        /// The Default GameObject set when creating a new Rule.
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// The Default Collider Type set when creating a new Rule.
        /// </summary>
        public Tile.ColliderType colliderType = Tile.ColliderType.Sprite;

        //public void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Debug.Log("Hit!");
        //}

        //public void OnTriggerEnter2D(Collider2D collision)
        //{
        //    Debug.Log("Hit!");
        //    Instance<DestroyableLayer>.instance.tilemap.SetTile(Coordinate, null);
        //}

        /// <summary>
        /// Retrieves any tile rendering data from the scripted tile.
        /// </summary>
        /// <param name="position">Position of the Tile on the Tilemap.</param>
        /// <param name="tilemap">The Tilemap the tile is present on.</param>
        /// <param name="tileData">Data to render the tile.</param>
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            var iden = Matrix4x4.identity;

            tileData.sprite = sprite;
            tileData.gameObject = gameObject;
            //tileData.gameObject.GetComponent<DestroyableObejectScript>().Coordinate = position;
            tileData.colliderType = colliderType;
            tileData.flags = TileFlags.LockTransform;
            tileData.transform = iden;
        }
    }
}

