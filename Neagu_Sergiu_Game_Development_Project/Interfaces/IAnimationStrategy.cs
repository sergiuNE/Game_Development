using Microsoft.Xna.Framework.Graphics;

namespace Neagu_Sergiu_Game_Development_Project.Interfaces
{
    public interface IAnimationStrategy
    {
        void Update(Vampire vampire);
        Texture2D GetCurrentFrame();
    }
}