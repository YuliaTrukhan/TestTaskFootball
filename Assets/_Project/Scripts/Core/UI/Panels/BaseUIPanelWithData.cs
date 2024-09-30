using _Project.Scripts.Core.UI.Data;

namespace _Project.Scripts.Core.UI.Panels
{
    public class BaseUIPanelWithData<T>: BaseUIPanel where T: BaseUIPanelData
    {
        protected T Data { get; private set; }

        public override void InitializePanel(BaseUIPanelData data = null)
        {
            base.InitializePanel(data);
            Data = (T)data;
        }

        public override void DeInitializePanel()
        {
            Data = null;
            base.DeInitializePanel();
        }

    }
}