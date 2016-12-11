using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MatrixBackground
{
    class Column
    {
        private double mCharTime;   // timer until adding another character to column
        private List<Character> mCharacters; // all characters in this column
        private float mX;   // x-position of column

        // helper properties
        private float mY { get { return Engine.Settings.Font.LineSpacing * mCharacters.Count; } }  // current y-position
        private bool filled { get { return mY >= Engine.Settings.Height; } }   // if number of characters is filling the height of the screen
        private bool done { get { return filled && mCharacters[mCharacters.Count - 1].IsDead; } }   // if the last character has faded out completely
        public bool Complete { get { return filled && done; } } // if filled the screen as well as faded out completely

        public Column()
        {
            mX = Engine.Settings.RandomColPosition;
            mCharacters = new List<Character>();
        }

        public void Update(double _Delta)
        {
            mCharTime -= _Delta;

            // if it's time to add another character and we aren't at the bottom
            if (mCharTime <= 0.0 && !filled)
            {
                // add a random character and reset the timer
                mCharacters.Add(new Character(new Vector2(mX, mY)));
                mCharTime = Engine.Settings.RandomCharTime;
            }

            // animate each of the characters fading
            foreach (Character c in mCharacters)
                c.Update(_Delta);
        }

        public void Draw(SpriteBatch _SB)
        {
            // draw each of the characters
            foreach (Character c in mCharacters)
                c.Draw(_SB);
        }
    }
}
