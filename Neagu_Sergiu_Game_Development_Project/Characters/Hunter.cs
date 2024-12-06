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

        public Hunter(Vector2 position)
        {
            this.Position = position;
            this.Behavior = new AggressiveBehavior(); // Stel het gedrag in op agressief
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("hunter"); // Laad de afbeelding voor de hunter
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
