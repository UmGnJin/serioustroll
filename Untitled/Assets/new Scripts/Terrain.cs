
namespace noname
{
	public class Terrain
	{
		public const int EMPTY = 0;
		public const int GROUND = 1;
		public const int WALL = 2;

		public const int DOOR = 3;
		public const int DOOR_OPEN = 4;
		public const int DOOR_LOCKED = 5;
		public const int DOOR_HIDDEN = 6;

		public const int STAIRS_UP = 7;
		public const int STAIRS_DOWN = 8;

		public int discover(int terr)
		{
			switch (terr)
			{
				case DOOR_HIDDEN:
					return DOOR;
				default:
					return terr;
			}
		}

	}
}