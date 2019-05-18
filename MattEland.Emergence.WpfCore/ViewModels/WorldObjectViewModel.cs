using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using JetBrains.Annotations;
using LanguageExt;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;
using MattEland.Emergence.Model.Messages;
using MattEland.Emergence.WpfCore;

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

        public WorldObjectViewModel(Some<WorldObject> source, Some<GameViewModel> gameViewModel)
        {
            _gameVM = gameViewModel;
            Source = source.Value;
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
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdatePosition(Position newPos)
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
        }
    }
}