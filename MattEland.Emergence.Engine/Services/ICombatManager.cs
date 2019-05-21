using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public interface ICombatManager
    {
        /// <summary>
        /// Handles the details for a direct attack from an <paramref name="attacker"/> on a <paramref name="defender"/>.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="attacker">The attacker.</param>
        /// <param name="defender">The defender.</param>
        /// <param name="verb">The verb indicating the type of command used.</param>
        void HandleAttack(CommandContext context, IGameObject attacker, IGameObject defender, string verb, DamageType damageType);

        /// <summary>
        /// Hurts the <paramref name="defender"/> for <paramref name="damage"/> damage and outputs necessary events and messages.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="defender">The defender.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="attacker">The attacker.</param>
        /// <param name="verb">The verb being executed.</param>
        /// <returns>A string describing the action, for display in the user interface</returns>
        string HurtObject(CommandContext context, IGameObject defender, int damage, IGameObject attacker, string verb, DamageType damageType);

        void HandleExplosion(CommandContext context, IGameObject executor, Pos2D epicenter, int strength, int radius, DamageType damageType);

        /// <summary>
        /// Handles the capture of another actor.
        /// </summary>
        /// <param name="context">The command context used to interact with the game world.</param>
        /// <param name="defender">The object being captured.</param>
        /// <param name="attacker">The actor doing the capturing</param>
        /// <returns>A message indicating the capture result.</returns>
        void HandleCapture(CommandContext context, IGameObject defender, IGameObject attacker);
    }
}