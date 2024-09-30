using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.Core.Pool
{
    [Serializable]
    public class PoolObject
    {
        [SerializeField] private string m_id;
        [SerializeField] GameObject m_targetObject;
        
        private List<GameObject> m_objectsList = new();

        public string Id => m_id;

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            for (int i = 0; i < m_objectsList.Count; i++)
            {
                if (m_objectsList[i].activeInHierarchy == false)
                {
                    m_objectsList[i].transform.position = position;
                    m_objectsList[i].transform.rotation = rotation;
                    return m_objectsList[i];
                }
            }

            var newObj = Create(position, rotation);
            return newObj;
        }

        private GameObject Create(Vector3 position, Quaternion rotation)
        {
            GameObject newObj = Object.Instantiate(m_targetObject, position, rotation);
            newObj.name = m_id;
            
            m_objectsList.Add(newObj);

            return newObj;
        }
    }
}