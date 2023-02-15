namespace TWF.Chests 
{
    public partial class TestChest : ChestBase 
    {
        public override string ChestModel => "models/sbox_props/concrete_barrier/concrete_barrier.vmdl";

        public override int ChestPrice => base.ChestPrice;

        public override ChestType ChestType => ChestType.Normal;
    }
}