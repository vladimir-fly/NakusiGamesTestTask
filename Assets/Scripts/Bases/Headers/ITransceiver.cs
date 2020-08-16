using System;

namespace NGTT.Bases.Headers
{
    public interface ITransceiver
    {
        void ReceiveMessage(IMessage message);
        Action<IMessage> SendMessage { get; set; }
    }
}