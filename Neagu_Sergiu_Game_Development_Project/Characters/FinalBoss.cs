using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.HealthClasses;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class FinalBoss : Hunter
    {
        //Health
        private Health _health;
        private HealthBar _healthBar;
        private Vector2 _healthBarOffset = new Vector2(0, -30);
        public Vector2 HealthBarPosition => Position + _healthBarOffset;

        // Death animation
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
            _health = new Health(100);
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

            // Update health bar
            _healthBar.Update(Health.CurrentHealth, Health.MaxHealth);

            if (_health.CurrentHealth <= 0)
            {
                _isDead = true;
                _deathTimer = 0;
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
            }
        }
      

        public override void Update(GameTime gameTime, Vampire vampire)
        {
            /*base.Update(gameTime, vampire);

            // Game logic for when vampire dies
           
            if (_health.IsDead)
            {
                _deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_deathTimer >= DeathAnimationDuration)
                {
                    TriggerGameOver();
                }

                return;
            }

            // Update hurt timer
            if (_isHurt)
            {
                _hurtTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_hurtTimer >= HurtDuration)
                {
                    _isHurt = false;
                }
            }

            // Update health bar position
            _healthBar.Position = HealthBarPosition;
            _healthBar.Update(_health.CurrentHealth, _health.MaxHealth);*/
            base.Update(gameTime, vampire);

            if (_isDead)
            {
                _deathTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                return; // Stop verdere updates
            }

            if (_health.IsDead && !_isDead)
            {
                _isDead = true;
                _deathTimer = 0;
                _currentAnimation = _isFacingRight ? _animations["dieRight"] : _animations["dieLeft"];
                TriggerGameOver();
            }

            if (_isHurt)
            {
                _hurtTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_hurtTimer >= HurtDuration)
                {
                    _isHurt = false;
                }
            }

            _healthBar.Position = HealthBarPosition;
            _healthBar.Update(_health.CurrentHealth, _health.MaxHealth);
        }

        private void TriggerGameOver()
        {
            Game1.gameOverMenu.ShowGameOverMenu();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //_healthBar.Draw(spriteBatch, _health.CurrentHealth, _health.MaxHealth);
            if (!_isDead)
            {
                _healthBar.Draw(spriteBatch, _health.CurrentHealth, _health.MaxHealth);
            }
        }
    }
}