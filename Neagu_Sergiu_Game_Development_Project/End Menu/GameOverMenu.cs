﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Neagu_Sergiu_Game_Development_Project.End_Menu
{
    public class GameOverMenu //Dependency Inversion Principle (DIP)
    {
        private SpriteFont _font;
        private Vector2 _position;
        private string[] _options = { "Restart", "Exit" };
        public bool IsGameOver { get; private set; }
        public bool IsGameOverFinalBoss { get; private set; }

        public GameOverMenu(SpriteFont font, Vector2 position)
        {
            _font = font;
            _position = position;
            IsGameOver = false;
            IsGameOverFinalBoss = false;
        }
        public void ShowGameOverMenuFinalBoss()
        {
            IsGameOverFinalBoss = true;
        }

        public void ShowGameOverMenu()
        {
            IsGameOver = true;
        }

        public void HideGameOverMenu()
        {
            IsGameOver = false;
            IsGameOverFinalBoss = false;
        }

        public void Update(Game1 game, KeyboardState keyboardState)
        {
            if (!IsGameOver && !IsGameOverFinalBoss) return;
      
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                game.RestartLevel();
                HideGameOverMenu();
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                game.Exit();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsGameOver)
            {
                spriteBatch.DrawString(_font, "               Game Over", _position, Color.Red);
                spriteBatch.DrawString(_font, "Press Enter to Restart\n            or Esc to Exit\n                YOU LOST !", _position + new Vector2(0, 50), Color.White);
            }
            else if (IsGameOverFinalBoss)
            {
                spriteBatch.DrawString(_font, "               Game Over", _position, Color.Green);
                spriteBatch.DrawString(_font, "Press Enter to Restart\n            or Esc to Exit\n                 YOU WON!", _position + new Vector2(0, 50), Color.White);
            }
        }
    }
}