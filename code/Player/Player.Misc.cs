namespace TWF 
{
    public partial class TWFPlayer 
    {
        public int CurrentMoney {get; set;}
        public int Money = 0;

        public void UpdateMoney() 
        {
            CurrentMoney = Money;
        }
    }
}