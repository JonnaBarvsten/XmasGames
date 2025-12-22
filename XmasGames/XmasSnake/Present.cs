using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace XmasGames.XmasSnake
{
    internal class Present
    {
        public (int x, int y) Position { get; private set; }
        private int gridWidth;
        private int gridHeight;
        private Random rand;

        public Present(int gridWidth, int gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            rand = new Random();
            Position = (-1, -1);
        }

        public void Spawn(List<(int x, int y)> snakeBody)
        {
            do
            {
                Position = (rand.Next(gridWidth), rand.Next(gridHeight));
            } while (snakeBody.Contains(Position));
        }

        public void Draw(int gridSize, int offsetX, int offsetY)
        {
            Raylib.DrawRectangle(offsetX + Position.x * gridSize, offsetY + Position.y * gridSize, gridSize, gridSize, Color.Red);
            Raylib.DrawRectangle(offsetX + Position.x * gridSize + gridSize / 4, offsetY + Position.y * gridSize, gridSize / 4, gridSize, Color.Green);
            Raylib.DrawRectangle(offsetX + Position.x * gridSize, offsetY + Position.y * gridSize + gridSize / 4, gridSize, gridSize / 4, Color.Green);
        }



        //public (int x, int y) Position { get; private set; }
        //private int gridWidth;
        //private int gridHeight;
        //private Random rand;

        //public Present(int gridWidth, int gridHeight)
        //{
        //    this.gridWidth = gridWidth;
        //    this.gridHeight = gridHeight;
        //    rand = new Random();
        //    Position = (-1, -1); // init
        //}

        //public void Spawn(List<(int x, int y)> snakeBody)
        //{
        //    do
        //    {
        //        Position = (rand.Next(gridWidth), rand.Next(gridHeight));
        //    } while (snakeBody.Contains(Position)); // spawnar ej på ormen
        //}

        //public void Draw(int gridSize)
        //{
        //    Raylib.DrawRectangle(Position.x * gridSize, Position.y * gridSize, gridSize, gridSize, Color.Red);
        //    // Grönt band
        //    Raylib.DrawRectangle(Position.x * gridSize + gridSize / 4, Position.y * gridSize, gridSize / 4, gridSize, Color.Green);
        //    Raylib.DrawRectangle(Position.x * gridSize, Position.y * gridSize + gridSize / 4, gridSize, gridSize / 4, Color.Green);
        //}

        //public (int x, int y) Position { get; private set; }
        //private int gridWidth;
        //private int gridHeight;
        //private Random rand;

        //public Present(int gridWidth, int gridHeight)
        //{
        //    this.gridWidth = gridWidth;
        //    this.gridHeight = gridHeight;
        //    rand = new Random();
        //    Spawn();
        //}

        //public void Spawn()
        //{
        //    Position = (rand.Next(gridWidth), rand.Next(gridHeight));
        //}

        //public void Draw(int gridSize)
        //{
        //    // Rött paket
        //    Raylib.DrawRectangle(Position.x * gridSize, Position.y * gridSize, gridSize, gridSize, Color.Red);

        //    // Grönt band (vertikalt och horisontellt)
        //    Raylib.DrawRectangle(Position.x * gridSize + gridSize / 4, Position.y * gridSize, gridSize / 4, gridSize, Color.Green);
        //    Raylib.DrawRectangle(Position.x * gridSize, Position.y * gridSize + gridSize / 4, gridSize, gridSize / 4, Color.Green);
        //}
    }
}

       