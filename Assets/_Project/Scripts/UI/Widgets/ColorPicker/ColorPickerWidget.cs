using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.ColorSelection.Configs;
using _Project.Scripts.ColorSelection.Data;
using _Project.Scripts.Core.UI.Widgets;
using UnityEngine;

namespace _Project.Scripts.UI.Widgets.ColorPicker
{
    public class ColorPickerWidget: BaseWidget
    {
        [SerializeField] private ColorSettingsConfig m_colorSettingsConfig;
        [SerializeField] private ColorPickerItemWidget m_pickerItemPrefab;
        [SerializeField] private RectTransform m_colorPicketItemsContainer;

        private List<ColorPickerItemWidget> m_pickerItemWidgets = new();
        private List<ColorPickerItemWidget> m_spawnedItemWidgets = new();
        private ColorPickerItemWidget m_selectedItem = null;

        public ColorItemData GetSelectedColorConfig()
        {
            return m_selectedItem.ColorItemData;
        }

        public override void ShowWidget()
        {
            base.ShowWidget();
            InitializeColorPicker();
        }

        public override void HideWidget()
        {
            DeInitializeColorPicker();
            base.HideWidget();
        }

        private void InitializeColorPicker()
        {
            var colorItems = m_colorSettingsConfig.ColorDataList;
            foreach (var colorItem in colorItems)
            {
                var picketItem = CreateItem();
                
                picketItem.OnSelectedItem += SelectPickerItem;
                picketItem.Initialize(colorItem);
                picketItem.ShowWidget();
            }

            SelectPickerItem(m_pickerItemWidgets.First());
        }
        
        private void DeInitializeColorPicker()
        {
            foreach (var pickerItemWidget in m_pickerItemWidgets)
            {
                pickerItemWidget.OnSelectedItem -= SelectPickerItem;
                pickerItemWidget.HideWidget();
                m_spawnedItemWidgets.Add(pickerItemWidget);
            }
        }

        private ColorPickerItemWidget CreateItem()
        {
            if (m_spawnedItemWidgets.Any(p => p.IsActive == false))
            {
                var item = m_spawnedItemWidgets.FirstOrDefault(p => p.IsActive == false);
                m_spawnedItemWidgets.Remove(item);
                return item;
            }

            var newItem = Instantiate(m_pickerItemPrefab, m_colorPicketItemsContainer, false);
            m_pickerItemWidgets.Add(newItem);

            return newItem;
        }

        private void SelectPickerItem(ColorPickerItemWidget item)
        {
            if (m_selectedItem == item)
            {
                return;
            }
            
            m_selectedItem?.UpdateSelectionStatus(false);
            m_selectedItem = item;
            m_selectedItem.UpdateSelectionStatus(true);
        }

    }
}