using System.Collections.Generic;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Numerics;

namespace MattEland.Emergence.Engine.Vision
{
    public class ShadowCasterViewProvider : IFieldOfViewProvider
    {

        private readonly LevelData _level;
        private readonly HashSet<Pos2D> _visible = new HashSet<Pos2D>();

        public ShadowCasterViewProvider(LevelData level)
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

        private Pos2D SetFoV(int x, int y)
        {
            var pos = new Pos2D(x, y);
            _visible.Add(pos);
            return pos;
        }
    }
}