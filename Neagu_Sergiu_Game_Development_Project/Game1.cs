using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Neagu_Sergiu_Game_Development_Project.Characters;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using Neagu_Sergiu_Game_Development_Project.End_Menu;
using Neagu_Sergiu_Game_Development_Project.Hearts;
using Neagu_Sergiu_Game_Development_Project.Levels;
using System;
using System.Collections.Generic;

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
        private Rectangle _backgroundRectangle;

        //Fonts
        private SpriteFont _font;
        private SpriteFont _fontNameGame;

        private Vector2 gameName;
        private Vector2 startPosition; 
        private Vector2 exitPosition;

        //MouseState
        private MouseState _previousMouseState; 

        //GameState
        public GameState _currentState;

        //Vampire
        private Vampire _vampire;

        //Transitions
        private double _transitionTime;
        private const double TransitionDuration = 2.0;

        //Texture
        private Texture2D blackTexture;
        private Texture2D _currentCastleTexture;

        //Level
        private Level _currentLevel;
        private LevelBase _currentLevelClass;

        //Hearts
        private List<Heart> _hearts; // For the UI
        private const int MaxHealth = 7;
        private static int _currentHealth = 6;
        private const float HeartScale = 0.6f;
        private const int HeartSpacing = 5;

        //Hunters
        public List<Hunter> _hunters { get; private set; }

        //FinalBoss
        private FinalBoss _finalBoss;

        //GameOverMenu
        public bool IsGameOver => _currentState == GameState.GameOver;
        public static GameOverMenu gameOverMenu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _hunters = new List<Hunter>();
        }

        protected override void Initialize()
        {
            //_graphics.IsFullScreen = true;
            //_graphics.ApplyChanges();
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

            SpriteFont font = Content.Load<SpriteFont>("MenuFont");
            gameOverMenu = new GameOverMenu(font, new Vector2(50, 100));

            // Black texture for transitions
            blackTexture = new Texture2D(GraphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

            _vampire = new Vampire(new Vector2(350, 80),7, this);
            _vampire.LoadContent(Content);

            // Hearts
            _hearts = new List<Heart>();
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
                    hunter.Update(gameTime, _vampire);
                }

                _currentLevelClass.CheckCollision();

                // Check if the vampire changes levels
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

                // Damage to the vampire
                if (_vampire._isHurt)
                {
                    RemoveHeart(1);
                    _vampire._isHurt = false;
                }

                // Recover health when a Hunter is dead
                var deadHunters = new List<Hunter>();
                 foreach (var hunter in _hunters)
                 {
                     if (hunter.Health.IsDead)
                     {
                         AddHeart(1);
                         deadHunters.Add(hunter);
                    }
                 }

                // Delete dead Hunters
                foreach (var hunter in deadHunters)
                {
                    _hunters.Remove(hunter);
                }

                //FinalBoss check
                if (_finalBoss != null)
                {
                   
                    if (_finalBoss.Health.IsDead)
                    {
                        if (_currentState != GameState.GameOver)
                        {
                            _currentState = GameState.GameOver;
                            gameOverMenu.ShowGameOverMenuFinalBoss();
                        }

                        _finalBoss = null;
                        return;
                    }
                }

                // Check for Game Over for Vampire
                if (_currentHealth <= 0 && _currentState != GameState.GameOver)
                {
                    _currentState = GameState.GameOver;
                    _vampire.IsDead = true; 
                    gameOverMenu.ShowGameOverMenu();
                    return; // Stop further updates
                }

            }
            else if (_currentState == GameState.GameOver)
            {
                gameOverMenu.Update(this, keyboardState);
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        private void UpdateHearts()
        {
            // Only update if the heart count has changed
            if (_hearts.Count != _currentHealth)
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
        }

        public void AddHeart(int amount)
        {
            _currentHealth = Math.Min(_currentHealth + amount, MaxHealth);
            UpdateHearts();
        }

        public void RemoveHeart(int amount)
        {
            _currentHealth = Math.Max(_currentHealth - amount, 0);
            UpdateHearts();

            if (_currentHealth <= 0)
            {
                _currentState = GameState.GameOver;
                gameOverMenu.ShowGameOverMenu();
            }
        }

        public void RestartLevel()
        {
            _currentHealth = 3;
            UpdateHearts();

            _currentState = GameState.StartScreen;
       
            _hunters.Clear();
            _currentLevel = Level.Level1;

            Initialize();
            LoadContent();
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
            _currentLevelClass = new Level1(GraphicsDevice, Content, _vampire, _hunters);
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
            _currentLevelClass = new Level2(GraphicsDevice, Content, _vampire, _hunters);
            _currentCastleTexture = _currentLevelClass.GetCastleTexture();
            _currentLevel = Level.Level2;

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
            _currentLevelClass = new Level3(GraphicsDevice, Content, _vampire, _hunters);
            _currentCastleTexture = _currentLevelClass.GetCastleTexture();
            _currentLevel = Level.Level3;

            _hunters.Clear();
            Vector2 positionFinalBoss = new Vector2(90, 90);

            _finalBoss = HunterFactory.CreateHunter(HunterType.FinalBossHunter, positionFinalBoss) as FinalBoss;
            _hunters.Add(_finalBoss);

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

                foreach (var hunter in _hunters)
                {
                    hunter.Draw(_spriteBatch);
                }

                foreach (var heart in _hearts)
                {
                    heart.Draw(_spriteBatch, HeartScale);
                }
            }

            else if (_currentState == GameState.GameOver)
            {
                _spriteBatch.Draw(blackTexture, _backgroundRectangle, Color.Black);
                gameOverMenu.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}