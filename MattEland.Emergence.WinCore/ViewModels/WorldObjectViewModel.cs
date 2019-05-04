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

        public int Size => 100;

        public Brush Brush => Brushes.Brown;
        
        public WorldObjectViewModel(Some<WorldObject> source)
        {
            Source = source.Value;
        }
    }
}