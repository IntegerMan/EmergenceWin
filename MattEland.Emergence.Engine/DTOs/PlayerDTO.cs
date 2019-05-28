using System.Collections.Generic;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.DTOs
{
    public class PlayerDto : ActorDto
    {
        public PlayerDto() : base(ActorType.Player)
        {
        }

        public List<CommandInfoDto> Hotbar { get; set; }
        public List<CommandInfoDto> StoredCommands { get; set; }
    }
}