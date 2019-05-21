using System;
using System.Linq;
using System.Text;
using GeneticSharp.Domain.Randomizations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Game
{
    /// <summary>
    /// Handles all combat calculations and coordinates the results of combat with the level and any impacted objects
    /// </summary>
    public class CombatManager : ICombatManager
    {
        /// <summary>
        /// Handles the details for a direct attack from an <paramref name="attacker"/> on a <paramref name="defender"/>.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="attacker">The attacker.</param>
        /// <param name="defender">The defender.</param>
        public void HandleAttack(CommandContext context, IGameObject attacker, IGameObject defender, string verb, DamageType damageType)
        {
            // Figure out if the attack lands
            if (!DetermineIfAttackHits(context, attacker, defender))
            {
                if (attacker.IsPlayer || defender.IsPlayer || context.CanPlayerSee(defender))
                {
                    context.AddEffect(new MissedEffect(defender));
                }

                return;
            }

            int damage = CalculateDamage(context, attacker, defender, verb);

            // TODO: Log to an analytics framework

            string message;
            if (damage <= 0)
            {
                message = defender.IsInvulnerable 
                    ? $"{attacker.Name} {verb} {defender.Name} but it is impervious to all damage." 
                    : $"{attacker.Name} {verb} {defender.Name} but deals no damage.";
            }
            else
            {
                message = HurtObject(context, defender, damage, attacker, verb, damageType);
            }

            // Only add this message if it occurs somewhere within the player's line of sight or involves the player
            if ((attacker.IsPlayer || defender.IsPlayer || context.CanPlayerSee(attacker) ||
                 context.CanPlayerSee(defender)) && !string.IsNullOrWhiteSpace(message))
            {
                var messageType = DetermineCombatMessageType(damage, defender.IsDead);

                context.AddMessage(message, messageType);

                if (damage <= 0)
                {
                    context.AddEffect(new NoDamageEffect(defender));
                }
            }
        }

        /// <summary>
        /// Determines the amount of damage an attack will do.
        /// </summary>
        /// <param name="context">The command context used for logging.</param>
        /// <param name="attacker">The actor carrying out the attack.</param>
        /// <param name="defender">The actor defending against the attack.</param>
        /// <returns>The amount of damage to deliver</returns>
        private static int CalculateDamage(CommandContext context, IGameObject attacker, IGameObject defender, string verb)
        {
            // Short circuit everything if we're targeting something invulnerable
            if (defender.IsInvulnerable)
            {
                return 0;
            }

            int attack = CalculateAttackStrength(attacker, context.Randomizer);
            int defense = CalculateDefenseStrength(defender, context.Randomizer);

            int damage = Math.Max(1, attack - defense);

            // Only add this message if it occurs somewhere within the player's line of sight or involves the player
            if (attacker.IsPlayer || defender.IsPlayer || context.CanPlayerSee(attacker.Pos) ||
                context.CanPlayerSee(defender.Pos))
            {
                context.AddMessage(
                    $"{attacker.Name} {verb} with {attack} strength vs {defender.Name}'s {defense} defense",
                    ClientMessageType.Math);
            }

            return damage;
        }

        /// <summary>
        /// Determines the type of message to use for the combat result message based on the combat parameters.
        /// </summary>
        /// <param name="damage">The damage done.</param>
        /// <param name="targetDied">if set to <c>true</c> the attack killed the target.</param>
        /// <returns>ClientMessageType.</returns>
        private static ClientMessageType DetermineCombatMessageType(int damage, bool targetDied)
        {
            if (targetDied)
            {
                return ClientMessageType.Success;
            }

            return damage <= 0 ? ClientMessageType.Failure : ClientMessageType.Generic;
        }

        /// <summary>
        /// Hurts the <paramref name="defender"/> for <paramref name="damage"/> damage and outputs necessary events and messages.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="defender">The defender.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="attacker">The attacker.</param>
        /// <param name="verb">A verb describing the action.</param>
        /// <returns>The message to log</returns>
        public string HurtObject(CommandContext context, IGameObject defender, int damage, IGameObject attacker, string verb, DamageType damageType)
        {
            if (defender.IsCapturable)
            {
                HandleCapture(context, defender, attacker);
                return null;
            }

            // Tell anyone who cares - but do it before any corruption, etc. takes place.
            context.PreviewObjectHurt(attacker, defender, damage, damageType);

            if (damageType == DamageType.Normal || damageType == DamageType.Combination)
            {

                defender.Stability -= damage;

                // Ensure the player's stats get updated
                if (defender is IActor defendingActor)
                {
                    defendingActor.DamageReceived += damage;
                }

                if (attacker is IActor attackingActor)
                {
                    attackingActor.DamageDealt += damage;
                }
            }

            if ((damageType == DamageType.Corruption || damageType == DamageType.Combination) && defender.IsCorruptable)
            {
                var originalCorruption = defender.Corruption;
                defender.ApplyCorruptionDamage(context, attacker, damage);
                if (attacker is IActor attackingActor)
                {
                    attackingActor.DamageDealt += Math.Abs(defender.Corruption - originalCorruption);
                }
            }

            // Add the damage to the object
            if (attacker.IsPlayer || defender.IsPlayer || context.CanPlayerSee(defender.Pos))
            {
                context.AddEffect(new DamagedEffect(defender, damage, damageType));
            }

            var isDead = defender.Stability <= 0;

            string damageTypeString;
            switch (damageType)
            {
                case DamageType.Corruption:
                    damageTypeString = "Corruption";
                    break;
                case DamageType.Combination:
                    damageTypeString = "Corruption Damage";
                    break;
                default:
                    damageTypeString = "Damage";
                    break;
            }

            var sb = new StringBuilder($"{attacker.Name} {verb} {defender.Name} for {damage} {damageTypeString}");
            if (isDead)
            {
                // Log the kill
                sb.Append(", terminating it");

                context.HandleObjectKilled(defender, attacker);

            }

            return sb.ToString();
        }

        public void HandleExplosion(CommandContext context, IGameObject executor, Pos2D epicenter, int strength, int radius, DamageType damageType)
        {
            var candidates = context.GetCellsVisibleFromPoint(epicenter, radius).ToList();
            foreach (var cell in candidates)
            {
                // Spread corruption as needed
                if ((damageType == DamageType.Corruption || damageType == DamageType.Combination) && context.Randomizer.GetDouble() > 0.3) // TODO: Distance / falloff might be nice here too
                {
                    cell.Corruption++;
                }

                foreach (var obj in cell.Objects.ToList())
                {
                    if (!obj.IsTargetable || obj.IsDead)
                    {
                        continue;
                    }

                    int resistance = CalculateDamageResistance(obj, damageType, context.Randomizer);
                    int damage = Math.Max(0, strength - resistance);

                    if (obj.IsInvulnerable)
                    {
                        damage = 0;
                    }

                    var isVisible = obj.IsPlayer || executor.IsPlayer || context.CanPlayerSee(obj.Pos);
                    bool showMessage = obj is Actor && isVisible;

                    if (damage <= 0)
                    {
                        if (showMessage)
                        {
                            if (obj.IsInvulnerable)
                            {
                                context.AddMessage($"{obj.Name} is impervious to all damage", ClientMessageType.Math);
                            }
                            else
                            {
                                context.AddMessage(
                                    $"{obj.Name} resists all damage ({resistance} resistance vs {strength} strength)",
                                    ClientMessageType.Math);
                            }
                        }

                        if (isVisible)
                        {
                            context.AddEffect(new NoDamageEffect(obj));
                        }

                    }
                    else
                    {
                        var harmResult = HurtObject(context, obj, damage, executor, "damages", damageType);

                        if (showMessage)
                        {
                            context.AddMessage(harmResult, ClientMessageType.Generic);
                        }
                    }
                }
            }

        }

        private static int CalculateDamageResistance(IGameObject gameObject, DamageType damageType, IRandomization random)
        {
            if (damageType != DamageType.Normal)
            {
                return 0;
            }

            return gameObject is IActor actor ? CalculateDefenseStrength(actor, random) : 0;
        }

        /// <summary>
        /// Handles the capture of another actor.
        /// </summary>
        /// <param name="context">The command context used to interact with the game world.</param>
        /// <param name="defender">The object being captured.</param>
        /// <param name="attacker">The actor doing the capturing</param>
        public void HandleCapture(CommandContext context, IGameObject defender, IGameObject attacker)
        {
            if (defender.Team == attacker.Team)
            {
                return;
            }

            var oldTeam = defender.Team;

            // Perform the capture
            defender.Team = attacker.Team;

            defender.OnCaptured(context, attacker, oldTeam);

            if (defender.IsPlayer || context.CanPlayerSee(defender.Pos))
            {
                context.AddEffect(new CapturedEffect(defender));
            }
        }

        private static int CalculateDefenseStrength(IGameObject defender, IRandomization random)
        {
            // Actors can defend anywhere from 100% - 250% their base strength
            return (int)Math.Round(defender.EffectiveDefense * (decimal) random.GetDouble(1, 2.5));
        }

        private static int CalculateAttackStrength(IGameObject attacker, IRandomization random)
        {
            // Actors can attack anywhere from 100% - 150% their base strength
            return (int)Math.Round(attacker.EffectiveStrength * (decimal) random.GetDouble(1, 1.5));
        }

        private static bool DetermineIfAttackHits(CommandContext context, IGameObject attacker, IGameObject defender)
        {
            decimal hitChance = attacker.EffectiveAccuracy;
            decimal evadeChance = defender.EffectiveEvasion;

            int chance = (int) Math.Min(Math.Max(5, Math.Round(evadeChance - hitChance)), 80);

            int roll = (int) Math.Round(context.Randomizer.GetDouble() * 100);

            bool isHit = roll >= chance;
            string verb = isHit ? "hit" : "missed";

            // Only add this message if it occurs somewhere within the player's line of sight or involves the player
            if (attacker.IsPlayer || defender.IsPlayer || context.CanPlayerSee(attacker.Pos) ||
                 context.CanPlayerSee(defender.Pos)) {

                context.AddMessage(
                    $"{attacker.Name} rolled a {roll} and {verb} {defender.Name} ({hitChance} Accuracy vs {evadeChance} Evasion: {chance} needed to hit)",
                    isHit ? ClientMessageType.Math : ClientMessageType.Failure);

            }

            return isHit;
        }
    }
}