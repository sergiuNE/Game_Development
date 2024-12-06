using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Neagu_Sergiu_Game_Development_Project.Levels
{
    public class Level2 : LevelBase
    {
        public Level2(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire)
            : base(graphicsDevice, content, vampire)
        {
            LoadContent();
            _vampire.Position = new Vector2(350, 70); 
        }

        public override void LoadContent()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheet_2");
            _pathBounds = new List<Rectangle>
        {
            new Rectangle(100, 100, 300, 300)
        };

            _blockedAreas = new List<Rectangle>
        {
            new Rectangle(200, 300, 100, 50)
        };
        }
    }

}
