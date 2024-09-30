using _Project.Scripts.ColorSelection;
using Mirror;
using UnityEngine;

namespace _Project.Scripts
{
    public class BaseEntityController: NetworkBehaviour
    {
        [Header("Components")]
        [SerializeField] private ColorChangerComponent m_colorChangerComponent;
        
        public void ChangeColor(Color color)
        {
            if (m_colorChangerComponent == null)
            {
                return;
            }
            
            m_colorChangerComponent.ChangeColor(color);
        }

        [ClientRpc]
        public void RpcChangeColor(Color color) => ChangeColor(color);
    }
}