using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MatrixBackground
{
    class Engine
    {
        public static class Settings
        {
            // time before spawning another character
            private const int MAX_SPAWN_TIME_CHAR = 75;
            private const int MIN_SPAWN_TIME_CHAR = 75;

            // time before spawning another column
            private const int MAX_SPAWN_TIME_COL = 100;
            private const int MIN_SPAWN_TIME_COL = 50;

            // time before character changes
            private const int MAX_DISPLAY_TIME = 5000;
            private const int MIN_DISPLAY_TIME = 500;

            public const double LIFE_TIME_CHAR = 3.0;       // fade-time
            public const double INIT_TIME_CHAR = 0.125;     // white-time

            // set this to RandomCharOnly or RandomZeroOne
            // decides which type of characters will be displayed
            public static char RandomChar { get { return RandomCharOnly; } }

            public static double RandomCharTime { get { return Random.Next(MIN_SPAWN_TIME_CHAR, MAX_SPAWN_TIME_CHAR) / 1000.0; } }
            public static double RandomColTime { get { return Random.Next(MIN_SPAWN_TIME_COL, MAX_SPAWN_TIME_COL) / 1000.0; } }
            public static double RandomDisplayTime { get { return Random.Next(MIN_DISPLAY_TIME, MAX_DISPLAY_TIME) / 1000.0; } }

            private static char RandomCharOnly { get { return (char)Random.Next(32, 126); } }
            private static char RandomZeroOne { get { return Random.Next(0, 2).ToString()[0]; } }
            public static float RandomColPosition { get { return Random.Next(0, Width); } }

            public static SpriteFont Font { get; private set; }
            public static Random Random { get; private set; }
            public static int Width { get; private set; }
            public static int Height { get; private set; }

            public static void Init(int _Width, int _Height, SpriteFont _Font)
            {
                Width = _Width;
                Height = _Height;
                Font = _Font;
                Random = new Random();
            }
        }

        private double mColTime;
        private List<Column> columns;

        public Engine(int _Width, int _Height, SpriteFont _Font)
        {
            Settings.Init(_Width, _Height, _Font);
            columns = new List<Column>();
        }

        public void Update(double _Delta)
        {
            // if it's time to spawn a new column
            mColTime -= _Delta;
            if (mColTime <= 0.0)
            {
                // add column and reset timer
                columns.Add(new Column());
                mColTime = Settings.RandomColTime;
            }

            // update each column
            for (int i = 0; i < columns.Count; ++i)
            {
                columns[i].Update(_Delta);

                // remove if column has reached bottom and faded out
                if (columns[i].Complete)
                    columns.RemoveAt(i);
            }
        }

        public void Draw(SpriteBatch _SB)
        {
            foreach (Column c in columns)
                c.Draw(_SB);
        }
    }
}
