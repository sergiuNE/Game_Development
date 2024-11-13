using Microsoft.Xna.Framework.Graphics;
using Neagu_Sergiu_Game_Development_Project.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neagu_Sergiu_Game_Development_Project
{
    public interface IAnimationStrategy
    {
        void Update(Vampire vampire);
        Texture2D GetCurrentFrame();
    }
}
