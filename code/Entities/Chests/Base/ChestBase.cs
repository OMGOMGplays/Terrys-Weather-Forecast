namespace TWF.Chests.Base 
{
    public partial class ChestBase : ModelEntity, IUse 
    {
        public string ChestModel {get; set;}

        public virtual int ChestPrice => 25;

        public bool IsOpened;

        public virtual ChestType ChestType => ChestType.Normal;

        public override void Spawn() 
        {
            base.Spawn();

            switch (ChestType) 
            {
                case ChestType.Normal: ChestModel = "models/temp/chest/chest_normal.vmdl"; break;
                case ChestType.Attack: ChestModel = "models/temp/chest/chest_attack.vmdl"; break;
                case ChestType.Health: ChestModel = "models/temp/chest/chest_health.vmdl"; break;
            }

            SetModel(ChestModel);

            IsOpened = false;

            PhysicsEnabled = false;
            UsePhysicsCollision = true;
            

            Tags.Add("prop", "solid");

            // SetupPhysics();
        }

        // public void SetupPhysics() 
        // {
        //     var physics = SetupPhysicsFromModel(PhysicsMotionType.Dynamic, true);
        //     if (!physics.IsValid()) return;
        // }

        public virtual bool OnUse(Entity user) 
        {
            if (user == null) return false;

            var playerUser = user as TWFPlayer;
            
            if (!IsOpened) 
            {
                if (playerUser.Money < ChestPrice) 
                {
                    FailOpenChest();
                    return false;
                }
                else if (playerUser.Money >= ChestPrice) 
                {
                    OpenChest();
                    playerUser.Money -= ChestPrice;
                    return true;
                }
            }

            return false;
        }

        public virtual void FailOpenChest() 
        {
            Log.Error("Player doesn't have enough money!");
            // Play sound, etc.
        }

        public virtual void OpenChest() 
        {
            Log.Info("Opening chest!");
            // Spawn item from pool
        }

        public virtual bool IsUsable(Entity user) 
        {
            if (user == null) return false;

            return true;
        }
    }

    public enum ChestType 
    {
        Normal,
        Attack,
        Health,
        BigNormal,
        BigAttack,
        BigHealth,
        Legendary
    }
}