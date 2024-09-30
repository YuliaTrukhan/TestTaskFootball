using _Project.Scripts.Core.UI.Manager;
using _Project.Scripts.Core.UI.Panels;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Panels
{
    public class SettingsPanel: BaseUIPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button m_exitButton;
        [SerializeField] private Button m_closePanelButton;

        public override void ShowPanel()
        {
            m_exitButton.onClick.AddListener(OnExitLobby);
            m_closePanelButton.onClick.AddListener(OnClosePanel);
            base.ShowPanel();
        }

        public override void HidePanel()
        {
            m_exitButton.onClick.RemoveListener(OnExitLobby);
            m_closePanelButton.onClick.RemoveListener(OnClosePanel);
            base.HidePanel();  
        }

        private void OnExitLobby()
        {
            NetworkManager.singleton.StopHost();
            OnClosePanel();
        }

        private void OnClosePanel()
        {
            UIManager.Instance.HidePanel<SettingsPanel>();
        }
    }
}