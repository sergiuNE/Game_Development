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
    public class AggressiveBehavior : IEnemyBehavior
    {
        private float speed = 2f; // Snelheid van de agressieve beweging

        public void ExecuteBehavior(Enemy enemy, GameTime gameTime)
        {
            // Beweeg de vijand naar de speler
            Vector2 direction = enemy.PlayerPosition - enemy.Position; // Richting naar de speler
            direction.Normalize(); // Normaliseer om een consistente snelheid te behouden

            // Beweeg de vijand in de richting van de speler
            enemy.SetPosition(enemy.Position + direction * speed);
        }
    }
}
