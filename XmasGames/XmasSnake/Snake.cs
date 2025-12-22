using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmasGames.XmasSnake
{
    internal class Snake
    {
        public List<(int x, int y)> Body { get; private set; } = new List<(int x, int y)>();
        public (int x, int y) Direction { get; private set; } = (1, 0); // startar åt höger

        public Snake(int startX, int startY)
        {
            Body.Add((startX, startY));
        }

        public void SetDirection((int x, int y) newDirection)
        {
            if (Body.Count > 1 && newDirection.x == -Direction.x && newDirection.y == -Direction.y)
                return;
            Direction = newDirection;
        }

        public void Move(bool grow = false)
        {
            var head = Body[0];
            var newHead = (head.x + Direction.x, head.y + Direction.y);
            Body.Insert(0, newHead);

            if (!grow)
                Body.RemoveAt(Body.Count - 1);
        }

        public bool CheckCollisionAt((int x, int y) pos, int gridWidth, int gridHeight)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= gridWidth || pos.y >= gridHeight)
                return true;

            for (int i = 0; i < Body.Count; i++)
            {
                if (Body[i] == pos)
                    return true;
            }

            return false;
        }

        public bool IsOnPosition((int x, int y) pos)
        {
            return Body[0] == pos;
        }

        public void Draw(int gridSize, int offsetX, int offsetY)
        {
            foreach (var part in Body)
                Raylib.DrawRectangle(offsetX + part.x * gridSize, offsetY + part.y * gridSize, gridSize, gridSize, Color.Green);
        }
    }
}

//        public List<(int x, int y)> Body { get; private set; } = new List<(int x, int y)>();
//        public (int x, int y) Direction { get; private set; } = (1, 0); // startar åt höger

//        public Snake(int startX, int startY)
//        {
//            Body.Add((startX, startY));
//        }

//        public void SetDirection((int x, int y) newDirection)
//        {
//            // Förhindra 180-graders vändning
//            if (Body.Count > 1 && newDirection.x == -Direction.x && newDirection.y == -Direction.y)
//                return;
//            Direction = newDirection;
//        }

//        public void Move(bool grow = false)
//        {
//            var head = Body[0];
//            var newHead = (head.x + Direction.x, head.y + Direction.y);
//            Body.Insert(0, newHead);

//            if (!grow)
//                Body.RemoveAt(Body.Count - 1); // ta bort svansen
//        }

//        // Kolla kollision med nästa position
//        public bool CheckCollisionAt((int x, int y) pos, int gridWidth, int gridHeight)
//        {
//            // Väggar
//            if (pos.x < 0 || pos.y < 0 || pos.x >= gridWidth || pos.y >= gridHeight)
//                return true;

//            // Sig själv
//            for (int i = 0; i < Body.Count; i++)
//            {
//                if (Body[i] == pos)
//                    return true;
//            }

//            return false;
//        }

//        public bool IsOnPosition((int x, int y) pos)
//        {
//            return Body[0] == pos;
//        }

//        public void Draw(int gridSize)
//        {
//            foreach (var part in Body)
//                Raylib.DrawRectangle(part.x * gridSize, part.y * gridSize, gridSize, gridSize, Color.Green);
//        }
//    }
//}
//}
//        public List<(int x, int y)> Body { get; private set; } = new List<(int x, int y)>();
//        private (int x, int y) direction = (1, 0); // startar åt höger

//        public Snake(int startX, int startY)
//        {
//            Body.Add((startX, startY));
//        }

//        public void SetDirection((int x, int y) newDirection)
//        {
//            // Förhindra att ormen vänder 180 grader
//            if (Body.Count > 1 && newDirection.x == -direction.x && newDirection.y == -direction.y)
//                return;
//            direction = newDirection;
//        }

//        public void Move(bool grow = false)
//        {
//            var head = Body[0];
//            var newHead = (head.x + direction.x, head.y + direction.y);
//            Body.Insert(0, newHead);

//            if (!grow)
//                Body.RemoveAt(Body.Count - 1);
//        }

//        public bool CheckCollision(int gridWidth, int gridHeight)
//        {
//            var head = Body[0];

//            // Kollision med väggar
//            if (head.x < 0 || head.y < 0 || head.x >= gridWidth || head.y >= gridHeight)
//                return true;

//            // Kollision med sig själv
//            for (int i = 1; i < Body.Count; i++)
//            {
//                if (Body[i] == head)
//                    return true;
//            }

//            return false;
//        }

//        public bool IsOnPosition((int x, int y) pos)
//        {
//            return Body[0] == pos;
//        }

//        public void Draw(int gridSize)
//        {
//            foreach (var part in Body)
//            {
//                Raylib.DrawRectangle(part.x * gridSize, part.y * gridSize, gridSize, gridSize, Color.Green);
//            }
//        }
//    }
//}
