using _Project.Scripts.Core.UI.Panels;
using _Project.Scripts.UI.Widgets.PlayerScore;
using UnityEngine;

namespace _Project.Scripts.UI.Panels
{
    public class GamePanel: BaseUIPanel
    {
        [SerializeField] private PlayerScoreWidget m_playerScoreWidget;
        [SerializeField] private SliderWidget m_playerCooldownWidget;
        [SerializeField] private PlayerForceWidget m_playerForceWidget;

        public PlayerScoreWidget PlayerScoreWidget => m_playerScoreWidget;
        public SliderWidget PlayerCooldownWidget => m_playerCooldownWidget;
        public PlayerForceWidget PlayerForceWidget => m_playerForceWidget;
    }
}