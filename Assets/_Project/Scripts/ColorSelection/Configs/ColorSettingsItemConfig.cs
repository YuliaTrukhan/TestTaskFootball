using System;
using UnityEngine;

namespace _Project.Scripts.ColorSelection.Configs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Color/ColorSettingsItemConfig", fileName = "ColorSettingsItemConfig")]
    public class ColorSettingsItemConfig: ScriptableObject
    {
        [SerializeField] private string m_id;
        [SerializeField] private string m_name;
        [SerializeField] private Color m_color;

        public string Id => m_id;
        public string Name => m_name;
        public Color Color => m_color;
    }
}