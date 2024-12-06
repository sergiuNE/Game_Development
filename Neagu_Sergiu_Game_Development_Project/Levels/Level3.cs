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
    public class Level3 : LevelBase
    {
        public Level3(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire)
            : base(graphicsDevice, content, vampire)
        {
            LoadContent();
            _vampire.Position = new Vector2(30, 115); 
        }

        public override void LoadContent()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheet_3");
            _pathBounds = new List<Rectangle>
        {
            new Rectangle(50, 50, 350, 350)
        };

            _blockedAreas = new List<Rectangle>
        {
            new Rectangle(250, 300, 20, 20)
        };
        }
    }

}
