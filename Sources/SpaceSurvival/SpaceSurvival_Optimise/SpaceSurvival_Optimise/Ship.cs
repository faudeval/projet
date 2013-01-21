#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace SpaceSurvival_Optimise
{
    enum Controls { Up, Down, Left, Right, Fire }
    class Ship:Obj
    {
        #region Fields
        Vector2[] hitBox;
        private int rotation = 0;
        public int points = 0;
        public String Pseudo;
        private Keys[] controls;

        public int Degrees
        {
            get { return rotation; }
            set
            {
                Direction = new Vector2((float)Math.Sin((value * Math.PI) / 180), -(float)Math.Cos((value * Math.PI) / 180));
                HitBox = rotate(hitBox, value);
                rotation = value;
            }
        }
        public float Radian
        {
            get { return (float)((rotation * Math.PI) / 180); }
        }

        public override Rectangle bounds
        {
            get { return new Rectangle((int)Position.X - (Image.Width), (int)Position.Y - (Image.Height), Image.Width * 2, Image.Height * 2); }
        }
        #endregion

        #region Initialize
        public Ship(int team, String pseudo, int skin, Keys[] ctrl, Vector2 Position):base(team, pseudo, skin, ctrl, Position)
        { }

        protected override void init(params object[] param)
        {
            Team = (int)param[0];
            Pseudo = (String)param[1];
            generateImage((int)param[2]);
            controls = (Keys[])param[3];
            Position = (Vector2)param[4];
            Direction = new Vector2(0, -1); // Vertical
            Speed = 0;
        }
        #endregion

        #region Update & Draw
        public override void Update(GameTime gameTime)
        {
            if (IsActive && IsReady)
            {
                Position += Direction * Speed;
                Speed -= Speed / 30;    /* "Inertie" */

                if (SpaceSurvival.keyboardState.IsKeyDown(controls[(int)Controls.Up]) && Speed < 10)     /* Augmente le Speed */
                    Speed += 0.5F;
                if (SpaceSurvival.keyboardState.IsKeyDown(controls[(int)Controls.Down]) && Speed > -10)   /* Diminue le Speed */
                    Speed -= 0.5F;
                if (SpaceSurvival.keyboardState.IsKeyDown(controls[(int)Controls.Left]))   /* Modifie la Direction */
                    Degrees -= 6;
                if (SpaceSurvival.keyboardState.IsKeyDown(controls[(int)Controls.Right]))  /* Modifie la Direction */
                    Degrees += 6;

                if (SpaceSurvival.keyboardState.IsKeyDown(controls[(int)Controls.Fire]) && SpaceSurvival.oldKeyboardState.IsKeyUp(controls[(int)Controls.Fire])) // On fait un tir à chaque nouvel appui sur la barre espace
                    SpaceSurvival.waitingComponents.Add(new Bullet(HitBox[0]+Position, Direction, this));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsActive && IsReady)
            {
                SpaceSurvival.spriteBatch.Draw(
                     Image,                                             /* Texture2D texture */
                     Position,                                          /* Vector2 position */
                     null,                                              /* Nullable<Rectangle> sourceRectangle */
                     Color.White,                                       /* Color color */
                     Radian,                                            /* float rotation */
                     new Vector2(Image.Width / 2, Image.Height / 2),    /* Vector2 origin */
                     1f,                                                /* Vector2 OR Single scale */
                     SpriteEffects.None,                                /* SpriteEffects effects */
                     0f                                                 /* float layerDepth */
                );
                SpaceSurvival.spriteBatch.DrawString(
                    SpaceSurvival.font,                                 /* SpriteFont spriteFont */
                    Pseudo,                                             /* string text */
                    Position - new Vector2(SpaceSurvival.font.MeasureString(Pseudo).X * 0.3f / 2, - (Image.Height / 2 + SpaceSurvival.font.MeasureString(Pseudo).Y * 0.3f)), /* Vector2 position */
                    SpaceSurvival.teamColor[Team],                                        /* Color color */
                    0f,                                                 /* float rotation */
                    Vector2.Zero,                                       /* Vector2 origin */
                    0.3f,                                               /* float scale */
                    SpriteEffects.None,                                 /* SpriteEffects effects */
                    0f                                                  /* float layerDepth */
                );
            }
        }
        #endregion

        #region Methods
        private void generateImage(int skin)
        {
            int size = 30;
            Image = new Texture2D(SpaceSurvival.graphics.GraphicsDevice, size, size, false, SurfaceFormat.Color);
            Color[] colorTable = new Color[size * size];
            for (int i = 0; i < size * size; i++)
                colorTable[i] = Color.Transparent;

            hitBox = new Vector2[size * 2];

            for (int i = 0; i < size / 2; i++)
            {
                colorTable[size / 2 - i + size * (i * 2)] = SpaceSurvival.teamColor[Team];
                hitBox[4 * i] = new Vector2((-i), (i * 2) - size / 2);
                colorTable[size / 2 - i + size * (2 * i + 1)] = SpaceSurvival.teamColor[Team];
                hitBox[4 * i + 1] = new Vector2((-i), (2 * i + 1) - size / 2);
                colorTable[size / 2 + i + size * (i * 2)] = SpaceSurvival.teamColor[Team];
                hitBox[4 * i + 2] = new Vector2((i), (2 * i) - size / 2);
                colorTable[size / 2 + i + size * (2 * i + 1)] = SpaceSurvival.teamColor[Team];
                hitBox[4 * i + 3] = new Vector2((i), (2 * i + 1) - size / 2);
            }
            Image.SetData<Color>(colorTable);
            HitBox = hitBox;
        }

        private Vector2[] rotate(Vector2[] tableau, int rot)
        {
            int x, y;
            float cx = 0;
            float cy = 0;
            float cosa = (float)Math.Cos((rot * Math.PI / 180));
            float sina = (float)Math.Sin((rot * Math.PI / 180));
            Vector2[] ret = new Vector2[tableau.Length];

            for (int i = 0; i < tableau.Length; i++)
            {
                x = (int)(((-cx + tableau[i].X) * cosa - (-cy + tableau[i].Y) * sina) + cx);
                y = (int)(((-cx + tableau[i].X) * sina + (-cy + tableau[i].Y) * cosa) + cy);
                ret[i] = new Vector2(x, y);
            }
            return ret;
        }
        #endregion
    }
}
