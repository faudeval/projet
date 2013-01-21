#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace SpaceSurvival_Optimise
{
    class Bullet:Obj
    {
        #region Fields
        public Ship Owner { get; private set; }
        #endregion

        #region Initialize
        public Bullet(Vector2 position, Vector2 direction, Ship ship)
            : base(new Object[] {position, direction, ship})
        { }

        protected override void init(params Object[] param)
        {
            Position = (Vector2)param[0];
            Direction = (Vector2)param[1];
            Owner = (Ship)param[2];
            Speed = 10;
            Team = Owner.Team;
            Image = generateImage();
        }
        #endregion

        #region Methods
        private Texture2D generateImage()
        {
            Texture2D texture = new Texture2D(SpaceSurvival.graphics.GraphicsDevice, 3, 3, false, SurfaceFormat.Color);
            Color color = SpaceSurvival.teamColor[Team];

            Color[] colorTable = new Color[3*3];
            for (int i = 0; i < 3*3; i++)
            {
                colorTable[i] = color;
            }
            texture.SetData<Color>(colorTable);

            HitBox = new Vector2[3*3];
            for (int i = 0; i < 3*3; i++)
            {
                HitBox[i] = new Vector2((int)(i / 3), (int)(i % 3));
            }
            return texture;
        }

        public override void Hit()
        {
            base.Hit();
            Owner.points++;
        }
        #endregion
    }
}
