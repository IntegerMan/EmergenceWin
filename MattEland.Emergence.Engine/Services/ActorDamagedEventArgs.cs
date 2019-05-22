using System;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public class ActorDamagedEventArgs : EventArgs
    {
        public ActorDamagedEventArgs(GameObjectBase attacker, GameObjectBase defender, int damage, DamageType damageType)
        {
            Attacker = attacker;
            Defender = defender;
            Damage = damage;
            DamageType = damageType;
        }

        public GameObjectBase Attacker { get; }
        public GameObjectBase Defender { get; }
        public DamageType DamageType { get; }
        public int Damage { get; }

    }
}