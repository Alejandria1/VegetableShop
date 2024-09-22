namespace VegetableShop.Factory
{
    public class Carrot : VegetableProduct
    {
        public Carrot(string name, float price, int quantity) : base(name, price, quantity)
        {
        }

        public override void applyOffers()
        {
            //Implement to add offers to carrots
        }
    }
}
