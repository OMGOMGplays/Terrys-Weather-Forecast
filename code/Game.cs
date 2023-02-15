using Sandbox;

using TWF.UI;

namespace TWF
{
	public partial class TWFGame : GameManager
    {
		public TWFGame() 
		{
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
