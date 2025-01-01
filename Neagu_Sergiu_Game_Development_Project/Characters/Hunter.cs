using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using System;
using System.Collections.Generic;
using Neagu_Sergiu_Game_Development_Project.Animations;
using Neagu_Sergiu_Game_Development_Project.HealthClasses;

namespace Neagu_Sergiu_Game_Development_Project.Characters 
{
    public abstract class Hunter
    {
        protected Dictionary<string, Animation> _animations;
        protected Animation _currentAnimation;
        public Vector2 Position { get;  set; }
        protected bool _isFacingRight;
        protected float _speed;
        protected int UniformWidth { get; set; } = 54; 
        protected int UniformHeight { get; set; } = 54;

        public Health Health { get; set; }

        //For hunter to attack slower
        private float _attackCooldown = 1.5f; 
        private float _timeSinceLastAttack = 0f; 

        //Hurt
        private bool _isHurt; 
        private float _hurtTimer; 
        private const float HurtDuration = 0.5f;
        public bool IsDead { get; set; } = false;

        public Rectangle CurrentHitbox
        {
            get
            {
                var frameHitbox = _currentAnimation.GetCurrentHitbox();
                 return new Rectangle(
                     (int)(Position.X + frameHitbox.X),
                     (int)(Position.Y + frameHitbox.Y),
                     frameHitbox.Width,
                     frameHitbox.Height
                 );
            }
        }

        public Hunter(Vector2 initialPosition, float speed, int maxHealth)
        {
            Position = initialPosition;
            _isFacingRight = true;
            _speed = speed;
            Health = new Health(maxHealth);
        }

        public virtual void LoadContent(ContentManager content, string texturePath)
        {
            var spriteFactory = new SpriteFactory(content);
            _animations = spriteFactory.LoadAnimationsForHunter(texturePath);
            _currentAnimation = _animations["idleRight"];
        }

        public virtual void TakeDamage(int damage)
        {
            if (_isHurt || IsDead) return;

            Health.TakeDamage(damage);
            _isHurt = true;
            _hurtTimer = 0;
            _currentAnimation = _isFacingRight ? _animations["hurtRight"] : _animations["hurtLeft"];

            if (Health.IsDead)
            {
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
                IsDead = true;
            }
        }

        public void FaceVampire(Vector2 vampirePosition)
        {
            _isFacingRight = vampirePosition.X > this.Position.X;
            _currentAnimation = _isFacingRight ? _animations["attackRight"] : _animations["attackLeft"];
        }

        public virtual void Update(GameTime gameTime, Vampire vampire)
        {
            _currentAnimation.Update(gameTime);

            // Handle hurt flicker
            if (_isHurt)
            {
                _hurtTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_hurtTimer >= HurtDuration)
                    _isHurt = false; 
            }

            // Update attack cooldown timer
            _timeSinceLastAttack += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Placeholder AI: Hunters alternate between idle and moving randomly
            Random random = new Random();
            if (random.Next(100) < 5) // 5% chance per frame to move
            {
                Vector2 randomDirection = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
                MoveToward(Position + randomDirection * 6); // Move a small random distance
            }
            else
            {
                ChangeState("Idle");
            }

            float distance = Vector2.Distance(vampire.Position, this.Position);

            if (distance < 47)
            {
                FaceVampire(vampire.Position);

                if (CheckCollision(vampire.CurrentHitbox) && _timeSinceLastAttack >= _attackCooldown)
                {
                    vampire.TakeDamage(1);
                    _timeSinceLastAttack = 0f;
                }
            }
            else
            {
                ChangeState("Walk");
            }
        }

        public void ChangeState(string state)
        {
            if (state == "Idle")
            {
                _currentAnimation = _isFacingRight ? _animations["idleRight"] : _animations["idleLeft"];
            }
            else if (state == "Attack")
            {
                _currentAnimation = _isFacingRight ? _animations["attackRight"] : _animations["attackLeft"];
            }
            else if (state == "Run")
            {
                _currentAnimation = _isFacingRight ? _animations["runRight"] : _animations["runLeft"];
            }
            else if (state == "Die")
            {
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
            }
            else if (state == "Hurt")
            {
                _currentAnimation = _isFacingRight ? _animations["hurtRight"] : _animations["hurtLeft"];
            }
        }

        public virtual void MoveToward(Vector2 targetPosition)
        {
            if (Position.X < targetPosition.X)
            {
                Position = new Vector2(Position.X + _speed, Position.Y);
                _isFacingRight = true;
            }
            else if (Position.X > targetPosition.X)
            {
                Position = new Vector2(Position.X - _speed, Position.Y);
                _isFacingRight = false;
            }

            if (Position.Y < targetPosition.Y)
            {
                Position = new Vector2(Position.X, Position.Y + _speed);
            }
            else if (Position.Y > targetPosition.Y)
            {
                Position = new Vector2(Position.X, Position.Y - _speed);
            }
        }

        public bool CheckCollision(Rectangle other)
        {
            return CurrentHitbox.Intersects(other);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!_isHurt || ((int)(_hurtTimer * 10) % 2 == 0)) 
            {
                var destRectangle = new Rectangle((int)Position.X, (int)Position.Y, UniformWidth, UniformHeight);
                spriteBatch.Draw(_currentAnimation.Texture, destRectangle, _currentAnimation.GetSourceRectangle(), Color.White);
            }
        }
    }
}