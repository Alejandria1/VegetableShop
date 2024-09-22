namespace VegetableShop.Factory
{
    public class Tomato : VegetableProduct
    {
        
        public Tomato(string name, float price, int quantity) : base(name, price, quantity)
        {
        }

        public override void applyOffers()
        {
            //For every 4€ spent on Tomatoes, we will deduct one euro from your final invoice.
            if(Total > 0)
            {
                TotalDiscount = (float)Math.Floor(Total / 4);
            }
        }
    }
}
