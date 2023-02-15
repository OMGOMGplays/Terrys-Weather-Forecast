namespace TWF
{
	public partial class TWFGame 
	{
		public int CurrentStage = 1;

		private TimeSince TimeSinceChangingStage;

		public string[] Stage1Maps = {"oop.stage1_1", "oop.stage1_2", "oop.stage1_3"};
		public string[] Stage2Maps = {"oop.stage2_1", "oop.stage2_2", "oop.stage2_3"};
		public string[] Stage3Maps = {"oop.stage3_1", "oop.stage3_2", "oop.stage3_3"};
		public string[] Stage4Maps = {"oop.stage4_1", "oop.stage4_2", "oop.stage4_3"};

		public void ChangeStage(int nextStage) 
		{
			if (CurrentStage == nextStage) 
			{
				Log.Error($"Stage is already {CurrentStage}!"); 
				return;
			}

			TimeSinceChangingStage = 0;

			CurrentStage++;
			Log.Info("Changing stage...");

			string nextMap = "";

			var randomOneToThree = Game.Random.Int(1, 3);
			
			switch (nextStage) 
			{
				case 1: nextMap = Stage1Maps[randomOneToThree]; break;
				case 2: nextMap = Stage2Maps[randomOneToThree]; break;
				case 3: nextMap = Stage3Maps[randomOneToThree]; break;
				case 4: nextMap = Stage4Maps[randomOneToThree]; break;
				case 5: nextMap = "oop.stage5"; break;
				case 6: nextMap = "oop.stage_shop"; break;
			}

			if (TimeSinceChangingStage >= 15.0f) 
			{
				Game.ChangeLevel(nextMap);
			}
		}
	}
}
