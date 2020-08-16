
using System.Collections.Generic;
using UnityEngine;
using NGTT.Bases.Headers;

namespace NGTT
{
    public class Dispatcher : MonoBehaviour
    { 
        private List<ITransceiver> _transceivers;
        
        private void Awake()
        {
            _transceivers = new List<ITransceiver>();
        }
        
        public void Register(ITransceiver transceiver)
        {
            transceiver.SendMessage = BroadcastMessage;
            _transceivers.Add(transceiver);
        }

        private void BroadcastMessage(IMessage message)
        {
            Debug.Log($"[Dispatcher][BroadcastMessage] {message}");
            _transceivers.ForEach(transceiver => { transceiver.ReceiveMessage(message) ;});
        }
    }
}