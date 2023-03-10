namespace TWF
{
	public partial class TWFPlayer : Player
    {
		private DamageInfo LastDamage;

		private Entity LastWeapon;

		public ClothingContainer Clothing = new();

		public ItemInventory ItemInventory;

		public TWFPlayer()
        {
			ItemInventory = new ItemInventory(this);
			Inventory = new Inventory(this);
        }

		public TWFPlayer(IClient cl) : this()
        {
			Clothing.LoadFromClient( cl );
        }

		public override void Respawn()
        {
			base.Respawn();

			SetModel( "models/citizen/citizen.vmdl" );

			Controller = new WalkController();

			Clothing.DressEntity( this );

			Inventory.Add(new TestWeapon());
        }

		public override void Simulate( IClient cl )
		{
			base.Simulate( cl );

			var controller = GetActiveController();
			if (controller != null)
			{
				SimulateAnimation( controller );
			}

			UpdateMoney();

			TickPlayerUse();
			SimulateActiveChild( cl, ActiveChild );
		}
	}
}
