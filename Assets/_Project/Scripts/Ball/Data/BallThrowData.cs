using UnityEngine;

namespace _Project.Scripts.Ball.Data
{
    public class BallThrowData
    {
        public BallThrowData(Vector3 direction, float throwStrength)
        {
            Direction = direction;
            ThrowStrength = throwStrength;
        }

        public Vector3 Direction { get; private set; }
        public float ThrowStrength { get; private set; }
    }
}