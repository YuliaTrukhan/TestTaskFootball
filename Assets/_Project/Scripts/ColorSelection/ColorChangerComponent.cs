using Mirror;
using UnityEngine;

namespace _Project.Scripts.ColorSelection
{
    public class ColorChangerComponent: NetworkBehaviour
    {
        [SerializeField] private MeshRenderer[] m_meshRenderers;

        public void ChangeColor(Color color)
        {
            foreach (var meshRenderer in m_meshRenderers)
            {
                meshRenderer.material.color = color;
            }
        }
    }
}