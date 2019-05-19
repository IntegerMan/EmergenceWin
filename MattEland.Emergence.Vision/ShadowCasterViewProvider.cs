using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using MattEland.Shared.Numerics;

namespace MattEland.Emergence.Vision
{
    public class ShadowCasterViewProvider : IFieldOfViewProvider
    {

        private readonly ILevel _level;
        private readonly HashSet<Pos2D> _visible = new HashSet<Pos2D>();

        public ShadowCasterViewProvider(ILevel level)
        {
            _level = level;
        }

        public bool IsInFov(Pos2D pos) => _visible.Contains(pos);

        public ISet<Pos2D> VisiblePositions => _visible;

        public ISet<Pos2D> ComputeFov(Pos2D origin, decimal radius)
        {
            _visible.Clear();
            ShadowCaster.ComputeFieldOfViewWithShadowCasting(origin.X, origin.Y, radius.ToInt(), IsOpaque, SetFoV);

            return _visible;
        }

        private bool IsOpaque(int x, int y) => _level.HasSightBlocker(new Pos2D(x, y));

        private void SetFoV(int x, int y)
        {
            _visible.Add(new Pos2D(x, y));
        }
    }
}