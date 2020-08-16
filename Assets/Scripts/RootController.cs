using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

using NGTT.Bases.Headers;
using NGTT.Bases.Enums;
using NGTT.Helpers;
using NGTT.Managers;
using NGTT.Messages;

namespace NGTT.Controllers
{
    public class RootController : MonoBehaviour, ITransceiver
    {
        public ERootState State;
        [SerializeField] private UIManager _uiManager;
        public Action<IMessage> SendMessage { get; set; }
        private void Start()
        {
            State = ERootState.Idle;
            _uiManager = FindObjectOfType<UIManager>();
            Assert.IsNotNull(_uiManager, LogExtension.BoldRed(Defaults.UIManagerNotFound));
            Assert.IsNotNull(_uiManager.LaunchButton, LogExtension.BoldRed(Defaults.LaunchButtonIsNotSet));
            Assert.IsNotNull(_uiManager.HaltButton, LogExtension.BoldRed(Defaults.HaltButtonIsNotSet));
            
            _uiManager.LaunchButton.onClick.AddListener(LaunchAction());
            _uiManager.PauseButton.onClick.AddListener(PauseAction());
            _uiManager.HaltButton.onClick.AddListener(HaltAction());

            Debug.Log(LogExtension.Bold("[RootController][Start] Complete"));
        }

        private UnityAction HaltAction() =>
            () =>
            {
                if (State == ERootState.Idle) return;
                State = ERootState.Idle;
                SendMessage?.Invoke(new HaltMessage());
            };

        private UnityAction PauseAction() => 
            () => Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        private UnityAction LaunchAction() =>
            () =>
            {
                if (State != ERootState.Idle) return;
                State = ERootState.Working;
                SendMessage?.Invoke(new LaunchMessage());
            };

        public void ReceiveMessage(IMessage message)
        {
            switch (message)
            {
                case UnitDataUpdateMessage unitDataUpdateMessage:
                    _uiManager.Log.text += $"{unitDataUpdateMessage.Message} \n";
                    break;
            }
        }
    }
}
