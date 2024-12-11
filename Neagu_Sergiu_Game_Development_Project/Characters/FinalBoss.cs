using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Neagu_Sergiu_Game_Development_Project.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class FinalBoss:Hunter
    {
        public FinalBoss(Vector2 initialPosition) : base(initialPosition, 2f)
        {
            UniformWidth = 90; 
            UniformHeight = 90;
        }

        public override void LoadContent(ContentManager content, string texturePath)
        {
            base.LoadContent(content, "FinalBossHunter"); 
        }
    }
}
