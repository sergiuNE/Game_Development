using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neagu_Sergiu_Game_Development_Project.Animations;
using System.Collections.Generic;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using Neagu_Sergiu_Game_Development_Project.HealthClasses;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class Vampire
    {
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;
        public Vector2 Position;
        public Vector2 PreviousPosition; // Added for collision handling        
        private bool _isFacingRight;
        private float _speed = 2.7f;
        private int uniformWidth = 64;
        private int uniformHeight = 64;
        private bool canMove = true;

        public bool _isHurt { get; set; } = false;
        private float _hurtTimer; // Timer for hurt flicker
        private const float HurtDuration = 0.5f;

        private float _deathTimer = 0f;
        private const float DeathAnimationDuration = 2f;
        public bool IsDead { get; set; }

        public Game1 game1;

        public Health Health { get; private set; }
        public Texture2D DeathTexture { get; set; }

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

        public Vampire(Vector2 initialPosition, int maxHealth)
        {
            Position = initialPosition;
            PreviousPosition = initialPosition;
            _isFacingRight = true;
            Health = new Health(maxHealth);
        }

        public void LoadContent(ContentManager content)
        {
            var spriteFactory = new SpriteFactory(content);
            _animations = spriteFactory.LoadAnimations();
            _currentAnimation = _animations["idleRight"];
            DeathTexture = content.Load<Texture2D>("vampire_die_left");
        }

        public void TakeDamage(int damage)
        {
            if (_isHurt || IsDead) return;

            Health.TakeDamage(damage);
            _isHurt = true;
            _hurtTimer = 0;
            _currentAnimation = _isFacingRight ? _animations["hurtRight"] : _animations["hurtLeft"];

            // Check if the vampire is dead
            if (Health.IsDead)
            {
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
                //TriggerGameOver();
                IsDead = true;
            }
        }

        private void TriggerGameOver()
        {
            Game1.gameOverMenu.ShowGameOverMenu();
        }

        public void AttackHunter(Hunter hunter)
        {
            if (hunter.IsDead) return;

            hunter.TakeDamage(1);
            _currentAnimation = _isFacingRight ? _animations["attackRight"] : _animations["attackLeft"];
            if (hunter.Health.IsDead)
            {
                Health.AddHeart(1);
                Game1.Hunters.Remove(hunter);
            }
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, bool isAttacking, bool isJumping, bool isRunning)
        {
            _currentAnimation.Update(gameTime);

            // Handle hurt flicker
            if (_isHurt)
            {
                _hurtTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_hurtTimer >= HurtDuration)
                    _isHurt = false;
            }

            // Game logic for when vampire dies
            if (Health.IsDead)
            {
                if (_deathTimer == 0)
                {
                    // Start de doodsanimatie
                    ChangeState("Die");
                }

                _deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_deathTimer >= DeathAnimationDuration)
                {
                    TriggerGameOver(); 
                }

                return;
            }

            // Attack logic
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                isAttacking = true;
                foreach (var hunter in Game1.Hunters)
                {
                    if (CheckCollision(hunter.CurrentHitbox))
                    {
                        AttackHunter(hunter);
                        if (hunter.Health.IsDead)
                        {
                            game1.AddHeart(1); // Add a heart if hunter dies
                        }
                    }
                }
            }

            PreviousPosition = Position;

            if (isAttacking)
                ChangeState("Attack");
            else if (isJumping)
                ChangeState("Jump");
            else if (isRunning)
                ChangeState("Run");
            else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Right))
                ChangeState("Walk");
            else if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down))
                ChangeState("Idle");
            else
                ChangeState("Idle");

            if (keyboardState.IsKeyDown(Keys.Left)) _isFacingRight = false;
            if (keyboardState.IsKeyDown(Keys.Right)) _isFacingRight = true;

            Move(keyboardState);  
        }

        private void Move(KeyboardState keyboardState)
        {
            if (!canMove)
                return;

            if (keyboardState.IsKeyDown(Keys.Left))
                Position = new Vector2(Position.X - _speed, Position.Y);
            if (keyboardState.IsKeyDown(Keys.Right))
                Position = new Vector2(Position.X + _speed, Position.Y);
            if (keyboardState.IsKeyDown(Keys.Up))
                Position = new Vector2(Position.X, Position.Y - _speed);
            if (keyboardState.IsKeyDown(Keys.Down))
                Position = new Vector2(Position.X, Position.Y + _speed);
        }

        public bool IsMovingVertically(KeyboardState keyboardState)
        {
            return keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down);
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
            else if (state == "Jump")
            {
                _currentAnimation = _isFacingRight ? _animations["jumpRight"] : _animations["jumpLeft"];
            }
            else if (state == "Run")
            {
                _currentAnimation = _isFacingRight ? _animations["runRight"] : _animations["runLeft"];
            }
            else if (state == "Walk")
            {
                _currentAnimation = _isFacingRight ? _animations["walkRight"] : _animations["walkLeft"];
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

        public void DrawDeathSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(DeathTexture, Position, Color.White);
        }


        public bool CheckCollision(Rectangle other)
        {
            return CurrentHitbox.Intersects(other);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isHurt || ((int)(_hurtTimer * 10) % 2 == 0)) // Flicker effect
            {
                var destRectangle = new Rectangle((int)Position.X, (int)Position.Y, uniformWidth, uniformHeight);
                spriteBatch.Draw(_currentAnimation.Texture, destRectangle, _currentAnimation.GetSourceRectangle(), Color.White);
            }
        }
    }
}