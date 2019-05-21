using System;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public class ActorDamagedEventArgs : EventArgs
    {
        public ActorDamagedEventArgs(IGameObject attacker, IGameObject defender, int damage, DamageType damageType)
        {
            Attacker = attacker;
            Defender = defender;
            Damage = damage;
            DamageType = damageType;
        }

        public IGameObject Attacker { get; }
        public IGameObject Defender { get; }
        public DamageType DamageType { get; }
        public int Damage { get; }

    }
}