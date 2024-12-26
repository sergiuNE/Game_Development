﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Neagu_Sergiu_Game_Development_Project.Characters 
{
    public class HunterOne:Hunter
    {
        public HunterOne(Vector2 initialPosition) : base(initialPosition, 2f, 2)
        {}

        public override void LoadContent(ContentManager content, string texturePath)
        {
            base.LoadContent(content, "HunterOne");
        }

        public override void Update(GameTime gameTime, Vampire vampire)
        {
            base.Update(gameTime, vampire);
        }
    }
}