namespace TWF.Items.Base
{
    public partial class ItemInventory : BaseInventory 
    {
        public ItemInventory(Player player) : base(player) 
        {
        }

        public bool CanAddItem(ItemBase item) 
        {
            if (!item.IsValid()) return false;

            if (base.CanAdd(item)) return false;

            return true;
        }

        public bool AddItem(ItemBase item) 
        {
            if (!item.IsValid()) return false;

            OnItemAdded(item);

            Log.Info($"Item {item.ItemName} has been added!");

            return base.Add(item, false);
        }

        public bool OnItemAdded(ItemBase item) 
        {
            if (item != null) return true;

            return false;
        }
    }
}