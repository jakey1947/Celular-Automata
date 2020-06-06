using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celular_Automata
{
    /// <summary>
    /// Author : Kieran Pavy
    /// Represents a 2D spacial position with 2 ints. specifically for Tile Set and Console Applications
    /// </summary>
    public struct Vector2
    {
        public int y, x;

        public Vector2(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        public Vector2(Vector2 position)
        {
            y = position.y;
            x = position.x;
        }

        public void Clamp(Vector2 limits)
        {
            if (y > limits.y)
                y = limits.y;
            else if (y < 0)
                y = 0;
            if (x > limits.x)
                x = limits.x;
            else if (x < 0)
                x = 0;
        }
        public void Clamp()
        {
            if (y < 0)
                y = 0;
            if (x < 0)
                x = 0;
        }

        public int[] Coords()
        {
            return new int[] { x, y };
        }

        public string ToString()
        {
            return "(" + x + "," + y + ")";
        }

        public static bool operator ==(Vector2 c1, Vector2 c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Vector2 c1, Vector2 c2)
        {
            return !c1.Equals(c2);
        }

        public static Vector2 operator -(Vector2 c1, Vector2 c2)
        {
            return new Vector2(c1.x - c2.x, c1.y - c2.y);
        }

        public static Vector2 operator +(Vector2 c1, Vector2 c2)
        {
            return new Vector2(c1.x + c2.x, c1.y + c2.y);
        }

        public static bool operator <(Vector2 c1, Vector2 c2)
        {
            return (c1.x < c2.x && c1.y < c2.y);
        }

        public static bool operator >(Vector2 c1, Vector2 c2)
        {
            return (c1.x > c2.x && c1.y > c2.y);
        }

        public static bool operator <=(Vector2 c1, Vector2 c2)
        {
            return (c1.x == c2.x  || c1.x < c2.x) && ( c1.y < c2.y || c1.y == c2.y);
        }

        public static bool operator >=(Vector2 c1, Vector2 c2)
        {
            return (c1.x == c2.x  || c1.x > c2.x) && ( c1.y > c2.y || c1.y == c2.y);
        }

        public static Vector2 operator /(Vector2 c1, Vector2 c2)
        {
            return new Vector2(c1.x / c2.x , c1.y / c2.y);
        }

        public static Vector2 operator *(Vector2 c1, Vector2 c2)
        {
            return new Vector2(c1.x * c2.x , c1.y * c2.y);
        }

        public static Vector2 operator *(Vector2 c1, int c2)
        {
            return new Vector2(c1.x * c2 , c1.y * c2);
        }

        public static Vector2 operator %(Vector2 c1, Vector2 c2)
        {
            return new Vector2(c1.x % c2.x, c1.y % c2.y);
        }


        public static Vector2 One
        {
            get { return new Vector2(1, 1); }
        }

        public static Vector2 Up
        {
            get { return new Vector2(0, -1); }
        }

        public static Vector2 Down
        {
            get { return new Vector2(0, 1); }
        }

        public static Vector2 Left
        {
            get { return new Vector2(-1, 0); }
        }

        public static Vector2 Right
        {
            get { return new Vector2(1, 0); }
        }

        public static Vector2[] directions
        {
            get
            {
                return new Vector2[] { Up, Right, Down, Left };
            }
        }

        public static Vector2[] extraDirections
        {
            get
            {
                return new Vector2[] { Up, Up + Right, Right, Right + Down, Down, Down + Left, Left, Left + Up };
            }
        }
    }
}
