using Amlos.Core;
using UnityEngine;

namespace Amlos
{

    public class Map
    {
        //public Tilemap floor;
        public MapTiles tiles;
        public MapCollider mapColliders;
        public DestroyableLayer destroyableLayer;

        public Vector3Int GetTileCoordinate(Vector3 wordPosition)
        {
            //Debug.Log("coordination converting");
            var ret = tiles.floor.WorldToCell(wordPosition);
            //Debug.Log(ret);
            //ret = tiles.wall.WorldToCell(wordPosition);
            //Debug.Log(ret);
            //ret = tiles.objects.WorldToCell(wordPosition);
            //Debug.Log(ret);
            //ret = tiles.platform.WorldToCell(wordPosition);
            //Debug.Log(ret);
            return ret;
        }

        public bool IsCoordinateHasTile(Vector3Int coordinate)
        {
            //Debug.Log(coordinate + "?");
            if (tiles.floor.GetTile(coordinate)) return true;
            if (tiles.wall.GetTile(coordinate)) return true;
            if (tiles.objects.GetTile(coordinate)) return true;
            if (tiles.platform.GetTile(coordinate)) return true;
            //Debug.Log(coordinate + " has no tile");
            return false;
        }

        public bool IsCoordinateHasTile(Vector3 worldPosition)
        {
            //Debug.Log("Checking empty");
            var coordinate = GetTileCoordinate(worldPosition);
            return IsCoordinateHasTile(coordinate);
        }

        public bool IsOnGround(Vector3 worldPosition, int horizontal)
        {
            var coordinate = GetTileCoordinate(worldPosition);
            for (int i = 0; i <= horizontal; i++)
            {
                coordinate.y--;
                bool hasTile = IsCoordinateHasTile(coordinate);
                if (hasTile) return true;
            }
            return false;
        }
        public bool IsOnGround(Vector3Int coordinate, int horizontal)
        {
            for (int i = 0; i < horizontal; i++)
            {
                coordinate.y--;
                bool hasTile = IsCoordinateHasTile(coordinate);
                if (hasTile) return hasTile;
            }
            return false;
        }
    }

    public static class MapExtension
    {
        public static bool CanStandOn(this Vector3Int coordinate)
        {
            bool v = !Simulation.GetModel<Map>().IsCoordinateHasTile(coordinate);
            //Debug.Log(tile.coordinate + " : " + v);
            return v;//&& !Simulation.GetModel<Map>().IsOnGround(tile.coordinate, 0);
        }
    }
}