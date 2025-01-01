using System;

namespace Neagu_Sergiu_Game_Development_Project.HealthClasses
{
    public class Health
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; set; }

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(CurrentHealth - damage, 0); // Zorgt dat de health niet onder 0 gaat
        }

        public void AddHeart(int count)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + count);
        }

        public bool IsDead => CurrentHealth <= 0;
    }
}