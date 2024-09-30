using _Project.Scripts.Core.UI.Widgets;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Widgets.PlayerScore
{
    public class PlayerScoreWidget: BaseWidget
    {
        [SerializeField] private TextMeshProUGUI m_playerScoreText;
        [SerializeField] private string m_textFormat = "Score: {0}";

        public override void ShowWidget()
        {
            UpdateScore(0);
            base.ShowWidget();
        }

        public void UpdateScore(int score)
        {
            m_playerScoreText.text = $"{string.Format(m_textFormat, score)}";
        }
    }
}