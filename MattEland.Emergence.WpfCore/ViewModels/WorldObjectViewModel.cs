using System;
using System.Diagnostics;
using System.Windows.Media;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Messages;
using MattEland.Shared.WPF;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    [DebuggerDisplay("{Source.GetType().Name} at ({Source.Pos.X}, {Source.Pos.Y}) rendering at ({X}, {Y})")]
    public class WorldObjectViewModel : ViewModelBase
    {
        private readonly GameViewModel _gameVm;

        public GameObjectBase Source { get; private set; }

        public int Size => 24;

        public Brush ForegroundBrush => Source.ForegroundColor.GetBrushForHexColor();
        
        public Brush BackgroundBrush => Source.BackgroundColor.GetBrushForHexColor();

        public string Content => Source.AsciiChar.ToString();

        public int X => (Source.Pos.X + _gameVm.XOffset) * Size;
        
        public int Y => (Source.Pos.Y + _gameVm.YOffset) * Size;

        public int ZIndex => Source.ZIndex;

        public Guid Id => Source.Id;

        public WorldObjectViewModel(GameObjectBase source, GameViewModel gameViewModel)
        {
            _gameVm = gameViewModel;
            Source = source;
        }

        public void UpdateFrom(ObjectUpdatedMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Source = message.Source;

            // Notify all properties changed
            OnPropertyChanged(string.Empty);
        }

        public void UpdatePosition(Pos2D newPos)
        {
            Source.Pos = newPos;

            NotifyOffsetChanged();

            if (Source is Player)
            {
                _gameVm.CenterOn(newPos);
            }
        }

        public void NotifyOffsetChanged()
        {
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
            OnPropertyChanged(nameof(Tag));
        }

        public string Tag => $"{Content} {Source.Pos}";

        public override string ToString() => Tag;
    }
}