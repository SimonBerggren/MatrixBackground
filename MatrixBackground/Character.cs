using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MatrixBackground
{
    class Character
    {
        private char mCharacter;
        private Vector2 mPosition;
        private double lifeTime;
        private double maxLife;
        private double startTime;
        private double maxStart;
        private double displayTime;

        public bool IsDead { get { return lifeTime <= 0.0; } }

        public Character(Vector2 _Position)
        {
            mPosition = _Position;
            lifeTime = maxLife = 3.0;
            startTime = maxStart = 0.125;
            mCharacter = Engine.Settings.RandomZeroOne;
        }

        public void Update(double _Delta)
        {
            startTime -= _Delta;
            lifeTime -= _Delta;
            displayTime -= _Delta;

            if (displayTime <= 0.0)
            {
                mCharacter = Engine.Settings.RandomChar;
                displayTime = Engine.Settings.RandomDisplayTime;
            }
        }

        public void Draw(SpriteBatch _SB)
        {
            Color c1 = new Color(0.0f, 1.0f, 0.0f, 1.0f) * (float)(lifeTime / maxLife);
            Color c2 = Color.White * (float)(startTime / maxStart);
            Color c3 = new Color();
            c3.R = (byte)(c1.R + c2.R);
            c3.G = (byte)(c1.G + c2.G);
            c3.B = (byte)(c1.B + c2.B);
            c3.A = (byte)(c1.A + c2.A);
            _SB.DrawString(Engine.Settings.Font, mCharacter.ToString(), mPosition, c3);
        }
    }
}
