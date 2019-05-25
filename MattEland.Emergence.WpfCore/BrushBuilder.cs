using System.Collections.Generic;
using System.Windows.Media;

namespace MattEland.Emergence.WpfCore
{
    public static class BrushBuilder
    {
        private static readonly IDictionary<string, Brush> Brushes = new Dictionary<string, Brush>();
        
        public static Brush BuildBrush(this string hexColor)
        {
            hexColor = hexColor.ToUpperInvariant();
            
            if (Brushes.ContainsKey(hexColor))
            {
                return Brushes[hexColor];
            }
            
            // TODO: This could be cached and shared
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexColor));
            brush.Freeze();

            Brushes[hexColor] = brush;
            
            return brush;
        }
        
    }
}