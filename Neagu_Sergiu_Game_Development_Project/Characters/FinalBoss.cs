﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.End_Menu;
using Neagu_Sergiu_Game_Development_Project.HealthClasses; 

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class FinalBoss : Hunter
    {
        private Health _health;
        private HealthBar _healthBar;
        private Vector2 _healthBarOffset = new Vector2(0, -30);
        public Vector2 HealthBarPosition => Position + _healthBarOffset;

        public FinalBoss(Vector2 initialPosition) : base(initialPosition, 2f, 4)
        {
            UniformWidth = 90;
            UniformHeight = 90;
            _health = new Health(100);
        }

        public override void LoadContent(ContentManager content, string texturePath)
        {
            base.LoadContent(content, "FinalBossHunter");

            var backgroundTexture = content.Load<Texture2D>("HealthBarBackground");
            var foregroundTexture = content.Load<Texture2D>("HealthBarForeground");

            // Initialize the health bar with different sizes for background and foreground
            _healthBar = new HealthBar(
                backgroundTexture,
                foregroundTexture,
                HealthBarPosition,
                130, // Background width
                50,  // Background height
                90,
                16,
                35
            );
        }

        public override void TakeDamage(int damage)
        {
            if (IsDead) return;
            base.TakeDamage(damage); // Ensure flicker and death logic are applied
                                     // Reduce health bar by 25% per hit
            _health.CurrentHealth -= 25;

            if (_health.CurrentHealth <= 0)
            {
                _health.CurrentHealth = 0;
                IsDead = true;
                TriggerGameOver();
            }
        }
       
        private void TriggerGameOver()
        {
            Game1.gameOverMenu.ShowGameOverMenu();
        }

        public override void Update(GameTime gameTime, Vampire vampire)
        {
            base.Update(gameTime, vampire);

            // Update health bar position
            _healthBar.Position = HealthBarPosition;

            _healthBar.Update(_health.CurrentHealth, _health.MaxHealth);

            // Game logic for when finalboss dies
            if (Health.IsDead)
            {
                TriggerGameOver();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            _healthBar.Draw(spriteBatch, _health.CurrentHealth, _health.MaxHealth);
        }
    }
}