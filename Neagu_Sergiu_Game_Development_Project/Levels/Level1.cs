using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Neagu_Sergiu_Game_Development_Project.Characters; 

namespace Neagu_Sergiu_Game_Development_Project.Levels
{
    public class Level1 : LevelBase
    {
        public Level1(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire, List<Hunter> hunterone)
            : base(graphicsDevice, content, vampire, hunterone)
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
        protected override void HandleVampireHunterCollision()
        {
            // Custom collision logic for Level1
            _vampire.Position = _vampire.PreviousPosition;
        }
    }
}