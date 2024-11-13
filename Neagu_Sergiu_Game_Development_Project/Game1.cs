using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Neagu_Sergiu_Game_Development_Project.Characters;
using System;
using System.Reflection.Emit;

namespace Neagu_Sergiu_Game_Development_Project
{
    public enum GameState
    {
        StartScreen,
        Transition,
        Playing,
        GameOver,
        Victory
    }

    public enum Level 
    {
        Level1,
        Level2,
        Level3
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D background;
        private SpriteFont _font;
        private SpriteFont _fontNameGame;
        private Rectangle _backgroundRectangle;

        private Vector2 gameName;
        private Vector2 startPosition;
        private Vector2 exitPosition;
        private MouseState _previousMouseState;
        private Song _backgroundSound;

        private GameState _currentState;

        private Texture2D _currentCastleTexture;
        private Vampire _vampire;

        private double _transitionTime;
        private const double TransitionDuration = 2.0; 

        private Texture2D blackTexture; // Voor de zwarte achtergrond

        private Level _currentLevel; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _currentState = GameState.StartScreen;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            _font = Content.Load<SpriteFont>("MenuFont");
            _fontNameGame = Content.Load<SpriteFont>("GameNameFont");

            _backgroundRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            startPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 170, _graphics.PreferredBackBufferHeight / 2 - 30);
            exitPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 30, _graphics.PreferredBackBufferHeight / 2 - 30);

            _backgroundSound = Content.Load<Song>("castle");
            MediaPlayer.Play(_backgroundSound);
            MediaPlayer.IsRepeating = true;

            gameName = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 400, _graphics.PreferredBackBufferHeight / 2 + 205);

            // Zwarte texture voor overgangsdoeleinden
            blackTexture = new Texture2D(GraphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

            _vampire = new Vampire(new Vector2(350, 80));
            _vampire.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            if (_currentState == GameState.StartScreen)
            {
                if (IsMouseOverText(mouseState, startPosition, "Start") && mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    StartTransition(); 
                }
                if (IsMouseOverText(mouseState, exitPosition, "Exit") && mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
                {
                    Exit();
                }
            }
            else if (_currentState == GameState.Transition)
            {
                _transitionTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (_transitionTime >= TransitionDuration)
                {
                    StartGame();  
                }
            }
            else if (_currentState == GameState.Playing)
            {
                bool isAttacking = keyboardState.IsKeyDown(Keys.Z);
                bool isJumping = keyboardState.IsKeyDown(Keys.Space);
                bool isRunning = keyboardState.IsKeyDown(Keys.LeftShift);

                _vampire.Update(gameTime, keyboardState, mouseState, isAttacking, isJumping, isRunning);

                CheckCollision();

                if (_vampire.Position.X <= 0 && _currentLevel != Level.Level2)
                {
                    LoadLevel2(); 
                }

                if (_currentLevel == Level.Level2)
                {
                    if (Math.Abs(_vampire.Position.X - 70) < 10 && Math.Abs(_vampire.Position.Y - 145) < 10)
                    {
                        LoadLevel3();  
                    }
                }
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        private void CheckCollision()
        {
            if (_vampire.Position.X < 0)
                _vampire.Position = new Vector2(0, _vampire.Position.Y);
            if (_vampire.Position.X > _graphics.PreferredBackBufferWidth - _vampire.BoundingBox.Width)
                _vampire.Position = new Vector2(_graphics.PreferredBackBufferWidth - _vampire.BoundingBox.Width, _vampire.Position.Y);

            if (_vampire.Position.Y < 0)
                _vampire.Position = new Vector2(_vampire.Position.X, 0);
            if (_vampire.Position.Y > _graphics.PreferredBackBufferHeight - _vampire.BoundingBox.Height)
                _vampire.Position = new Vector2(_vampire.Position.X, _graphics.PreferredBackBufferHeight - _vampire.BoundingBox.Height);
        }

        private void LoadLevel1()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheets_1");
            _vampire.Position = new Vector2(370, 90);  // Reset de positie  
        }

        private void LoadLevel2()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheet_2");
            _vampire.Position = new Vector2(350, 70);
            _currentLevel = Level.Level2;
        }

        private void LoadLevel3()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheet_3");
            _vampire.Position = new Vector2(30,115);
            _currentLevel = Level.Level3;
        }

        private bool IsMouseOverText(MouseState mouseState, Vector2 position, string text)
        {
            Vector2 textSize = _font.MeasureString(text);
            return mouseState.X >= position.X && mouseState.X <= position.X + textSize.X &&
                   mouseState.Y >= position.Y && mouseState.Y <= position.Y + textSize.Y;
        }

        private void StartTransition()
        {
            _currentState = GameState.Transition;
            _transitionTime = 0;
        }

        private void StartTransitionToNextLevel()
        {
            _currentState = GameState.Transition;
            _transitionTime = 0;
            LoadLevel2();
        }

        private void StartGame()
        {
            _currentState = GameState.Playing;
            LoadLevel1();
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            if (_currentState == GameState.StartScreen)
            {
                _spriteBatch.Draw(background, _backgroundRectangle, Color.White);
                _spriteBatch.DrawString(_fontNameGame, "Castle of Shadows", gameName, Color.White);
                _spriteBatch.DrawString(_font, "Start", startPosition, Color.White);
                _spriteBatch.DrawString(_font, "Exit", exitPosition, Color.White);
            }
            else if (_currentState == GameState.Transition)
            {
                // Zwarte achtergrond voor overgang
                _spriteBatch.Draw(blackTexture, _backgroundRectangle, Color.Black);
                _spriteBatch.DrawString(_font, "Game is Starting...", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 260, _graphics.PreferredBackBufferHeight / 2-30), Color.White);
            }
            else if (_currentState == GameState.Playing)
            {
                _spriteBatch.Draw(blackTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black);
                _spriteBatch.Draw(_currentCastleTexture, new Vector2(0, 0), Color.White);
                _vampire.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
