using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Entities;
using MattEland.Emergence.Definitions.Model.Messages;
using MattEland.Emergence.WpfCore;
using MattEland.Emergence.WpfCore.ViewModels;
using ActorType = MattEland.Emergence.Definitions.Model.Entities.ActorType;

namespace MattEland.Emergence.WinCore.ViewModels
{
    [DebuggerDisplay("{Source.GetType().Name} at ({Source.Pos.X}, {Source.Pos.Y}) rendering at ({X}, {Y})")]
    public class WorldObjectViewModel : INotifyPropertyChanged
    {
        [NotNull]
        private readonly GameViewModel _gameVM;

        [NotNull, UsedImplicitly]
        public WorldObject Source { get; private set; }

        public int Size => 24;

        public Brush ForegroundBrush => Source.ForegroundColor.BuildBrush();
        
        public Brush BackgroundBrush => Source.BackgroundColor.BuildBrush();

        public string Content => Source.AsciiChar.ToString();

        [UsedImplicitly] 
        public int X => (Source.Pos.X + _gameVM.XOffset) * Size;
        
        [UsedImplicitly] 
        public int Y => (Source.Pos.Y + _gameVM.YOffset) * Size;

        [UsedImplicitly]
        public int ZIndex => Source.ZIndex;

        public Guid Id => Source.Id;

        public WorldObjectViewModel(WorldObject source, GameViewModel gameViewModel)
        {
            _gameVM = gameViewModel;
            Source = source;
        }

        public void UpdateFrom([NotNull] ObjectUpdatedMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Source = message.Source;

            // Notify all properties changed
            OnPropertyChanged(string.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void UpdatePosition(Pos2D newPos)
        {
            Source.Pos = newPos;

            NotifyOffsetChanged();

            if (Source is Actor a && a.ActorType == ActorType.Player)
            {
                _gameVM.CenterOn(newPos);
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