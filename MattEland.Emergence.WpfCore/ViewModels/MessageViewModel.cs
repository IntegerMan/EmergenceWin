using MattEland.Emergence.Definitions.Model.Messages;

namespace MattEland.Emergence.WpfCore.ViewModels
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