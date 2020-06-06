using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Celular_Automata
{
    class Host
    {
        Grid map;
        bool resetting;
        Random rando;
        string empty = "  ", alive = "██", dead = "><";

        readonly object mapLock = new object();

        Vector2 mapSize = new Vector2(70, 70);

        static void Main(string[] args)
        {
            Host host = new Host();
            host.Start();
        }

        void Start()
        {
            Console.SetWindowSize(mapSize.x * 2 + 2, mapSize.y + 2);
            rando = new Random();
            Reset();
            Task input = Task.Run(() => WaitForInput());

            while (true)
            {
                if (resetting)
                    Reset();
                Update();
                Thread.Sleep(20);
            }
        }

        void WaitForInput()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else if (key.Key == ConsoleKey.F5)
                    resetting = true;
            }
        }

        void Reset()
        {
            map = new Grid(mapSize);

            for (int i = 0; i < 200; i++)
            {
                Vector2 randomPos = new Vector2(rando.Next(mapSize.x), rando.Next(mapSize.y));
                map.ToggleCell(randomPos);
            }
            resetting = false;
        }

        void Update()
        {
            bool changed = false;
            string buffer = "   Press F5 to reset\n ";
            Grid tempMap = map;
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < map.Size.y; y++)
            {
                for (int x = 0; x < map.Size.x; x++)
                {
                    if (map.GetCell(x, y))
                    {
                        if (map.ActiveNeighbors(x, y) > 3 || map.ActiveNeighbors(x, y) < 2)
                        {
                            tempMap.ToggleCell(x, y);
                            buffer += dead;
                            changed = true;
                        }
                        else
                            buffer += alive;
                    }
                    else if (map.ActiveNeighbors(x, y) == 3)
                    {
                        buffer += alive;
                        tempMap.ToggleCell(x, y);
                        changed = true;
                    }
                    else
                        buffer += empty;
                    //Thread.Sleep(1);
                }
                buffer += "\n ";
            }
            Console.Write(buffer);
            if (!changed)
            {
                resetting = true;
            }
            map = tempMap;
        }
    }

    struct Grid
    {
        public Grid(Vector2 size)
        {
            activeCells = new bool[size.x, size.y];
            Size = size;
        }

        bool[,] activeCells;

        public bool[,] GetArray()
        {
            return activeCells;
        }

        public Vector2 Size
        {
            get;
            private set;
        }

        public bool GetCell(int x, int y)
        {
            return activeCells[x, y];
        }

        public void ActiveCell(int x, int y)
        {
            activeCells[x, y] = !activeCells[x, y];
        }

        public bool GetCell(Vector2 position)
        {
            return activeCells[position.x, position.y];
        }

        public void ToggleCell(int x, int y)
        {
            activeCells[x, y] = !activeCells[x, y];
        }

        public void ToggleCell(Vector2 position)
        {
            activeCells[position.x, position.y] = !activeCells[position.x, position.y];
        }

        public Vector2[] GetNeighbours(Vector2 cell)
        {
            Vector2[] temp = new Vector2[4];
            foreach (Vector2 dir in Vector2.extraDirections)
            {
                if (dir + cell > Vector2.One * -1)
                    temp.AddToArray(dir + cell);
            }
            return temp;
        }

        public int ActiveNeighbors(Vector2 cell)
        {
            int temp = 0;
            foreach (Vector2 dir in Vector2.extraDirections)
            {
                if (dir + cell > Vector2.One * -1)
                    if (activeCells[(dir + cell).x, (dir + cell).y])
                        temp++;
            }

            return temp;
        }

        public Vector2[] GetNeighbours(int x, int y)
        {
            Vector2 cell = new Vector2(x, y);
            Vector2[] temp = new Vector2[4];
            foreach (Vector2 dir in Vector2.extraDirections)
            {
                if (dir + cell > Vector2.One * -1)
                    temp.AddToArray(dir + cell);
            }
            return temp;
        }

        public int ActiveNeighbors(int x, int y)
        {
            Vector2 cell = new Vector2(x, y);
            int temp = 0;
            foreach (Vector2 dir in Vector2.extraDirections)
            {
                Vector2 other = (dir + cell) % Size;
                    if (other.x < 0)
                        other.x = Size.x - 1;

                    if (other.y < 0)
                        other.y = Size.y - 1;

                    //if (other.x >= Size.x)
                    //    other.x = 0;

                    //if (other.y >= Size.y)
                    //    other.y = 0;

                    if (activeCells[other.x, other.y])
                        temp++;
            }

            return temp;
        }

        static public bool operator ==(Grid c1, Grid c2)
        {
            return c1.activeCells == c2.activeCells;
        }

        static public bool operator !=(Grid c1, Grid c2)
        {
            return c1.activeCells != c2.activeCells;
        }
    }

    abstract class UIElement
    {
        protected Vector2 topLeft, bottomRight;

        virtual public Vector2 TopLeft
        {
            get
            {
                return topLeft;
            }
            set
            {
                topLeft = value;
                if (topLeft.x > bottomRight.x)
                {
                    int temp = topLeft.x;
                    topLeft.x = bottomRight.x;
                    bottomRight.x = temp;
                }
                if (topLeft.y < bottomRight.y)
                {
                    int temp = topLeft.y;
                    topLeft.y = bottomRight.y;
                    bottomRight.y = temp;
                }
            }
        }

        virtual public void CorrectBounds()
        {
            if (topLeft.x > bottomRight.x)
            {
                int temp = topLeft.x;
                topLeft.x = bottomRight.x;
                bottomRight.x = temp;
            }
            if (topLeft.y < bottomRight.y)
            {
                int temp = topLeft.y;
                topLeft.y = bottomRight.y;
                bottomRight.y = temp;
            }
        }

        virtual public Vector2 BottomRight
        {
            get
            {
                return bottomRight;
            }
            set
            {
                bottomRight = value;
                if (topLeft.x > bottomRight.x)
                {
                    int temp = topLeft.x;
                    topLeft.x = bottomRight.x;
                    bottomRight.x = temp;
                }
                if (topLeft.y > bottomRight.y)
                {
                    int temp = topLeft.y;
                    topLeft.y = bottomRight.y;
                    bottomRight.y = temp;
                }
            }
        }

        public abstract void Update();
    }

    class Grid_Graphic : UIElement
    {
        Vector2 tileSize, tileCount, displaySize, topLeft;
        bool changed = false;

        ConsoleColor[,] colors;
        char[,] characters;

        public ConsoleColor[,] Colors
        {
            get { return colors; }
            set
            {
                changed = true;
                colors = value;
            }
        }

        public char[,] Characters
        {
            get { return characters; }
            set
            {
                changed = true;
                characters = value;
            }
        }

        public Grid_Graphic(Vector2 position, Vector2 displaySize, Vector2 tileCount = new Vector2(), Vector2 tileSize = new Vector2())
        {
            topLeft = position;
            BottomRight = displaySize + topLeft;

            if (tileCount > new Vector2())
            {
                this.tileSize = displaySize / tileCount;
                this.tileCount = tileCount;
                this.displaySize = displaySize;
            }
            else if (tileSize > new Vector2())
            {
                this.tileSize = tileSize;
                this.tileCount = displaySize / tileSize;
                this.displaySize = displaySize;
            }
            else
            {
                this.tileSize = Vector2.One;
                this.tileCount = displaySize;
                this.displaySize = displaySize;
            }
            Characters = new char[this.tileCount.x, this.tileCount.y];
            Colors = new ConsoleColor[this.tileCount.x, this.tileCount.y];

            changed = true;
        }

        public Vector2 GridSize
        {
            get { return displaySize; }
        }

        public void ChangeTileColor(Vector2 position, ConsoleColor color)
        {
            Colors[position.x, position.y] = color;
            changed = true;
        }

        public void ChangeTileCharacter(Vector2 position, char character)
        {
            Characters[position.x, position.y] = character;
            changed = true;
        }

        public Grid_Graphic(Vector2 position, Vector2 displaySize, object[,] referenceArray)
        {
            topLeft = position;
            BottomRight = displaySize + topLeft;
            tileSize = displaySize / new Vector2(referenceArray.GetLength(0), referenceArray.GetLength(1));
            tileCount = new Vector2(referenceArray.GetLength(0), referenceArray.GetLength(1));
            this.displaySize = displaySize;

            Colors = new ConsoleColor[tileCount.x, tileCount.y];
            changed = true;
        }

        public override void Update()
        {
            if (!changed)
                return;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(topLeft.x, topLeft.y);
            for (int y = 0; y < tileCount.y; y++)
            {
                for (int i = 0; i < tileSize.y; i++)
                {
                    Console.SetCursorPosition(topLeft.x, topLeft.y + (y - 1) * tileSize.y + i);


                    if (y + i > displaySize.y)
                        return;
                    for (int x = 0; x < tileCount.x; x++)
                    {
                        Console.ForegroundColor = Colors[x, y];
                        for (int j = 0; j < tileSize.x; j++)
                        {
                            if (x + j > displaySize.x)
                                continue;
                            Console.Write(Characters[x, y]);
                        }
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            changed = false;
        }

    }
}
