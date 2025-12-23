using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace XmasGames.XmasSnake
{
    internal class Snake
    {
        public List<(int x, int y)> Body { get; private set; } = new List<(int x, int y)>();
        public (int x, int y) Direction { get; private set; } = (1, 0); 

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

        public void ResetPosition(int startX, int startY)
        {
            Body.Clear();                
            Body.Add((startX, startY));  
            Direction = (0, 0);         
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