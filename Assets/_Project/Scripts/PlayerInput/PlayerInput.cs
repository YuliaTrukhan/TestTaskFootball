using System;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.PlayerInput
{
    public class PlayerInput: NetworkBehaviour
    {
        public event Action<Vector2> OnMouseMovement = null;
        public event Action OnShootPrepared = null;
        public event Action OnShootFired = null;
        public event Action OnSettingsFired = null;

        [SerializeField, Range(1, 40)] private float m_mouseSensivityX = 1f;
        [SerializeField, Range(1, 40)] private float m_mouseSensivityY = 1f;
        [SerializeField] private KeyCode m_shootButton = KeyCode.Space;
        [SerializeField] private KeyCode m_settingsButton = KeyCode.Escape;

        private void Update()
        {
            if (isLocalPlayer)
            {
                CheckMouseMovement();
                CheckShootInput();
                CheckSettingsInput();
            }
        }

        private void CheckMouseMovement()
        {
            float horizontal = Input.GetAxis("Mouse X") * m_mouseSensivityX;
            float vertical = - Input.GetAxis("Mouse Y")  * m_mouseSensivityY;
            Vector2 movement = new Vector2(horizontal, vertical);
            
            OnMouseMovement?.Invoke(movement);
        }
        
        private void CheckShootInput()
        {
            if (Input.GetKeyDown(m_shootButton))
            {
                OnShootPrepared?.Invoke();
            }
            
            if (Input.GetKeyUp(m_shootButton))
            {
                OnShootFired?.Invoke();
            }
        }

        private void CheckSettingsInput()
        {
            if (Input.GetKeyUp(m_settingsButton))
            {
                OnSettingsFired?.Invoke();
            }
        }
    }
}