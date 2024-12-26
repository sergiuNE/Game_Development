using System;

namespace Neagu_Sergiu_Game_Development_Project.HealthClasses
{
    public class Health
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth = Math.Max(CurrentHealth - damage, 0); // Zorgt dat de health niet onder 0 gaat
        }

        public void Heal(int amount)
        {
            CurrentHealth = Math.Min(CurrentHealth + amount, MaxHealth); // Zorgt dat de health niet boven MaxHealth gaat
        }

        public bool IsDead => CurrentHealth <= 0;
    }
}