namespace TWF.Items 
{
    public class TestItem1 : ItemBase 
    {
        public override string ItemModel => "models/sbox_props/concrete_barrier/concrete_barrier.vmdl";

        public override string ItemName => "Test Item 1";
        public override string ItemDesc => "This is a test item!";

        public override ItemRarity ItemRarity => ItemRarity.Common;

        public override void Spawn() 
        {
            base.Spawn();
        }

        public override bool OnUse(Entity user) 
        {
            base.OnUse(user);

            (user as TWFPlayer).Health *= 100;
            return true;
        }
    }
}