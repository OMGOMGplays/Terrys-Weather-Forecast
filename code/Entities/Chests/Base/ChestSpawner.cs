namespace TWF.Chests.Base 
{
	public partial class ChestSpawner 
	{
		public int AmountOfChestsToSpawn;

		public void SpawnChests() 
		{
			var randomNumber = Game.Random.Int(15, 45);
			var randomPositionOnMap = Game.PhysicsWorld.Body.Position;

			AmountOfChestsToSpawn = randomNumber;

			for (int i = 0; i < AmountOfChestsToSpawn; i++) 
			{
				var newChest = new ChestBase();
				newChest.Position = randomPositionOnMap;
			}
		}
	}
}
