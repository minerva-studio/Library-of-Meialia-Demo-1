using Amlos.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Amlos
{
    public class DestroyableObejectScript : MonoBehaviour
    {
        public Tilemap tilemap;
        public Vector3Int coordinate;
        public bool isDestroying;

        private void Start()
        {
            tilemap = Simulation.GetModel<Map>().destroyableLayer.tilemap;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Hit!");
            if (!isDestroying)
            {
                isDestroying = true;
                StartCoroutine(DestroySelf());
            }
        }

        IEnumerator DestroySelf()
        {
            yield return new WaitForEndOfFrame();
            tilemap.SetTile(tilemap.WorldToCell(transform.position), null);
        }
    }
}

