namespace MattEland.Emergence.Engine.DTOs
{
    public sealed class EffectDto
    {
        public EffectType Effect { get; set; }
        public string StartPos { get; set; }
        public string EndPos { get; set; }
        public string Text { get; set; }
        public string Data { get; set; }
    }
}