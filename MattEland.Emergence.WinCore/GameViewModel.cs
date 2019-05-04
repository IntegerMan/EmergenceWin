using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MattEland.Emergence.WinCore
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            GameObjects.Add("What");
            GameObjects.Add("the");
            GameObjects.Add("Fez?");
        }

        public ObservableCollection<string> GameObjects { get; } = new ObservableCollection<string>();
    }
}
