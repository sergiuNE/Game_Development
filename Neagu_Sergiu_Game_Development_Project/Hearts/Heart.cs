using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Neagu_Sergiu_Game_Development_Project.Hearts
{
    public class Heart
    {
        public enum HeartState
        {
            Full,
            Half,
            Empty
        }

        private Texture2D _fullTexture; 
        private Texture2D _halfTexture;
        private Texture2D _emptyTexture;
        private Vector2 _position;
        private HeartState _state;

        public Heart(Vector2 position)
        {
            _position = position;
            _state = HeartState.Full;
        }

        public static Vector2 HeartSize => new Vector2(50, 50);

        public void LoadContent(ContentManager content)
        {
            _fullTexture = content.Load<Texture2D>("heart_full");
            _halfTexture = content.Load<Texture2D>("heart_half");
            _emptyTexture = content.Load<Texture2D>("heart_empty");
        }

        public void SetState(HeartState state)
        {
            _state = state;
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            Texture2D texture = _state switch
            {
                HeartState.Full => _fullTexture,
                HeartState.Half => _halfTexture,
                HeartState.Empty => _emptyTexture,
                _ => _emptyTexture,
            };

            Rectangle destination = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                (int)(HeartSize.X * scale),
                (int)(HeartSize.Y * scale)
            );

            spriteBatch.Draw(texture, destination, Color.White);
        }
    }
}