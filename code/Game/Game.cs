namespace TWF
{
	public partial class TWFGame : GameManager
    {
		public static TWFGame Instance;

		public static Teleporter Teleporter;

		public TWFGame() 
		{
			Instance = this;

			if (Game.IsClient) 
			{
				_ = new TWFUI();
			}
			if (Game.IsServer) 
			{
				// PlayStageMusic("temp_stage_music");

				ChestSpawner chestSpawner = new();
				chestSpawner.SpawnChests();

				var teleSpawner = new TeleporterSpawner();
				teleSpawner.SpawnTeleporter();
				Teleporter = teleSpawner.NewTeleporter;
			}
		}

		public override void ClientJoined(IClient cl)
        {
			base.ClientJoined(cl);

			var pawn = new TWFPlayer(cl);
			pawn.Respawn();

			cl.Pawn = pawn;
        }
    }
}
