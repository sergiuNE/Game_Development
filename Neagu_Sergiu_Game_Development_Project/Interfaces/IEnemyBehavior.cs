using Microsoft.Xna.Framework;
using Neagu_Sergiu_Game_Development_Project.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neagu_Sergiu_Game_Development_Project.Interfaces
{
    public interface IEnemyBehavior
    {
        void ExecuteBehavior(Enemy enemy, GameTime gameTime);
    }
}
