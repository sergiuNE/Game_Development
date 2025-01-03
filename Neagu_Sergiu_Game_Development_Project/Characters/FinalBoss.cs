using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.HealthClasses;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class FinalBoss : Hunter
    {
        //Health
        public HealthBar _healthBar;
        private Vector2 _healthBarOffset = new Vector2(0, -30);
        public Vector2 HealthBarPosition => Position + _healthBarOffset;

        //Death animation
        private bool _isDead { get; set; } = false;
        private float _deathTimer;
        private const float DeathAnimationDuration = 2f;

        //Hurt
        private bool _isHurt;
        private float _hurtTimer;
        private const float HurtDuration = 0.5f;

        public FinalBoss(Vector2 initialPosition) : base(initialPosition, 2f, 4)
        {
            UniformWidth = 90;
            UniformHeight = 90;
            Health = new Health(4);
        }

        public override void LoadContent(ContentManager content, string texturePath)
        {
            base.LoadContent(content, "FinalBossHunter");

            var backgroundTexture = content.Load<Texture2D>("HealthBarBackground");
            var foregroundTexture = content.Load<Texture2D>("HealthBarForeground");

            // Health bar with different sizes for background and foreground
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
            if (_isHurt || _isDead) return;
            base.TakeDamage(damage);

            _isHurt = true;
            _hurtTimer = 0;

            _healthBar.Update(Health.CurrentHealth, Health.MaxHealth);

            if (Health.CurrentHealth <= 0)
            {
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
                _isDead = true;
                _deathTimer = 0;
            }
        }
      
        public override void Update(GameTime gameTime, Vampire vampire)
        {
            base.Update(gameTime, vampire);

            if (_isHurt)
            {
                _hurtTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_hurtTimer >= HurtDuration)
                {
                    _isHurt = false;
                }
            }

            if (Health.CurrentHealth <= 0) 
            {
                if (_deathTimer == 0)
                {
                    ChangeState("Die");
                }
                _deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_deathTimer >= DeathAnimationDuration)
                {
                  TriggerGameOver();
                }

                return;
            }

            _healthBar.Position = HealthBarPosition;
            _healthBar.Update(Health.CurrentHealth, Health.MaxHealth);
        }

        private void TriggerGameOver()
        {
            Game1.gameOverMenu.ShowGameOverMenu();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (!_isDead)
            {
                _healthBar.Draw(spriteBatch, Health.CurrentHealth, Health.MaxHealth);
            }
        }
    }
}