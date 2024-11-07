using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Neagu_Sergiu_Game_Development_Project
{
    public class Game1 : Game 
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D background;
        private SpriteFont _font; // Font for "Start" en "Exit"
        private SpriteFont _fontNameGame;
        private Rectangle _backgroundRectangle;

        private Vector2 gameName;

        private Vector2 startPosition;
        private Vector2 exitPosition;
        private MouseState _previousMouseState;
        private Song _backgroundSound; // For castle.mp3

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            _font = Content.Load<SpriteFont>("MenuFont"); // Load MenuFont.spritefont
            _fontNameGame = Content.Load<SpriteFont>("GameNameFont");

            // Makes the background image fullscreen
            _backgroundRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            // Position for the Start and Exit 
            startPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 170, _graphics.PreferredBackBufferHeight / 2 - 30);
            exitPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 30, _graphics.PreferredBackBufferHeight / 2 - 30);

            // Load and play the background sound
            _backgroundSound = Content.Load<Song>("castle"); // Load castle.mp3
            MediaPlayer.Play(_backgroundSound);
            MediaPlayer.IsRepeating = true;  // muziek in een loop 

            //Position For The game Name
            gameName = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 400, _graphics.PreferredBackBufferHeight / 2 + 205);

        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            // Check for a mouse click on the "Start" button
            if (IsMouseOverText(mouseState, startPosition, "Start") && mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
            {
                StartGame();
            }

            // Check for a mouse click on the "Exit" button
            if (IsMouseOverText(mouseState, exitPosition, "Exit") && mouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released)
            {
                Exit();
            }

            _previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, _backgroundRectangle, Color.White);
            _spriteBatch.DrawString(_font, "Start", startPosition, Color.White);
            _spriteBatch.DrawString(_font, "Exit", exitPosition, Color.White);
            _spriteBatch.DrawString(_fontNameGame, "Castle of Shadows", gameName, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool IsMouseOverText(MouseState mouseState, Vector2 position, string text)
        {
            Vector2 textSize = _font.MeasureString(text);
            return mouseState.X >= position.X && mouseState.X <= position.X + textSize.X &&
                   mouseState.Y >= position.Y && mouseState.Y <= position.Y + textSize.Y;
        }

        private void StartGame()
        {
            // Logica om het spel te starten (bijv. laden van het eerste level)
        }
    }
}
