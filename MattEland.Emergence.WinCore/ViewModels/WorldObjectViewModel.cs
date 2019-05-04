using System.Windows.Media;
using JetBrains.Annotations;
using LanguageExt;
using MattEland.Emergence.Domain;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class WorldObjectViewModel
    {
        [NotNull, UsedImplicitly]
        public WorldObject Source { get; }

        public int Size => 24;

        public Brush Brush => Brushes.Gray;

        public int X => Source.Position.x * Size; // TODO:  This won't work for view offsets
        public int Y => Source.Position.y * Size; // TODO:  This won't work for view offsets

        public WorldObjectViewModel(Some<WorldObject> source)
        {
            Source = source.Value;
        }
    }
}