using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel(GameMessage msg)
        {
            Message = msg;
        }

        public GameMessage Message { get; }

        public string Text => Message.ToString();
    }
}