

using UnityEngine;

namespace _Project.Scripts.UI.Widgets.PlayerScore
{
    public class PlayerForceWidget: SliderWidget
    {
        private bool m_isStarted = false;
        private float m_currentValue;
        private float m_maxValue;

        protected override void Awake()
        {
            base.Awake();
            UpdateCooldown(0f);
        }

        public void StartForce(float maxForce)
        {
            m_currentValue = 0f;
            m_maxValue = maxForce;
            m_isStarted = true;
        }

        public void StopForce()
        {
            m_isStarted = false;
            m_currentValue = 0f;

            UpdateCooldown(0f);
        }

        private void Update()
        {
            AnimateForce();
        }

        private void AnimateForce()
        {
            if (m_isStarted == false)
            {
                return;
            }

            m_currentValue += Time.deltaTime;
            UpdateCooldown(m_currentValue / m_maxValue);
            
            if (m_currentValue >= m_maxValue)
            {
                StopForce();
            }
        }
    }
}