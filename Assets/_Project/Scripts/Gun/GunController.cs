using _Project.Scripts.Ball;
using _Project.Scripts.Ball.Data;
using _Project.Scripts.Base.Interfaces;
using _Project.Scripts.Core.UI.Manager;
using _Project.Scripts.Gun.Configs;
using _Project.Scripts.Player;
using _Project.Scripts.UI.Panels;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Gun
{
    public class GunController : BaseEntityController, IInitializable
    {
        [Header("Settings")] 
        [SerializeField] private GunSettingsConfig m_settingsConfig;

        [Space] 
        [Header("References")] 
        [SerializeField] private Transform m_rotationRoot;
        [SerializeField] private Transform m_shootSpawnPoint;

        [SyncVar] private float m_lastCooldownTime = 0f;
        [SyncVar] private float m_startShootTime;
        [SyncVar] private bool m_initialized = false;

        private bool IsCooldownCompleted => Time.time - m_lastCooldownTime >= m_settingsConfig.ShootCooldown;

        public void Initialize()
        {
            m_initialized = true;
        }
        
        public void SetAim(Vector3 aimPoint)
        {
            if (m_initialized == false)
            {
                return;
            }
            
            Vector3 targetPositionInLocalSpace = transform.InverseTransformPoint(aimPoint);
            targetPositionInLocalSpace.y = 0.0f;
            
            Vector3 limitedRotation = targetPositionInLocalSpace.x >= 0.0f 
                ? Vector3.RotateTowards(Vector3.forward, targetPositionInLocalSpace, Mathf.Deg2Rad * m_settingsConfig.RightRotationLimit, float.MaxValue) 
                : Vector3.RotateTowards(Vector3.forward, targetPositionInLocalSpace, Mathf.Deg2Rad * m_settingsConfig.LeftRotationLimit, float.MaxValue);

           
            Quaternion whereToRotate = Quaternion.LookRotation(limitedRotation);
        
            m_rotationRoot.localRotation = Quaternion.RotateTowards(m_rotationRoot.localRotation, whereToRotate, m_settingsConfig.RotationSpeed);
        }
        
        public void StartShootPrepare()
        {
            if (m_initialized == false)
            {
                return;
            }

            CmdStartShootPrepare();
            GetGamePanel().PlayerForceWidget.StartForce(m_settingsConfig.MaxShootPreparedTime);
        }
        
        public void Shoot()
        {
            if (m_initialized == false)
            {
                return;
            }
            
            if (isLocalPlayer == false)
            {
                return;
            }

            GetGamePanel().PlayerForceWidget.StopForce();
            CmdShoot();
        }

        [Command]
        private void CmdStartShootPrepare()
        {
            m_startShootTime = Time.time;
        }

        [Command]
        private void CmdShoot()
        {
            if (IsCooldownCompleted == false)
            {
                return;
            }
            
            m_lastCooldownTime = Time.time;
            
            Color color = netIdentity.GetComponent<PlayerController>().Color;
            var ball = BallManager.Instance.CreateBall(m_shootSpawnPoint.position, Quaternion.identity);
            
            ball.ResetBall();
            ball.SetOwnerId(netId);
            ball.RpcChangeColor(color);
            ball.Throw(new BallThrowData(m_shootSpawnPoint.forward, GetShootForceValue()));
        }

        private void Update()
        {
            UpdateCooldownView();
        }

        private float GetShootForceValue()
        {
            var shotPreparedTime = Time.time - m_startShootTime;
            var normalizedTime = Mathf.Lerp(0, m_settingsConfig.MaxShootPreparedTime, shotPreparedTime);

            return Mathf.Lerp(m_settingsConfig.MinForce, m_settingsConfig.MaxForce, normalizedTime);
        }

        private void UpdateCooldownView()
        {
            if (isLocalPlayer)
            {
                float currentCooldown = Time.time - m_lastCooldownTime;
                float normalizedValue = 1f - currentCooldown / m_settingsConfig.ShootCooldown;
                
                GetGamePanel()?.PlayerCooldownWidget?.UpdateCooldown(normalizedValue);
            }
        }
        
        private GamePanel GetGamePanel() => UIManager.Instance.TryGetPanel<GamePanel>();
    }
}
