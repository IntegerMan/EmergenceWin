using System;
using System.Collections.Generic;
using System.Windows.Media;
using JetBrains.Annotations;

namespace MattEland.Emergence.WpfCore
{
    public static class BrushBuilder
    {
        [NotNull]
        private static readonly IDictionary<string, Brush> CachedBrushes = new Dictionary<string, Brush>();
        
        public static Brush BuildBrush([NotNull] this string hexColor)
        {
            if (hexColor == null) throw new ArgumentNullException(nameof(hexColor));

            hexColor = hexColor.ToUpperInvariant();
            
            if (CachedBrushes.ContainsKey(hexColor))
            {
                return CachedBrushes[hexColor];
            }

            var color = ColorConverter.ConvertFromString(hexColor);

            if (color == null)
            {
                return Brushes.Transparent;
            }

            var brush = new SolidColorBrush((Color) color);
            brush.Freeze();

            CachedBrushes[hexColor] = brush;

            return brush;
        }
        
    }
}