using UnityEngine;

namespace _Project.Scripts.Core.UI.Widgets
{
    public class BaseWidget: MonoBehaviour
    {
        protected virtual void Awake()
        {
            
        }
        
        protected virtual void OnDestroy()
        {
            
        }
        
        public virtual void ShowWidget()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void HideWidget()
        {
            gameObject.SetActive(false);
        }
    }
}