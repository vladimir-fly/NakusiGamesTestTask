using NGTT.Bases.Headers;

namespace NGTT.Messages
{
    public struct HaltMessage : IMessage
    {
        public override string ToString() => GetType().Name;
    }
}