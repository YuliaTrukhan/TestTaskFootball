using System.Collections.Generic;
using _Project.Scripts.Core.UI.Data;
using _Project.Scripts.Core.UI.Widgets;
using UnityEngine;

namespace _Project.Scripts.Core.UI.Panels
{
    public abstract class BaseUIPanel: MonoBehaviour
    {
        [Header("Widgets")] 
        [SerializeField] private List<BaseWidget> m_widgetsList = new();
        
        protected BaseUIPanelData CurrentData { get; set; }
        
        protected virtual void Awake()
        {
            
        }
        
        protected virtual void OnDestroy()
        {
            
        }

        public virtual void InitializePanel(BaseUIPanelData data = null)
        {
            CurrentData = data;
        }

        public virtual void DeInitializePanel()
        {
            CurrentData = null;
        }
        
        public virtual void ShowPanel()
        {
            gameObject.SetActive(true);
            m_widgetsList.ForEach(widget => widget.ShowWidget());
        }
        
        public virtual void HidePanel()
        {
            m_widgetsList.ForEach(widget => widget.HideWidget());
            gameObject.SetActive(false);
        }
    }
}