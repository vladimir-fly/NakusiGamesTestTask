using System;
using NGTT.Bases.Headers;

using UnityEngine;

namespace NGTT.Bases.Abstracts
{
    public abstract class SystemBase : MonoBehaviour, ITransceiver
    {
        public Action<IMessage> SendMessage { get; set; }
        public virtual void ReceiveMessage(IMessage message) { }
        
        public abstract void Launch();
        public abstract void Halt();

    }
}