using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.Characters;

namespace Neagu_Sergiu_Game_Development_Project.Interfaces
{
    public interface IAnimationStrategy
    {
        void Update(Vampire vampire);
        Texture2D GetCurrentFrame(); 
    }
}