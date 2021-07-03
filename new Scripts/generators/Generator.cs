using System.Collections;
using System.Collections.Generic;
using noname.rooms;

namespace noname.generator
{
    public abstract class Generator
    {
        public abstract void SetStairRooms(List<Room> rooms); //출입구방 생성
        public abstract void SetRooms(List<Room> rooms); // 일반 방 생성
        public abstract void PlaceRooms(List<Room> rooms); //방 위치 설정
        public abstract void MoveRooms(List<Room> rooms);
        public abstract bool CheckOverlap(Room r, List<Room> rooms);
        public abstract bool CheckOverlap(Room r, List<Room> rooms, out int index);
        public abstract void PaintRooms(List<Room> rooms);

        public abstract void Generate();
    }
}