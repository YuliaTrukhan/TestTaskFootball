using System;
using _Project.Scripts.Core.Pool;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Ball
{
    public class BallManager: MonoBehaviour
    {
        [SerializeField] private string m_poolObjectId = "Ball";
        public static BallManager Instance { get; private set; }

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

        public BallController CreateBall(Vector3 position, Quaternion rotation)
        {
            var ball = PoolManager.Instance.Get(m_poolObjectId, position, rotation).GetComponent<BallController>();

            if (ball != null)
            {
                NetworkServer.Spawn(ball.gameObject);
            }

            return ball;
        }
    }
}