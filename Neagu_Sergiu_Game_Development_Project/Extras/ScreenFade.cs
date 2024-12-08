using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Neagu_Sergiu_Game_Development_Project.Extras
{
    public class ScreenFade
    {
        private float _opacity;
        private bool _isFadingIn;
        private bool _isFadingOut;
        private float _fadeSpeed;

        public bool IsFadingComplete => _opacity >= 1.0f;

        public ScreenFade()
        {
            _opacity = 0f;
            _fadeSpeed = 0.01f; // Fade speed
            _isFadingIn = false;
            _isFadingOut = false;
        }

        public void StartFadeIn()
        {
            _opacity = 1f;
            _isFadingIn = true;
            _isFadingOut = false;
        }

        public void StartFadeOut()
        {
            _opacity = 0f;
            _isFadingOut = true;
            _isFadingIn = false;
        }

        public void Update()
        {
            if (_isFadingIn)
            {
                _opacity -= _fadeSpeed;
                if (_opacity <= 0f)
                {
                    _opacity = 0f;
                    _isFadingIn = false;
                }
            }
            else if (_isFadingOut)
            {
                _opacity += _fadeSpeed;
                if (_opacity >= 1f)
                {
                    _opacity = 1f;
                    _isFadingOut = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            Texture2D fadeTexture = new Texture2D(graphicsDevice, 1, 1);
            fadeTexture.SetData(new[] { Color.Black });

            spriteBatch.Draw(
                fadeTexture,
                new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
                Color.White * _opacity
            );

            fadeTexture.Dispose();
        }
    }
}
