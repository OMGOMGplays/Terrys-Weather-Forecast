using Sandbox;
using System;
using System.Linq;

namespace TWF 
{
    partial class Inventory : BaseInventory 
    {
        public Inventory(Player player) : base (player) 
        {
        }

        public override bool CanAdd(Entity entity) 
        {
            if (!entity.IsValid()) return false;

            if (!base.CanAdd(entity)) return false;

            return !IsCarryingType(entity.GetType());
        }

        public override bool Add(Entity entity, bool makeActive = false) 
        {
            if (!entity.IsValid()) return false;

            if (IsCarryingType(entity.GetType())) return false;

            return base.Add(entity, makeActive);
        }

        public bool IsCarryingType(Type t) 
        {
            return List.Any(x => x?.GetType() == t);
        }
    }
}