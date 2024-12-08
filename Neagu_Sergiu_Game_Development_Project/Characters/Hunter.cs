using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Neagu_Sergiu_Game_Development_Project.Design_Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neagu_Sergiu_Game_Development_Project.Interfaces;
using Neagu_Sergiu_Game_Development_Project.Behaviors;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class Hunter : Enemy
    {
        private Texture2D _texture;
        private Vector2 _position;
        public Rectangle BoundingBox => new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);

        public Hunter(Vector2 position)
        {
            this.Position = position;
            this.Behavior = new AggressiveBehavior(); // Stel het gedrag in op agressief
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("hunter"); // Laad de afbeelding voor de hunter
        }

        /* public void Update(GameTime gameTime, Vector2 playerPosition, Action onPlayerHit)
        {
        // Simpele logica om naar de speler toe te bewegen
        Vector2 direction = playerPosition - _position;
        if (direction.Length() > 1)
        {
            direction.Normalize();
            _position += direction * 2f; // Beweging naar de speler
        }

        // Check op collision met speler
        if (BoundingBox.Intersects(new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 50, 50))) // Placeholder voor speler's bounding box
        {
            onPlayerHit?.Invoke(); // Roept de schade-methode aan
        }
    }*/

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
