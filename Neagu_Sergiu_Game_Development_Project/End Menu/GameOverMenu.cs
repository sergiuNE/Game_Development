using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Neagu_Sergiu_Game_Development_Project.End_Menu
{
    public class GameOverMenu
    {
        private SpriteFont _font;
        private Vector2 _position;
        private string[] _options = { "Restart", "Exit" };
        private int _selectedIndex = 0;
        private KeyboardState _previousKeyboardState; // For input debouncing

        // Constructor
        public GameOverMenu(SpriteFont font, Vector2 position)
        {
            _font = font;
            _position = position;
        }

        // Updates the menu based on keyboard input
        public void Update(Game1 game, KeyboardState keyboardState)
        {
            // Debouncing input to avoid rapid changes
            if (_previousKeyboardState == null)
                _previousKeyboardState = keyboardState;

            if (IsKeyPressed(keyboardState, Keys.Up))
            {
                _selectedIndex = Math.Max(0, _selectedIndex - 1);
            }
            else if (IsKeyPressed(keyboardState, Keys.Down))
            {
                _selectedIndex = Math.Min(_options.Length - 1, _selectedIndex + 1);
            }
            else if (IsKeyPressed(keyboardState, Keys.Enter))
            {
                HandleSelection(game);
            }

            _previousKeyboardState = keyboardState; // Update the previous state
        }

        // Draws the menu
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _options.Length; i++)
            {
                var color = i == _selectedIndex ? Color.Red : Color.White;
                spriteBatch.DrawString(_font, _options[i], _position + new Vector2(0, i * 30), color);
            }
        }

        // Checks if a key is newly pressed
        private bool IsKeyPressed(KeyboardState currentState, Keys key)
        {
            return currentState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
        }

        // Handles the selected menu option
        private void HandleSelection(Game1 game)
        {
            if (_selectedIndex == 0)
            {
                game.RestartLevel(); // Restart the game
            }
            else if (_selectedIndex == 1)
            {
                game.Exit(); // Exit the game
            }
        }
    }
}
