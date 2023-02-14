﻿using Sandbox;

using TWF.Items.Base;

namespace TWF 
{
	public partial class TWFGame
	{
		[ConCmd.Client("cl_spawnitem")]
		public static void SpawnItem(string item) 
		{
			var caller = ConsoleSystem.Caller;

			if (caller == null) return;

			var ent = TypeLibrary.GetType(item);
			var newEntity = ent.Create<ItemBase>();
			
			newEntity.Position = Trace.Ray(caller.Pawn.AimRay, 800).Ignore(caller.Pawn).Run().HitPosition;
		}
	}
}