namespace TWF.Chests.Base 
{
	public partial class ChestSpawner 
	{
		public void SpawnChests() 
		{
			var randomOneToHundred = Game.Random.Int(1, 100);
			var randomNumber = Game.Random.Int(15, 30);
			int amountOfChestsToSpawn;

			amountOfChestsToSpawn = randomNumber;

			for (int i = 0; i < amountOfChestsToSpawn; i++) 
			{	
				Vector3 randomPositionOnMap = Vector3.Zero;
				randomPositionOnMap += Vector3.Random.x * randomOneToHundred;
				randomPositionOnMap += Vector3.Random.y * randomOneToHundred;

				var newChest = new ChestBase();
				newChest.Position = randomPositionOnMap;
			}
		}
	}
}
