using _Project.Scripts.Core.Pool;
using _Project.Scripts.Core.UI.Manager;
using _Project.Scripts.Network.Messages;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Data;
using _Project.Scripts.UI.Panels;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Network
{
    public class CustomNetworkManager: NetworkManager
    {
        [Header("Custom")] 
        [SerializeField] private PlayerController m_playerControllerPrefab;
        
        public override void OnStartServer()
        {
            Debug.Log("OnStartServer");
            
            base.OnStartServer();
            
            NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter);
        }

        public override void OnStopServer()
        {
            Debug.Log("OnStopServer");
            
            base.OnStopServer();
        }

        public override void OnClientDisconnect()
        {
            base.OnClientDisconnect();
            
            UIManager.Instance.HidePanel<GamePanel>();
            UIManager.Instance.ShowPanel<EnterLobbyPanel>();
            
            Debug.Log("OnClientDisconnect");
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            
            UIManager.Instance.HidePanel<EnterLobbyPanel>();
            UIManager.Instance.ShowPanel<SelectColorPanel>();
            
            Debug.Log("OnClientConnect");
        }

        private void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
        {
            Debug.Log("OnCreateCharacter");
            
            Transform startPoint = GetStartPosition();
            PlayerController player = Instantiate(m_playerControllerPrefab, startPoint.position, startPoint.rotation);
            
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
            
            player.SetPlayerData(new PlayerData()
            {
                ColorItemData = message.ColorItemData
            });
        }
    }
}