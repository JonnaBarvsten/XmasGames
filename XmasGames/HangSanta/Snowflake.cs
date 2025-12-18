using System;
using System.Collections.Generic;
using System.Text;

namespace XmasGames.HangSanta
{
    internal class Snowflake
    {
        public float X, Y;
        public float Speed;
        public Snowflake(float x, float y, float speed)
        {
            X = x;
            Y = y;
            Speed = speed;
        }
    }
}
