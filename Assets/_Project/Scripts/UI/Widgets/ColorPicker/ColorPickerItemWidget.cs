using System;
using _Project.Scripts.ColorSelection.Data;
using _Project.Scripts.Core.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Widgets.ColorPicker
{
    public class ColorPickerItemWidget: BaseWidget
    {
        public event Action<ColorPickerItemWidget> OnSelectedItem;
        
        [SerializeField] private Image m_colorImage;
        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private TextMeshProUGUI m_selectedText;
        [SerializeField] private Button m_targetButton;

        public bool IsActive { get; private set; } = false;
        public ColorItemData ColorItemData { get; private set; } = null;
        public bool IsSelected { get; private set; } = false;

        protected override void Awake()
        {
            base.Awake();
            UpdateSelectionStatus(false);
        }

        protected override void OnDestroy()
        {
            m_targetButton.onClick.RemoveListener(OnButtonClick);
            base.OnDestroy();
        }

        public void Initialize(ColorItemData colorItem)
        {
            ColorItemData = colorItem;

            m_colorImage.color = ColorItemData.Color;
            m_titleText.text = ColorItemData.Name;
        }

        public override void ShowWidget()
        {
            IsActive = true;
            m_targetButton.onClick.AddListener(OnButtonClick);
            base.ShowWidget();
        }

        public override void HideWidget()
        {
            m_targetButton.onClick.RemoveListener(OnButtonClick);
            IsActive = false;
            base.HideWidget();
        }
        
        public void UpdateSelectionStatus(bool isSelected)
        {
            IsSelected = isSelected;
            m_selectedText.enabled = IsSelected;
        }

        private void OnButtonClick()
        {
            OnSelectedItem?.Invoke(this);
        }
    }
}