using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Neagu_Sergiu_Game_Development_Project.Characters;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using Neagu_Sergiu_Game_Development_Project.Hearts;
using Neagu_Sergiu_Game_Development_Project.Levels;
using System;
using System.Collections.Generic;
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

        private GameState _currentState;

        private Texture2D _currentCastleTexture;
        private Vampire _vampire;

        private double _transitionTime;
        private const double TransitionDuration = 2.0;

        //Texture
        private Texture2D blackTexture;

        //Level
        private Level _currentLevel;
        private LevelBase _currentLevelClass;

        //Hearts
        private List<Heart> _hearts; // For the UI
        private int _maxHealth = 5;
        private int _currentHealth = 2;
        private const float HeartScale = 0.6f;
        private const int HeartSpacing = 5;

        //Hunter
        private List<Hunter> _hunters;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _hunters = new List<Hunter>();
        }

        protected override void Initialize()
        {
            // _graphics.IsFullScreen = true;
            // _graphics.ApplyChanges();
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

            var castleTheme = Content.Load<Song>("castle");
            BackgroundMusicManager.Instance.Play(castleTheme);
            MediaPlayer.IsRepeating = true;

            gameName = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 400, _graphics.PreferredBackBufferHeight / 2 + 205);

            // Zwarte texture voor overgangsdoeleinden
            blackTexture = new Texture2D(GraphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

            _vampire = new Vampire(new Vector2(350, 80));
            _vampire.LoadContent(Content);

            // Harten initialiseren
            _hearts = new List<Heart>();
            for (int i = 0; i < _currentHealth; i++) // Begin met aantal levens
            {
                Vector2 position = new Vector2(
                    _graphics.PreferredBackBufferWidth - (Heart.HeartSize.X * HeartScale + HeartSpacing) * (i + 1),
                    HeartSpacing
                );
                Heart heart = new Heart(position);
                heart.LoadContent(Content);
                _hearts.Add(heart);
            }

            // Load content for all hunters
            foreach (var hunter in _hunters)
            {
                hunter.LoadContent(Content, hunter.GetType().Name); 
            }
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
                bool isAttacking = mouseState.LeftButton == ButtonState.Pressed;
                bool isJumping = keyboardState.IsKeyDown(Keys.Space);
                bool isRunning = keyboardState.IsKeyDown(Keys.LeftShift);

                _vampire.Update(gameTime, keyboardState, mouseState, isAttacking, isJumping, isRunning);

                foreach (var hunter in _hunters)
                {
                    hunter.Update(gameTime);
                }

                _currentLevelClass.CheckCollision();

                // Simuleer schade voor testen
                if (keyboardState.IsKeyDown(Keys.D1) && _currentHealth > 0)
                {
                    _currentHealth--;
                    UpdateHearts();
                }
                if (keyboardState.IsKeyDown(Keys.D2) && _currentHealth < _maxHealth)
                {
                    _currentHealth++;
                    UpdateHearts();
                }

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

        private void UpdateHearts()
        {
            _hearts.Clear();
            for (int i = 0; i < _currentHealth; i++)
            {
                Vector2 position = new Vector2(
                    _graphics.PreferredBackBufferWidth - (Heart.HeartSize.X * HeartScale + HeartSpacing) * (i + 1),
                    HeartSpacing
                );
                Heart heart = new Heart(position);
                heart.LoadContent(Content);
                _hearts.Add(heart);
            }
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

        private void StartGame()
        {
            _currentState = GameState.Playing;
            LoadLevel1();
        }

        private void LoadLevel1()
        {
            _currentLevelClass = new Level1(GraphicsDevice, Content, _vampire);
            _currentCastleTexture = _currentLevelClass.GetCastleTexture();
            _currentLevel = Level.Level1;

            // Hunters for Level 1
            _hunters.Clear();
            Vector2 positionHunterOne = new Vector2(50, 260);
            _hunters.Add(HunterFactory.CreateHunter(HunterType.HunterOne, positionHunterOne));
            foreach (var hunter in _hunters)
            {
                hunter.LoadContent(Content, hunter.GetType().Name);
            }
        }

        private void LoadLevel2()
        {
            _currentLevelClass = new Level2(GraphicsDevice, Content, _vampire);
            _currentCastleTexture = _currentLevelClass.GetCastleTexture();
            _currentLevel = Level.Level2;

            // Hunters for Level 2
            _hunters.Clear();
            Vector2 positionHunterTwo = new Vector2(100, 190);
            Vector2 positionHunterThree = new Vector2(550, 190);
            _hunters.Add(HunterFactory.CreateHunter(HunterType.HunterTwo, positionHunterTwo));
            _hunters.Add(HunterFactory.CreateHunter(HunterType.HunterThree, positionHunterThree));
            foreach (var hunter in _hunters)
            {
                hunter.LoadContent(Content, hunter.GetType().Name);
            }
        }

        private void LoadLevel3()
        {
            _currentLevelClass = new Level3(GraphicsDevice, Content, _vampire);
            _currentCastleTexture = _currentLevelClass.GetCastleTexture();
            _currentLevel = Level.Level3;

            // Hunters for Level 3
            _hunters.Clear();
            Vector2 positionFinalBoss = new Vector2(90, 90);
            _hunters.Add(HunterFactory.CreateHunter(HunterType.FinalBossHunter, positionFinalBoss));
            foreach (var hunter in _hunters)
            {
                hunter.LoadContent(Content, hunter.GetType().Name);
            }
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
                // Black background for transition
                _spriteBatch.Draw(blackTexture, _backgroundRectangle, Color.Black);
                _spriteBatch.DrawString(_font, "Game is Starting...", new Vector2(_graphics.PreferredBackBufferWidth / 2 - 260, _graphics.PreferredBackBufferHeight / 2 - 30), Color.White);
            }
            else if (_currentState == GameState.Playing)
            {
                _spriteBatch.Draw(blackTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black);
                _spriteBatch.Draw(_currentCastleTexture, new Vector2(0, 0), Color.White);
                _vampire.Draw(_spriteBatch);

                // Draw all hunters
                foreach (var hunter in _hunters)
                {
                    hunter.Draw(_spriteBatch);
                }

                // Teken de harten
                foreach (var heart in _hearts)
                {
                    heart.Draw(_spriteBatch, HeartScale);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}