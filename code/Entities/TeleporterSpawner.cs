namespace TWF 
{
    public partial class TeleporterSpawner 
    {
        public Teleporter NewTeleporter;

        public void SpawnTeleporter() 
        {
            var randomOneToHundred = Game.Random.Int(1, 100);

            Vector3 randomPositionOnMap = Vector3.Zero;
            randomPositionOnMap += Vector3.Random.x * randomOneToHundred;
            randomPositionOnMap += Vector3.Random.y * randomOneToHundred;

            NewTeleporter = new Teleporter();
            NewTeleporter.Position = randomPositionOnMap;
        }
    }
}