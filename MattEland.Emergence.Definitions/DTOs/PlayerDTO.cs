using System.Collections.Generic;

namespace MattEland.Emergence.Definitions.DTOs
{
    public class PlayerDto : ActorDto
    {
        public List<CommandInfoDto> Hotbar { get; set; }
        public List<CommandInfoDto> StoredCommands { get; set; }
    }
}