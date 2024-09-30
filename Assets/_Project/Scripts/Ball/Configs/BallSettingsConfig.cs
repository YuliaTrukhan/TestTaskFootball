using System;
using UnityEngine;

namespace _Project.Scripts.Ball.Configs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Ball/BallSettingsConfig", fileName = "BallSettingsConfig")]
    public class BallSettingsConfig: ScriptableObject
    {
        [SerializeField] private float m_lifetime = 5f;
        [SerializeField] private float m_maxSpeed = 15f;
        [SerializeField] private float m_spinForce = 5f;

        public float Lifetime => m_lifetime;
        public float MaxSpeed => m_maxSpeed;
        public float SpinForce => m_spinForce;
    }
}