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

            ItemName = Add.Label("UI Name 1", "itemname");
            ItemDesc = Add.Label("UI Description 1", "itemdesc");

            ItemIconTexture = Add.Panel("itemicon");
            ItemIconTexture.Style.BackgroundImage = Texture.Load(FileSystem.Mounted, "ui/temp/temp_item_icon.png");

            SetClass("hidden", true);
        }  

        public override void Tick() 
        {
            base.Tick();

            var user = (Game.LocalPawn as TWFPlayer);

            if (user.ItemInventory.OnItemAdded(Item) || Input.Pressed(InputButton.Menu)) 
            {
                DisplayItem(Item);
            }
        }

        public void DisplayItem(ItemBase item) 
        {
            SetClass("hidden", false);

            Log.Info("Displaying!");

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