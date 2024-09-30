using System.Collections;
using _Project.Scripts.Ball.Configs;
using _Project.Scripts.Ball.Data;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Ball
{
    public class BallController: BaseEntityController
    {
        [Header("Settings")] 
        [SerializeField] private BallSettingsConfig m_settingsConfig;
        
        [Header("References")] 
        [SerializeField] private Rigidbody m_rigidbody;

        [SyncVar] private uint m_ownerId;
        [SyncVar] private bool m_ballHitted = false;

        public uint OwnerId => m_ownerId;
        public bool IsHit => m_ballHitted;

        public void SetOwnerId(uint ownerId)
        {
            m_ownerId = ownerId;
        }

        public void Throw(BallThrowData data)
        {
            Debug.Log($"Throw with strength {data.ThrowStrength}");
            
            m_ballHitted = false;

            m_rigidbody.AddForce(data.Direction * data.ThrowStrength, ForceMode.Impulse);
            m_rigidbody.AddTorque(Vector3.up * m_settingsConfig.SpinForce);

            StartCoroutine(DestroyWithDelay(m_settingsConfig.Lifetime));
        }
        
        public void BallHited()
        {
            m_ballHitted = true;
        }
        
        public void ResetBall()
        {
            m_rigidbody.isKinematic = false;
            m_rigidbody.velocity = Vector3.zero;
            m_rigidbody.angularVelocity = Vector3.zero;
            m_ballHitted = false;

            gameObject.SetActive(true);
            
            RpcResetBall();
        }

        [ClientRpc]
        private void RpcResetBall()
        {
            gameObject.SetActive(true);
        }
        
        private void FixedUpdate()
        {
            if (isLocalPlayer)
            {
                return;
            }
            
            CheckMaxSpeed();
        }

        private void CheckMaxSpeed()
        {
            if (m_rigidbody.velocity.magnitude > m_settingsConfig.MaxSpeed)
            {
                m_rigidbody.velocity = m_rigidbody.velocity.normalized * m_settingsConfig.MaxSpeed;
            }
        }

        private IEnumerator DestroyWithDelay(float destroyDelay)
        {
            yield return new WaitForSecondsRealtime(destroyDelay);
            ReturnToPool();
        }
        
        private void ReturnToPool()
        {
            m_rigidbody.isKinematic = true;
            gameObject.SetActive(false);
            
            NetworkServer.UnSpawn(gameObject);
        }
    }
}