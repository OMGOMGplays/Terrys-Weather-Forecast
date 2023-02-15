namespace TWF.UI 
{
    public partial class MoneyCounter : Panel 
    {
        public Label Money;

        public MoneyCounter() 
        {
            StyleSheet.Load("ui/hud/MoneyCounter.scss");

            Money = Add.Label("0", "money");
        }

        public override void Tick() 
        {
            base.Tick();

            var player = (Game.LocalPawn as TWFPlayer);

            Money.Text = $"{player.Money}";
        }
    }
}