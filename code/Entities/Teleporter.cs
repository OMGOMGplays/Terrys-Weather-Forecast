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
        }

        public void TriggerTeleEvent() 
        {
            // SpawnBoss(); - Not yet even implemented enemy NPCs, do this when it all done

            IsInTeleEvent = true;

            Particles.Create("particles/teleporter/teleporterevent.vpcf", this);
        }

        public bool TeleEventFinished() 
        {
            if (IsInTeleEvent) return false;

            IsInTeleEvent = false;
            return true;
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
            TriggerTeleEvent();
            return true;
        }

        public virtual bool IsUsable(Entity user) 
        {
            return true;
        }
    }
}