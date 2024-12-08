using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Neagu_Sergiu_Game_Development_Project.Animations;
using System.Collections.Generic;

public class Vampire
{
    private Dictionary<string, Animation> _animations;
    private Animation _currentAnimation;
    public Vector2 Position;
    public Vector2 PreviousPosition; // Added for collision handling
    private bool _isFacingRight;
    private float _speed = 2.7f;
    private int uniformWidth = 64;  // Uniform width of the sprite
    private int uniformHeight = 64; // Uniform height of the sprite

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

    public Vampire(Vector2 initialPosition)
    {
        Position = initialPosition;
        PreviousPosition = initialPosition; 
        _isFacingRight = true;
    }

    public void LoadContent(ContentManager content)
    {
        var spriteFactory = new SpriteFactory(content);
        _animations = spriteFactory.LoadAnimations();
        _currentAnimation = _animations["idleRight"];
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState, MouseState mouseState, bool isAttacking, bool isJumping, bool isRunning)
    {
        _currentAnimation.Update(gameTime);

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
    }

    private void Move(KeyboardState keyboardState)
    {
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
        var destRectangle = new Rectangle(
            (int)Position.X, (int)Position.Y,
            uniformWidth, uniformHeight
        );

        spriteBatch.Draw(_currentAnimation.Texture, destRectangle, _currentAnimation.GetSourceRectangle(), Color.White);
    }

    public bool CheckCollision(Rectangle other)
    {
        return CurrentHitbox.Intersects(other);
    }
}