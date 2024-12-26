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

        public override void Update(GameTime gameTime, Vampire vampire)
        {
            base.Update(gameTime, vampire);

            // Update health bar position
            _healthBar.Position = HealthBarPosition;

            if (_health.CurrentHealth <= 0)
            {
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
            }

            _healthBar.Update(_health.CurrentHealth, _health.MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
            if (Health.IsDead)
            {
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            _healthBar.Draw(spriteBatch, _health.CurrentHealth, _health.MaxHealth);
        }
    }
}