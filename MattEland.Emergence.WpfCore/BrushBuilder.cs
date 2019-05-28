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

        public static Brush GetBrushForHexColor([NotNull] this string hexColor)
        {
            if (hexColor == null) throw new ArgumentNullException(nameof(hexColor));

            hexColor = hexColor.ToUpperInvariant();

            if (CachedBrushes.ContainsKey(hexColor))
            {
                return CachedBrushes[hexColor];
            }

            var color = GetColorFromHexColor(hexColor);

            var brush = BuildBrushForColor(color);

            CachedBrushes[hexColor] = brush;

            return brush;
        }

        private static SolidColorBrush BuildBrushForColor(Color color)
        {
            SolidColorBrush brush;

            if (color.A == 0)
            {
                brush = Brushes.Transparent;
            }
            else
            {
                brush = new SolidColorBrush(color);
                brush.Freeze();
            }

            return brush;
        }

        private static Color GetColorFromHexColor(string hexColor) 
            => (Color?) ColorConverter.ConvertFromString(hexColor) ?? Colors.Transparent;
    }
}