using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Neagu_Sergiu_Game_Development_Project.Characters 
{
    public class HunterTwo:Hunter
    {
        public HunterTwo(Vector2 initialPosition) : base(initialPosition, 2f, 2)
        {}

        public override void LoadContent(ContentManager content, string texturePath)
        {
            base.LoadContent(content, "HunterTwo");
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override void Update(GameTime gameTime, Vampire vampire)
        {
            base.Update(gameTime, vampire);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}