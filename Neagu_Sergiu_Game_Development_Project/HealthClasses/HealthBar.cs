using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Neagu_Sergiu_Game_Development_Project.HealthClasses
{
    public class HealthBar
    {
        private Texture2D _backgroundTexture;
        private Texture2D _foregroundTexture;
        public Vector2 Position { get; set; }
        private int _backgroundWidth;
        private int _backgroundHeight;

        private int _foregroundWidth;
        private int _foregroundHeight;

        private int _foregroundOffsetX; // Offset voor foreground naar rechts

        private float _healthPercentage; // Tracks the current health as a percentage

        public HealthBar(Texture2D backgroundTexture, Texture2D foregroundTexture, Vector2 position, int backgroundWidth, int backgroundHeight, int foregroundWidth, int foregroundHeight, int foregroundOffsetX)
        {
            _backgroundTexture = backgroundTexture;
            _foregroundTexture = foregroundTexture;
            Position = position;
            _backgroundWidth = backgroundWidth;
            _backgroundHeight = backgroundHeight;
            _foregroundWidth = foregroundWidth;
            _foregroundHeight = foregroundHeight;
            _foregroundOffsetX = foregroundOffsetX; // Stel de horizontale offset in
            _healthPercentage = 1.0f; // Default to 100% health
        }

        public void Update(int currentHealth, int maxHealth)
        {
            // Calculate the current health percentage
            _healthPercentage = currentHealth / (float)maxHealth;
            // Ensure it remains within 0 and 1 bounds
            _healthPercentage = MathHelper.Clamp(_healthPercentage, 0.0f, 1.0f);
        }

        public void Draw(SpriteBatch spriteBatch, int currentHealth, int maxHealth)
        {
            // Teken de achtergrond
            spriteBatch.Draw(
                _backgroundTexture,
                new Rectangle((int)Position.X, (int)Position.Y, _backgroundWidth, _backgroundHeight),
                Color.White
            );

            // Bereken de breedte van de foreground gebaseerd op de huidige gezondheid
            int healthBarWidth = (int)((currentHealth / (float)maxHealth) * _foregroundWidth);

            // Bereken de Y-offset zodat de foreground gecentreerd wordt binnen de achtergrond
            int verticalOffset = (_backgroundHeight - _foregroundHeight) / 2;

            // Teken de foreground, met de extra offset naar rechts
            spriteBatch.Draw(
                _foregroundTexture,
                new Rectangle(
                    (int)Position.X + _foregroundOffsetX,        // Voeg de horizontale offset toe
                    (int)Position.Y + verticalOffset,            // Verticaal gecentreerd
                    healthBarWidth,                              // Dynamische breedte gebaseerd op gezondheid
                    _foregroundHeight                            // Vastgestelde hoogte van de foreground
                ),
                Color.Red
            );
        }
    }
}