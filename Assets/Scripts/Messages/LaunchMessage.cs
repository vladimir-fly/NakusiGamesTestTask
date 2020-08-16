using NGTT.Bases.Headers;

namespace NGTT.Messages
{
    public struct LaunchMessage : IMessage
    {
        public override string ToString() => GetType().Name;
    }
}