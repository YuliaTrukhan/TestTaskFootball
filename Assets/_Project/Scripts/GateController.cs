using _Project.Scripts.Ball;
using _Project.Scripts.Base.Interfaces;
using _Project.Scripts.Score;
using Mirror;
using UnityEngine;

namespace _Project.Scripts
{
    public class GateController : BaseEntityController, IInitializable
    {
        [SerializeField] private Rigidbody m_rigidbody;
        
        [Header("Move Settings")]
        [SerializeField] private float m_moveSpeed = 5f;
        [SerializeField] private float m_movementRange = 10f;
        
        [SyncVar] private bool m_initialized = false;

        private Vector3 m_startPosition;

        public void Initialize()
        {
            m_startPosition = transform.position;
            m_initialized = true;
        }

        private void Update()
        {
            if (m_initialized == false)
            {
                return;
            }
            
            if (isLocalPlayer == false)
            {
                return;
            }
            
            Move();
        }

        private void Move()
        {
            float xPosition = Mathf.PingPong(Time.time * m_moveSpeed, m_movementRange) - m_movementRange / 2f;
            Vector3 nextPosition = m_startPosition + transform.right * xPosition;

            m_rigidbody.position = nextPosition;
        }

        [ServerCallback]
        private void OnCollisionEnter(Collision other)
        {
            if (isServer == false)
            {
                return;
            }
            
            var ball = other.transform.GetComponent<BallController>();
            if (ball != null && ball.IsHit == false)
            {
                uint ballOwnerId = ball.OwnerId;
                uint gateOwnerId = netId;

                if (ballOwnerId == gateOwnerId)
                {
                    return;
                }

                ScoreController.Instance.AddScore(ballOwnerId, 1);
                ScoreController.Instance.AddScore(gateOwnerId, -1);
                
                ball.BallHited();
            }
        }
    }
}
