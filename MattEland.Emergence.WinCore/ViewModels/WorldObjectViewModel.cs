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

        [UsedImplicitly] 
        public int X => Source.Position.X * Size; // TODO:  This won't work for view offsets
        
        [UsedImplicitly] 
        public int Y => Source.Position.Y * Size; // TODO:  This won't work for view offsets

        public WorldObjectViewModel(Some<WorldObject> source)
        {
            Source = source.Value;
        }
    }
}