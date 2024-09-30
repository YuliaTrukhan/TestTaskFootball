using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Score
{
    public class ScoreController : MonoBehaviour
    {
        private List<IScoreObserver> m_scoreObserversList = new();
        
        public static ScoreController Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            { 
                Destroy(gameObject); 
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public void AddObserver(IScoreObserver observer)
        {
            if (m_scoreObserversList.Contains(observer) == false)
            {
                m_scoreObserversList.Add(observer);
            }
        }
        
        public void RemoveObserver(IScoreObserver observer)
        {
            if (m_scoreObserversList.Contains(observer))
            {
                m_scoreObserversList.Remove(observer);
            }
        }
        
        public void AddScore(uint playerId, int score = 1)
        {
            m_scoreObserversList.ForEach(o => o.UpdateScore(playerId, score));
        }
    }
}
