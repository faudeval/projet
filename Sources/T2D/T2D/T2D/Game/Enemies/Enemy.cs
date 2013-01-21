using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace T2D
{
    class Enemy : Sprite
    {
        private Queue<Vector2> waypoints = new Queue<Vector2>();

        protected float startHealth;
        protected float currentHealth;

        protected bool alive = true;

        protected float speed = 0.5f;
        protected int bountyGiven;

        public float CurrentHealth
        {
            get { return this.currentHealth; }
            set { this.currentHealth = value; }
        }

        public bool IsDead
        {
            get { return this.currentHealth <= 0; }
        }

        public int BountyGiven
        {
            get { return this.bountyGiven; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public Enemy(Texture2D texture, Vector2 position, float health, int bountyGiven, float speed)
            : base(texture, position)
        {
            this.startHealth = health;
            this.currentHealth = startHealth;
            this.bountyGiven = bountyGiven;
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.waypoints.Count > 0)
            {
                if (DistanceToDestination < speed)
                {
                    this.position = this.waypoints.Peek();
                    this.waypoints.Dequeue();
                }
                else
                {
                    Vector2 direction = waypoints.Peek() - this.position;
                    direction.Normalize();
                    this.velocity = Vector2.Multiply(direction, this.speed);
                    this.position += velocity;
                }
            }
            else
                this.alive = false;

            if (currentHealth <= 0)
                this.alive = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.alive)
            {
                float healthPercentage = (float)this.currentHealth / (float)this.startHealth;
                Color color = new Color(new Vector3(1 - healthPercentage, 1 - healthPercentage, 1 - healthPercentage));         
                base.Draw(spriteBatch);
            }

        }

        public void SetWaypoints(Queue<Vector2> waypoints)
        {
            foreach (Vector2 waypoint in waypoints)
                this.waypoints.Enqueue(waypoint);

            this.position = this.waypoints.Dequeue();
        }
    }
}
