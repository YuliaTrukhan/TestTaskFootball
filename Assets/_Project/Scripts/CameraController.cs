using Mirror;
using UnityEngine;

namespace _Project.Scripts
{
    public class CameraController : NetworkBehaviour
    {
        [SerializeField, Range(0.1f, 5f)] private float smoothTime = 3f; //A
        
        [Header("Horizontal rotation settings")]
        [SerializeField, Range(0, 360)] private float m_rightRotationLimit = 30f;
        [SerializeField, Range(0, 360)] private float m_leftRotationLimit = 30f;
        
        [Header("Vertical rotation settings")]
        [SerializeField, Range(0, 180)] private float upwardRotationLimit;
        [SerializeField, Range(0, 180)] private float downwardRotationLimit;    
        
        private float rotationX;
        private float rotationY;
        private Vector3 currentRotation;
        private Vector3 velocity;

        public Vector3 Forward => transform.forward;

        private void Start()
        {
            if (isLocalPlayer == false)
            {
                gameObject.SetActive(false);
            }
        }

        public void Rotate(Vector2 mouseMovement)
        {
            if (isLocalPlayer == false)
            {
                return;
            }
            
            rotationX += mouseMovement.x;
            rotationY += mouseMovement.y;

            rotationX = Mathf.Clamp(rotationX, -m_rightRotationLimit, m_leftRotationLimit);
            rotationY = Mathf.Clamp(rotationY, -upwardRotationLimit, downwardRotationLimit);
            
            Vector3 newRotation = new Vector3(rotationY, rotationX,0);
            currentRotation = Vector3.SmoothDamp(currentRotation, newRotation, ref velocity, smoothTime);
            
            transform.localEulerAngles = Vector3.SmoothDamp(currentRotation, newRotation, ref velocity, smoothTime);;
        }
    }
}
