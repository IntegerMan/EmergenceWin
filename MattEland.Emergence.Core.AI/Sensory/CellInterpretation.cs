using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace MattEland.Emergence.AI.Sensory
{
    public class CellInterpretation
    {
        [NotNull]
        private readonly IDictionary<CellAspectType, decimal> _aspects = new Dictionary<CellAspectType, decimal>();

        public void AddAspect(CellAspectType id, decimal value)
        {
            _aspects[id] = value;
        }

        public IEnumerable<CellAspect> Aspects => _aspects.Select(a => new CellAspect(a.Key, a.Value));
    }
}