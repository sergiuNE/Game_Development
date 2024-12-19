using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.HealthClasses;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class FinalBoss : Hunter
    {
        private Health _health;
        private HealthBar _healthBar;
        private Vector2 _healthBarOffset = new Vector2(0, -30);

        public Vector2 HealthBarPosition => Position + _healthBarOffset;

        public FinalBoss(Vector2 initialPosition) : base(initialPosition, 2f)
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update health bar position
            _healthBar.Position = HealthBarPosition;

            // Simulate damage or healing for testing if needed
            /*
             * if (damage condition)  
             * {
             *    _health.TakeDamage(10);
             * }
             *
             * if (_health.IsDead) 
             * {
             *    ChangeState("Die");
             * }
             */
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            _healthBar.Draw(spriteBatch, _health.CurrentHealth, _health.MaxHealth);
        }
    }
}