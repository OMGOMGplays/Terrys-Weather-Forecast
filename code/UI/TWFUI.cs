using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace TWF.UI 
{
    public partial class TWFUI : HudEntity<RootPanel> 
    {
        public static TWFUI Instance;

        public TWFUI()
        {
            Instance = this;

            if (Instance != null) 
            {
                Instance = null;
                Instance?.Delete();
            }

            RootPanel.AddChild<PickupItem>();
        }
    }
}