using System;
using UnityEngine;

namespace _Project.Scripts.Core.Pool.Data
{
    [Serializable]
    public class PoolObjectData
    {
        public GameObject TargetObject;
        public int InitializeCount;
        public int MaxCount;
    }
}