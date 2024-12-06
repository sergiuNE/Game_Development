using Microsoft.Xna.Framework;
using Neagu_Sergiu_Game_Development_Project.Characters;
using Neagu_Sergiu_Game_Development_Project.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neagu_Sergiu_Game_Development_Project.Behaviors
{
    public class PassiveBehavior : IEnemyBehavior
    {
        public void ExecuteBehavior(Enemy enemy, GameTime gameTime)
        {
            // Vijand blijft stil of beweegt langzaam
        }
    }
}
