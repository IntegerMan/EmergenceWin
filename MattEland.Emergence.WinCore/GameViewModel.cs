using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MattEland.Emergence.Domain;

namespace MattEland.Emergence.WinCore
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            GameObjects.Add(new Obstacle(new Position(2, 2), new Health(10) ));
            GameObjects.Add(new Obstacle(new Position(4, 1), new Health(10) ));
        }

        public ObservableCollection<WorldObject> GameObjects { get; } = new ObservableCollection<WorldObject>();
    }
}
