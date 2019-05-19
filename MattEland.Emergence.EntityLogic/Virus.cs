using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.EntityLogic
{
    public class Virus : Bug
    {
        public Virus(ActorDto dto) : base(dto)
        {
        }

        public override DamageType AttackDamageType => DamageType.Corruption;

    }
}