using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neagu_Sergiu_Game_Development_Project.Animations;
using System.Collections.Generic;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using Neagu_Sergiu_Game_Development_Project.HealthClasses;
using Neagu_Sergiu_Game_Development_Project.Characters;
using Neagu_Sergiu_Game_Development_Project;

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
    private bool _isHurt; 
    private float _hurtTimer; // Timer for hurt flicker
    private const float HurtDuration = 0.5f; // Duration for flicker in seconds

    public Health Health { get; private set; }

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
    }

    public void TakeDamage(int damage)
    {
        if (_isHurt) return; // Prevent stacking damage during flicker

        Health.TakeDamage(damage);
        _isHurt = true;
        _hurtTimer = 0;

        _currentAnimation = _isFacingRight ? _animations["hurtRight"] : _animations["hurtLeft"];

        // Check if the vampire is dead
        if (Health.IsDead)
        {
            _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        // Logic to show GameOverMenu
    }

    public void AttackHunter(Hunter hunter)
    {
        hunter.TakeDamage(1);
        _currentAnimation = _isFacingRight ? _animations["attackRight"] : _animations["attackLeft"];
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, bool isAttacking, bool isJumping, bool isRunning)
    {
        _currentAnimation.Update(gameTime);

        // Handle hurt flicker
        if (_isHurt)
        {
            _hurtTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_hurtTimer >= HurtDuration)
                _isHurt = false; // Reset flicker state
        }

        // Store the previous position for collision handling
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
                        Game1.AddHeart(); // Add a heart if hunter dies
                    }
                }
            }
        }

        // Game logic for when vampire dies
        if (Health.IsDead)
        {
            TriggerGameOver();
        }
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
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!_isHurt || ((int)(_hurtTimer * 10) % 2 == 0)) // Flicker effect
        {
            var destRectangle = new Rectangle((int)Position.X, (int)Position.Y, uniformWidth, uniformHeight);
            spriteBatch.Draw(_currentAnimation.Texture, destRectangle, _currentAnimation.GetSourceRectangle(), Color.White);
        }
    }

    public bool CheckCollision(Rectangle other)
    {
        return CurrentHitbox.Intersects(other);
    }
}