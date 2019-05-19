namespace MattEland.Emergence.Definitions.DTOs
{
    public class ClientMessage
    {
        public ClientMessage(string message, ClientMessageType messageType)
        {
            Message = message;
            MessageType = messageType;
        }

        public string Message { get; set; }
        public ClientMessageType MessageType { get; set; }
    }
}