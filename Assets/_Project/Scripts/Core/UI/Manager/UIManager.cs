using System;
using _Project.Scripts.Core.UI.Data;
using _Project.Scripts.Core.UI.Panels;
using UnityEngine;

namespace _Project.Scripts.Core.UI.Manager
{
    public class UIManager: MonoBehaviour
    {
        public event Action<BaseUIPanel> OnPanelShow = null;
        public event Action<BaseUIPanel> OnPanelHide = null;
        
        [SerializeField] private BaseUIPanel m_startPanel;
        [SerializeField] private BaseUIPanel[] m_uiPanels;
        
        public static UIManager Instance { get; private set; }

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

        private void Start()
        {
            m_startPanel.ShowPanel();
        }

        public void ShowPanel<T>(BaseUIPanelData data = null) where T : BaseUIPanel
        {
            foreach (var panel in m_uiPanels)
            {
                if (panel is T)
                {
                    panel.InitializePanel(data);
                    panel.ShowPanel();
                    
                    OnPanelShow?.Invoke(panel);
                    break;
                }
            }
        }
        
        public void HidePanel<T>() where T : BaseUIPanel
        {
            foreach (var panel in m_uiPanels)
            {
                if (panel is T)
                {
                    panel.HidePanel();
                    OnPanelHide?.Invoke(panel);
                    break;
                }
            }
        }

        public T TryGetPanel<T>() where T : BaseUIPanel
        {
            foreach (var panel in m_uiPanels)
            {
                if (panel is T tPanel)
                {
                    return tPanel;
                }
            }

            return null;
        }
    }
}