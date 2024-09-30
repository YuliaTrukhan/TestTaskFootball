using System;
using UnityEngine;

namespace _Project.Scripts.Gun.Configs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Gun/GunSettingsConfig", fileName = "GunSettingsConfig")]
    public class GunSettingsConfig: ScriptableObject
    {
        [Header("Rotation settings")]
        [SerializeField,Range(0, 300)] private float m_rotationSpeed;
        [SerializeField, Range(0, 180)] private float m_rightRotationLimit;
        [SerializeField, Range(0, 180)] private float m_leftRotationLimit;

        [Header("Shoot settings")]
        [SerializeField] private float m_shootCooldown = 1f;
        [SerializeField] private float m_maxShootPreparedTime = 2f;

        [SerializeField] private float m_minForce;
        [SerializeField] private float m_maxForce;
        
        public float ShootCooldown => m_shootCooldown;
        public float MinForce => m_minForce;
        public float MaxForce => m_maxForce;
        public float MaxShootPreparedTime => m_maxShootPreparedTime;
        public float RotationSpeed => m_rotationSpeed;
        public float RightRotationLimit => m_rightRotationLimit;
        public float LeftRotationLimit => m_leftRotationLimit;
    }
}