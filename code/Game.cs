using Sandbox;

namespace TWF
{
	public partial class TWFGame : GameManager
    {
		public override void ClientJoined(IClient cl)
        {
			base.ClientJoined(cl);

			var pawn = new TWFPlayer(cl);
			pawn.Respawn();

			cl.Pawn = pawn;
        }
    }
}
