using UnityEngine;

namespace _Project.Scripts.Zones
{
    public class PlayerZonesContainer: MonoBehaviour
    {
        [SerializeField] private PlayerZone[] m_allPlayerZones;

        public PlayerZone[] AllPlayerZones => m_allPlayerZones;
    }
}