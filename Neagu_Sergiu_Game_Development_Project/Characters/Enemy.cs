using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.Interfaces;
using Neagu_Sergiu_Game_Development_Project.Behaviors;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public abstract class Enemy
    {
        public Vector2 Position { get; protected set; }
        public IEnemyBehavior Behavior { get; set; } // Behavior Enemy
        public Vector2 PlayerPosition { get; set; } 
        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        // Abstracte methoden voor content laden en tekenen
        public abstract void LoadContent(ContentManager content);
        public abstract void Draw(SpriteBatch spriteBatch);

        // Update methode voor de vijand
        public void Update(GameTime gameTime)
        {
            // Als er een gedrag is ingesteld, voer het uit
            Behavior?.ExecuteBehavior(this, gameTime);
        }
    }
}
