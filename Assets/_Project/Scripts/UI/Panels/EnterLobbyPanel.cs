using _Project.Scripts.Core.UI.Panels;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Panels
{
    public class EnterLobbyPanel: BaseUIPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button m_hostLobbyButton;
        [SerializeField] private Button m_enterLobbyButton;

        protected override void Awake()
        {
            base.Awake();
            ShowPanel();
        }

        public override void ShowPanel()
        {
            SubscribeButtonEvents();
            base.ShowPanel();
        }

        public override void HidePanel()
        {
            UnsubscribeButtonEvents();
            base.HidePanel();
        }

        protected override void OnDestroy()
        {
            UnsubscribeButtonEvents();
            base.OnDestroy();
        }

        private void SubscribeButtonEvents()
        {
            m_hostLobbyButton.onClick.AddListener(HostLobby);
            m_enterLobbyButton.onClick.AddListener(EnterLobby);
        }

        private void UnsubscribeButtonEvents()
        {
            m_hostLobbyButton.onClick.RemoveListener(HostLobby);
            m_enterLobbyButton.onClick.RemoveListener(EnterLobby);
        }

        private void HostLobby()
        {
            NetworkManager.singleton.StartHost();
        }

        private void EnterLobby()
        {
            NetworkManager.singleton.StartClient();
        }
    }
}