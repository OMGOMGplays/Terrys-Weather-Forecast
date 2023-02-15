namespace TWF
{
	public partial class TWFGame : GameManager
    {
		public static TWFGame Instance;

		public TWFGame() 
		{
			Instance = this;

			if (Game.IsClient) 
			{
				_ = new TWFUI();
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
