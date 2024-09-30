using _Project.Scripts.Core.UI.Manager;
using _Project.Scripts.Score;
using _Project.Scripts.UI.Panels;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerScore: NetworkBehaviour, IScoreObserver
    {
        [SyncVar(hook = nameof(OnScoreUpdated)), SerializeField] private int m_curentScore = 0;
        [SerializeField] private TextMeshPro m_scoreText;

        private void Awake()
        {
            ScoreController.Instance.AddObserver(this);
        }

        private void Start()
        {
            if (isLocalPlayer)
            {
                m_scoreText.enabled = false;
            }
            
            m_curentScore = 0;
            m_scoreText.text = $"{m_curentScore}";
        }

        private void OnDestroy()
        {
            ScoreController.Instance.RemoveObserver(this);
        }

        public void UpdateScore(uint unetId, int score = 1)
        {
            if (unetId != netId)
            {
                return;
            }

            m_curentScore = Mathf.Max(m_curentScore + score , 0);
        }
        
        private void OnScoreUpdated(int oldScore, int newScore)
        {
            m_scoreText.text = $"{newScore}";
            
            if (isLocalPlayer)
            {
                GamePanel gamePanel = UIManager.Instance.TryGetPanel<GamePanel>();
                gamePanel?.PlayerScoreWidget?.UpdateScore(m_curentScore);
            }
        }
    }
}