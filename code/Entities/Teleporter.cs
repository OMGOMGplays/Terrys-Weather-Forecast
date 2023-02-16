namespace TWF
{
    public partial class Teleporter : ModelEntity, IUse 
    {
        public int ChargePercent = 0;

        public bool IsInTeleEvent;

        public override void Spawn() 
        {
            base.Spawn();

            SetModel("models/temp/teleporter/teleporter_temp.vmdl");

            PhysicsEnabled = false;
            UsePhysicsCollision = true;
        }

        public void TriggerTeleEvent() 
        {
            // SpawnBoss(); - Haven't even implemented enemy NPCs, do this when it all done

            IsInTeleEvent = true;

            Particles.Create("particles/teleporter/teleporterevent.vpcf", this);
        }

        public bool TeleEventFinished() 
        {
            if (IsInTeleEvent) return false;

            Log.Info("Tele event is finished!");

            var user = Game.LocalPawn as TWFPlayer;
            if (OnUse(user))
            {
                return true;
            }

            return false;
        }   

        public override void Simulate(IClient cl) 
        {
            base.Simulate(cl);

            if (IsInTeleEvent) 
            {
                ChargePercent += (Time.Delta / 2).FloorToInt();
            }

            if (ChargePercent == 100) 
            {
                TeleEventFinished();
            }
        }

        public virtual bool OnUse(Entity user)
        {
            if (!IsInTeleEvent) 
            {
                TriggerTeleEvent();
                return true;
            }

            return false;
        }

        public virtual bool IsUsable(Entity user) 
        {
            return true;
        }
    }
}