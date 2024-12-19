using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Neagu_Sergiu_Game_Development_Project.Levels
{
    public class Level1 : LevelBase
    {
        public Level1(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire)
            : base(graphicsDevice, content, vampire)
        {
            LoadContent();
            _vampire.Position = new Vector2(370, 90);
        }

        public override void LoadContent()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheets_1");
            _pathBounds = new List<Rectangle>
            {
              new Rectangle(45, 150, 350, 110) //x, y, width, height
            };

            _blockedAreas = new List<Rectangle>
            {
                new Rectangle(49, 130, 300, 100)
            };
        }
    }
}