using System;

namespace MattEland.Emergence.Definitions.Model
{
    public class Health
    {
        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public int MaxHealth { get; }

        public int CurrentHealth { get; private set; }
        public bool IsDead => CurrentHealth <= 0;
        public bool IsFullHealth => CurrentHealth == MaxHealth;

        public int AdjustHealth(int delta) => CurrentHealth = Math.Min(Math.Max(0, CurrentHealth + delta), MaxHealth);
    }
}