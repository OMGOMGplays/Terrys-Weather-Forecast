using Sandbox;

using TWF.UI;

namespace TWF.Items.Base
{
	public partial class ItemBase : ModelEntity, IUse
	{
		public virtual string ItemModel {get; set;}

		public virtual string ItemName {get; set;}
		public virtual string ItemDesc {get; set;}

		public virtual ItemRarity ItemRarity {get; set;}

		private string ParticleToPlay;

		public override void Spawn()
		{
			base.Spawn();

			switch (ItemRarity) 
			{
				case ItemRarity.Common: ParticleToPlay = "particles/items/common.vpcf"; break;
				case ItemRarity.Uncommon: ParticleToPlay = "particles/items/uncommon.vpcf"; break;
				case ItemRarity.Rare: ParticleToPlay = "particles/items/rare.vpcf"; break;
				case ItemRarity.Legendary: ParticleToPlay = "particles/items/legendary.vpcf"; break;
				case ItemRarity.Equipment: ParticleToPlay = "particles/items/equipment.vpcf"; break;
			}

			Particles.Create(ParticleToPlay, this);

			Log.Info($"Item {ItemName} has been spawned!");

			SetModel(ItemModel);
		}

		public virtual bool OnUse(Entity user)
		{
			(user as TWFPlayer).ItemInventory.AddItem(this);

			Delete();

			Log.Info($"{(user as TWFPlayer).Client.Name} picked up {ItemName}.");

			return true;
		}

		public virtual bool IsUsable(Entity user)
		{
			return true;
		}
	}

	public enum ItemRarity 
	{
		Common,
		Uncommon,
		Rare,
		Legendary,
		Equipment
	}
}
