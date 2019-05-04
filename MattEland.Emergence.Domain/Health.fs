namespace MattEland.Emergence.Domain

type Health (maxHealth: int) =
  let mutable currentHealth = maxHealth

  member this.CurrentHealth = currentHealth;
  member this.MaxHealth = maxHealth;

  member this.IsDead = currentHealth <= 0

  member this.IsFullHealth = currentHealth = maxHealth