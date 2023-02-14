using Sandbox;
using System;
using System.Linq;

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

            return base.Add(item, false);
        }
    }
}