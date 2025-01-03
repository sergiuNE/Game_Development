using System;

namespace Neagu_Sergiu_Game_Development_Project.HealthClasses 
{
    public class Health
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; set; }
        public bool IsDead => CurrentHealth <= 0;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(CurrentHealth - damage, 0); 
        }

        public void AddHeart(int count)
        {
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + count);
        }
    }
}