using _Project.Scripts.Core.UI.Panels;
using _Project.Scripts.Network.Messages;
using _Project.Scripts.UI.Widgets.ColorPicker;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Panels
{
    public class SelectColorPanel : BaseUIPanel
    {
        [SerializeField] private ColorPickerWidget m_colorPickerWidget;
        
        [Header("Buttons")]
        [SerializeField] private Button m_completeButton;

        public override void ShowPanel()
        {
            m_completeButton.onClick.AddListener(OnCompleteButtonClick);
            base.ShowPanel();
        }

        public override void HidePanel()
        {
            m_completeButton.onClick.RemoveListener(OnCompleteButtonClick);
            base.HidePanel();
        }

        protected override void OnDestroy()
        {
            m_completeButton.onClick.RemoveListener(OnCompleteButtonClick);
            base.OnDestroy();
        }

        private void OnCompleteButtonClick()
        {
            var selectedColorData = m_colorPickerWidget.GetSelectedColorConfig();
            if (selectedColorData == null)
            {
                return;
            }

            CreateCharacterMessage createCharacterMessage = new CreateCharacterMessage()
            {
                ColorItemData = selectedColorData
            };
            
            NetworkClient.Send(createCharacterMessage);
        }
    }
}
