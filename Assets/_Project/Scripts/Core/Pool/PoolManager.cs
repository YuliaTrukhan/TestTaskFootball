using UnityEngine;

namespace _Project.Scripts.Core.Pool
{
    public class PoolManager: MonoBehaviour 
    {
        [SerializeField] private PoolObject[] m_poolObjects;
        public static PoolManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if(Instance != this)
            { 
                Destroy(gameObject); 
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public GameObject Get(string id, Vector3 position, Quaternion rotation)
        {
            PoolObject pool = GetPool(id);
            return pool?.Get(position, rotation);
        }

        private PoolObject GetPool(string id)
        {
            foreach (var poolObject in m_poolObjects)
            {
                if (poolObject.Id.Equals(id))
                {
                    return poolObject;
                }   
            }

            return null;
        }
    }
}