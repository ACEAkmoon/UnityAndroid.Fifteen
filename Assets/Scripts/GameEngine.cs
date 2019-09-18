using System;
using UnityEngine;

public class Game : MonoBehaviour {
        int size;
        Map map;
        Coord space;

        public int moves { get; private set; }

        public Game(int size)
        {
            this.size = size;
            map = new Map(size);
        }

        public void Start (int mixingValue)
        {
            int digit = 0;

            foreach (Coord xy in new Coord().GiveCoord(size))
                map.Set(xy, ++digit);
            space = new Coord(size - 1, size - 1);
            if (mixingValue > 0)
                Shuffle(mixingValue);
            moves = 0;
        }

        void Shuffle(int mixingValue)
        {
            System.Random random = new System.Random(mixingValue);
            for (int j = 0; j < mixingValue; j++)
                Press(random.Next(size), random.Next(size));
        }

        public int Press (int x, int y)
        {
            return Press (new Coord (x, y));
        }

        int Press(Coord xy)
        {
            if (space.Equals(xy)) return 0;
            if (xy.x != space.x && xy.y != space.y)
                return 0;

            int steps = Math.Abs (xy.x - space.x) +
                        Math.Abs (xy.y - space.y);

            while (xy.x != space.x)
                Shift(Math.Sign(xy.x - space.x), 0);

            while (xy.y != space.y)
                Shift(0, Math.Sign(xy.y - space.y));
            moves += steps;

            return steps;
        }

        void Shift(int sx, int sy)
        {
            Coord next = space.Add(sx, sy);
            map.Copy(next, space);
            space = next;

        }

        public int GetDigit (int x, int y)
        {
            return GetDigit (new Coord (x, y));
        }

        int GetDigit(Coord xy)
        {
            if (space.Equals(xy))
                return 0;
            return map.Get(xy);
        }

        public bool Solved ()
        {
            if (!space.Equals(new Coord(size)))
                return false;
            int digit = 0;
            foreach (Coord xy in new Coord().GiveCoord(size))
                if (map.Get(xy) != ++digit)
                    return space.Equals (xy);
            return true;
        }
 }
