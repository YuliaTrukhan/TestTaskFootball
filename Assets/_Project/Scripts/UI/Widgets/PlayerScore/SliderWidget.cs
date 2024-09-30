using _Project.Scripts.Core.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Widgets.PlayerScore
{
    public class SliderWidget: BaseWidget
    {
        [SerializeField] private Slider m_cooldownSlider;

        public override void HideWidget()
        {
            m_cooldownSlider.value = 0f;
            base.HideWidget();
        }

        public void UpdateCooldown(float value)
        {
            m_cooldownSlider.value = value;
        }
    }
}