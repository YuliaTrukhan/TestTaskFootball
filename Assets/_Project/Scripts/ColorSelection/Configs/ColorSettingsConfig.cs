using System;
using _Project.Scripts.ColorSelection.Data;
using UnityEngine;

namespace _Project.Scripts.ColorSelection.Configs
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game/Color/ColorSettingsConfig", fileName = "ColorSettingsConfig")]
    public class ColorSettingsConfig: ScriptableObject
    {
        [SerializeField] private ColorItemData[] m_colorDataList;

        public ColorItemData[] ColorDataList => m_colorDataList;
    }
}