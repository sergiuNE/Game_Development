    using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neagu_Sergiu_Game_Development_Project.Interfaces;
using Neagu_Sergiu_Game_Development_Project.Behaviors;
using Neagu_Sergiu_Game_Development_Project.Animations;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public abstract class Hunter
    {
        protected Dictionary<string, Animation> _animations;
        protected Animation _currentAnimation;
        public Vector2 Position { get; protected set; }
        protected bool _isFacingRight;
        protected float _speed;
        protected int UniformWidth { get; set; } = 55; 
        protected int UniformHeight { get; set; } = 55; 

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

        public Hunter(Vector2 initialPosition, float speed)
        {
            Position = initialPosition;
            _isFacingRight = true;
            _speed = speed;
        }

        public virtual void LoadContent(ContentManager content, string texturePath)
        {
            var spriteFactory = new SpriteFactory(content);
            _animations = spriteFactory.LoadAnimationsForHunter(texturePath);
            _currentAnimation = _animations["idleRight"];
        }

        public void Update(GameTime gameTime)
        {
            _currentAnimation.Update(gameTime);

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

        public void Draw(SpriteBatch spriteBatch)
        {
            var destRectangle = new Rectangle(
                (int)Position.X, (int)Position.Y,
                UniformWidth, UniformHeight
            );

            spriteBatch.Draw(_currentAnimation.Texture, destRectangle, _currentAnimation.GetSourceRectangle(), Color.White);
        }

        public bool CheckCollision(Rectangle other)
        {
            return CurrentHitbox.Intersects(other);
        }
    }
}