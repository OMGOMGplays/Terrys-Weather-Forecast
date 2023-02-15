using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

using TWF.Items.Base;

namespace TWF.UI 
{
    public partial class PickupItem : Panel 
    {
        public ItemBase Item;

        public Label ItemName;
        public Label ItemDesc;

        public Panel ItemIconTexture;

        private TimeSince TimeSinceDisplayed;

        public PickupItem() 
        {
            StyleSheet.Load("ui/Pickupitem.scss");

            ItemName = Add.Label("UI", "itemname");
            ItemDesc = Add.Label("UI", "itemdesc");

            ItemIconTexture = Add.Panel("itemicon");

            SetClass("hidden", true);
        }  

        public override void Tick() 
        {
            base.Tick();

            var user = (Game.LocalPawn as TWFPlayer);

            if (Item == null) return;

            if (user.ItemInventory.OnItemAdded()) 
            {
                DisplayItem(Item);
            }
        }

        public void DisplayItem(ItemBase item) 
        {
            SetClass("hidden", false);

            TimeSinceDisplayed = 0;

            ItemName.Text = item.ItemName;
            ItemDesc.Text = item.ItemDesc;

            ItemIconTexture.Style.BackgroundImage = Texture.Load(FileSystem.Mounted, $"{item.ItemModel}_c.png");

            if (TimeSinceDisplayed >= 5.0f) 
            {
                SetClass("hidden", true);
            }
        }
    }
}