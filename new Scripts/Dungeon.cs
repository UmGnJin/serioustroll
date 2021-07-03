using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noname
{
    public class Dungeon : MonoBehaviour
    {
        public GameObject wallTile, floorTile, upStairsTile, downStairsTile;
        public GameObject doorTile;
        public List<Level> levels = new List<Level>();
        public Level currentlevel;
        public bool changed = false;
        private void Start()
        {
            currentlevel = new Level();
            currentlevel.Create();
            levels.Add(currentlevel);
            PrintLevel();
        }
        public void Nextlevel()
        {
            Level l = new Level();
            //기존에 깔린 판 치우기(어차피 레벨 자체에 맵 정보는 저장되어 있으니 상관없음)
            Transform[] allChildren = GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == transform.name)
                    continue;
                Destroy(child.gameObject);
            }

            //새로 판 깔기
            l.Create();
            levels.Add(l);
            currentlevel = l;
            PrintLevel();
        }//여기 판 갈아주세요 (판 치우고 새로 깔아야 한다.)
        public void PrintLevel()
        {
            int pos = 0;
            for (int i = 0; i < currentlevel.height; i++, pos += currentlevel.width)
            {
                for (int j = pos; j < pos + currentlevel.width; j++)
                {
                    GameObject tileObject;
                    int tile = currentlevel.map[j];
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
                    GameObject newTile = Instantiate(tileObject, new Vector2(j - pos - 1, -i - 1), Quaternion.identity) as GameObject;
                    newTile.transform.SetParent(this.transform, false);
                }
            }
        }//현재 레벨의 맵 화면상에 출력

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && changed == false)
            {
                Nextlevel();
                changed = true;
            }
        }
    }
}