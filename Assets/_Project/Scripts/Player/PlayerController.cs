using _Project.Scripts.Core.UI.Manager;
using _Project.Scripts.Core.UI.Panels;
using _Project.Scripts.Gun;
using _Project.Scripts.Player.Data;
using _Project.Scripts.UI.Panels;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private GunController m_ownedGun;
        [SerializeField] private GateController m_ownedGate;
        [SerializeField] private CameraController m_ownedCamera;
        [SerializeField] private PlayerInput.PlayerInput m_playerInput;

        [SyncVar(hook = nameof(HookSetPlayerData))] private PlayerData m_playerData;

        public PlayerData PlayerData => m_playerData;
        public Color Color => PlayerData.ColorItemData.Color;
        
        public void SetPlayerData(PlayerData playerData)
        {
            m_playerData = playerData; 
        }
        
        private void HookSetPlayerData(PlayerData oldData, PlayerData newData)
        {
            Color color = newData.ColorItemData.Color;
            
            m_ownedGun.ChangeColor(color);
            m_ownedGate.ChangeColor(color);
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            
            UIManager.Instance.HidePanel<SelectColorPanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
            
            Cursor.lockState = CursorLockMode.Locked;    
            
            m_ownedGun.Initialize();
            m_ownedGate.Initialize();
            
            SubscribeEvents();
        }

        public override void OnStopLocalPlayer()
        {
            UnsubscribeEvents();
            
            Cursor.lockState = CursorLockMode.None;    
            UIManager.Instance.OnPanelHide -= OnPanelHide;
            
            base.OnStopLocalPlayer();
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            m_playerInput.OnShootPrepared += EventShootPrepared;
            m_playerInput.OnShootFired += EventShootFired;
            m_playerInput.OnMouseMovement += EventMouseMovement;
            m_playerInput.OnSettingsFired += EventSettingsFired;
        }

        private void UnsubscribeEvents()
        {
            m_playerInput.OnShootPrepared -= EventShootPrepared;
            m_playerInput.OnShootFired -= EventShootFired;
            m_playerInput.OnMouseMovement -= EventMouseMovement;
            m_playerInput.OnSettingsFired -= EventSettingsFired;
        }

        private void EventMouseMovement(Vector2 mouseMovement)
        {
            m_ownedCamera.Rotate(mouseMovement);
            m_ownedGun.SetAim(m_ownedCamera.Forward * 200f);
        }

        private void EventShootPrepared()
        {
            m_ownedGun.StartShootPrepare();
        }
        
        private void EventShootFired()
        {
            m_ownedGun.Shoot();
        }

        private void EventSettingsFired()
        {
            UnsubscribeEvents();
            Cursor.lockState = CursorLockMode.None;  
            
            UIManager.Instance.OnPanelHide += OnPanelHide;
            UIManager.Instance.ShowPanel<SettingsPanel>();
        }

        private void OnPanelHide(BaseUIPanel uiPanel)
        {
            if (uiPanel is SettingsPanel)
            {
                UIManager.Instance.OnPanelHide -= OnPanelHide;
                Cursor.lockState = CursorLockMode.Locked;     
                
                SubscribeEvents();
            }
        }
    }
}
