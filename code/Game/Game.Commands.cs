namespace TWF 
{
	public partial class TWFGame
	{
		[ConCmd.Client("cl_givemoney")]
		public static void GiveMoneyCommand(int amountToGive) 
		{
			var caller = ConsoleSystem.Caller;
			if (caller == null) return;

			var callerPlayer = Game.LocalPawn as TWFPlayer;

			if (amountToGive <= 0) 
			{
				Log.Error("Can't give less than or 0 money!");
				return;
			}

			Log.Info($"Giving {amountToGive} money!");
			callerPlayer.Money += amountToGive;
		}

		[ConCmd.Client("cl_spawnitem")]
		public static void SpawnItemCommand(string item) 
		{
			var caller = ConsoleSystem.Caller;
			if (caller == null) return;

			var ent = TypeLibrary.GetType(item);
			var newEntity = ent.Create<ItemBase>();
			
			newEntity.Position = Trace.Ray(caller.Pawn.AimRay, 800)
								.Ignore(caller.Pawn)
								.Run()
								.HitPosition;
		}

		[ConCmd.Client("cl_spawnchest")]
		public static void SpawnChestCommand(string chest) 
		{
			var caller = ConsoleSystem.Caller;
			if (caller == null) return;

			var ent = TypeLibrary.GetType(chest);
			var newEntity = ent.Create<ChestBase>();
			
			newEntity.Position = Trace.Ray(caller.Pawn.AimRay, 800)
								.Ignore(caller.Pawn)
								.Run()
								.HitPosition;
		}

		[ConCmd.Client("cl_giveitem")]
		public static void GiveItemCommand(string item) 
		{
			var caller = ConsoleSystem.Caller;
			if (caller == null) return;

			var callerPlayer = caller as TWFPlayer;

			var listOfItems = new List<ItemBase>();

			var itemToAdd = listOfItems.First().ItemName;

			if (item == itemToAdd) 
			{
				var itemToAddEntity = listOfItems.Find(x => x.Name == item);
				callerPlayer.ItemInventory.AddItem(itemToAddEntity);
			}
		}

		[ConCmd.Server("sv_changestage")]
		public static void ChangeStageCommand(int nextStage) 
		{
			if (nextStage <= 0) 
			{
				Log.Error("There is no stage 0 or below!");
				return;
			}
			else if (nextStage > 6) 
			{
				Log.Error("There aren't more than 6 stages!");
				return;
			}

			Log.Info($"Changing stage to {nextStage}");
		}
	}
}
