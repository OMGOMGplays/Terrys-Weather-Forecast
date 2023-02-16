namespace TWF.Chests.Base 
{
    public partial class ChestBase : ModelEntity, IUse 
    {
        public string ChestModel {get; set;}

        public virtual int ChestPrice => 25;

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

            PhysicsEnabled = true;
            UsePhysicsCollision = true;
            Tags.Add("prop", "solid");

            SetupPhysics();
        }

        public void SetupPhysics() 
        {
            var physics = SetupPhysicsFromModel(PhysicsMotionType.Dynamic, true);
            if (!physics.IsValid()) return;
        }

        public virtual bool OnUse(Entity user) 
        {
            if (user == null) return false;

            var playerUser = (user as TWFPlayer);
            if (playerUser.Money < ChestPrice) 
            {
                FailOpenChest();
                return false;
            }

            OpenChest();
            playerUser.Money -= ChestPrice;
            return true;
        }

        public virtual void FailOpenChest() 
        {
            // Play sound, etc.
        }

        public virtual void OpenChest() 
        {
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