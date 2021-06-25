using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noname
{
    public class Dungeon : MonoBehaviour
    {
        public GameObject wallTile, floorTile, upStairsTile, downStairsTile;
        public GameObject doorTile;
        public Level level;
        private void Start()
        {
            level = new Level();
            level.Create();
            int pos = 0;
            for(int i = 0; i < level.height; i++, pos += level.width)
            {
                for (int j = pos; j < pos + level.width; j++)
                {
                    GameObject tileObject;
                    int tile = level.map[j];
                    switch (tile)
                    {
                        case Terrain.EMPTY:
                            continue;
                        case Terrain.GROUND:
                            tileObject = floorTile;
                            break;
                        case Terrain.WALL:
                            tileObject = wallTile;
                            break;
                        case Terrain.STAIRS_UP:
                            tileObject = upStairsTile;
                            break;
                        case Terrain.STAIRS_DOWN:
                            tileObject = downStairsTile;
                            break;
                        case Terrain.DOOR:
                            tileObject = doorTile;
                            break;
                        default:
                            continue;
                    }
                    Instantiate(tileObject, new Vector2(j - pos - 1, -i - 1), Quaternion.identity);
                }
            }
        }
    }
}