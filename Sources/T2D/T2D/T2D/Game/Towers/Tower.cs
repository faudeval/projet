using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace T2D
{
    class Tower : Sprite
    {
        protected int cost;
        protected int damage;

        protected float radius;

        protected Enemy target;
        public Enemy Target
        {
            get { return this.target; }
        }

        public int Cost
        {
            get { return this.cost; }
        }
        public int Damage
        {
            get { return damage; }
        }
        public float Radius
        {
            get { return this.radius; }
        }

        public Tower(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            this.radius = 1000;
        }

        public bool IsInRange(Vector2 position)
        {
            if (Vector2.Distance(this.center, this.position) <= this.radius)
                return true;

            return false;
        }

        public void GetClosestEnemy(List<Enemy> enemies)
        {
            this.target = null;
            float smallestRange = radius;

            foreach (Enemy enemy in enemies)
            {
                if (Vector2.Distance(this.center, enemy.Center) < smallestRange)
                {
                    smallestRange = Vector2.Distance(this.center, enemy.Center);
                    this.target = enemy;
                }
            }
        }

        protected void FaceTarget()
        {
            Vector2 direction = this.center - this.target.Center;
            direction.Normalize();
            rotation = (float)Math.Atan2(-direction.X, direction.Y);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.target != null)
                FaceTarget();
        }
    }
}
