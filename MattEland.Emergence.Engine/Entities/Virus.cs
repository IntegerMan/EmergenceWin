using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
{
    public class Virus : Bug
    {
        public Virus(ActorDto dto) : base(dto)
        {
        }

        public override DamageType AttackDamageType => DamageType.Corruption;

    }
}