using System.Collections.Generic;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Messages;

namespace MattEland.Emergence.Engine.Game
{
    public class VisibleCellsMessage : GameMessage
    {
        public ISet<Pos2D> Cells { get; }

        public VisibleCellsMessage(ISet<Pos2D> cells)
        {
            Cells = cells;
        }

        public override string ToString() => $"{Cells.Count} visible cells";
    }
}