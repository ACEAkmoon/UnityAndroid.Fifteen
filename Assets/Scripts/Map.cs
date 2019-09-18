using UnityEngine;

struct Map {
        int size;
        int[,] map;

        public Map (int size)
        {
            this.size = size;
            map = new int[size, size];
        }

        public void Set(Coord xy, int value)
        {
            if (xy.CheckBoard(size))
                map[xy.x, xy.y] = value;
        }

        public int Get(Coord xy)
        {
            if (xy.CheckBoard(size))
                return map[xy.x, xy.y];
            return 0;
        }

        public void Copy(Coord from, Coord to)
        {
            Set(to, Get(from));
        }
    }
