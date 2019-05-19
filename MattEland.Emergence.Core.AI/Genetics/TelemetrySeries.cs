using System.Collections.Generic;

namespace MattEland.Emergence.AI.Genetics
{
    public class TelemetrySeries
    {
        public string Title { get; set; }
        public IList<decimal> Values { get; } = new List<decimal>();
    }
}