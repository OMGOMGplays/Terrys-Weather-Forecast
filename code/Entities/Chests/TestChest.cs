namespace TWF.Chests 
{
    public partial class TestChest : ChestBase 
    {
        public override int ChestPrice => base.ChestPrice;

        public override ChestType ChestType => ChestType.Normal;
    }
}