using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Neagu_Sergiu_Game_Development_Project.Characters; 

namespace Neagu_Sergiu_Game_Development_Project.Levels
{
    public class Level3 : LevelBase 
    {
        public Level3(GraphicsDevice graphicsDevice, ContentManager content, Vampire vampire, List<Hunter> finalBoss)
            : base(graphicsDevice, content, vampire, finalBoss)
        {
            LoadContent();
            _vampire.Position = new Vector2(30, 115);
        }

        public override void LoadContent()
        {
            _currentCastleTexture = Content.Load<Texture2D>("castle_spritesheet_3"); 
            _pathBounds = new List<Rectangle>
            {
                new Rectangle(50, 150, 90, 5)
            };
        }

        protected override void HandleVampireHunterCollision()
        {
            _vampire.Position = _vampire.PreviousPosition;
        }
    }
}