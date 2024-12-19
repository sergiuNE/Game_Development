using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Neagu_Sergiu_Game_Development_Project.Characters
{
    public class HunterTwo:Hunter
    {
        public HunterTwo(Vector2 initialPosition) : base(initialPosition, 2f)
        {
        }

        public override void LoadContent(ContentManager content, string texturePath)
        {
            base.LoadContent(content, "HunterTwo");
        }
    }
}
