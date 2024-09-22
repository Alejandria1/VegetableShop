namespace VegetableShop.Factory
{
    public class Aubergine : VegetableProduct
    {
        public Aubergine(string name, float price, int quantity) : base(name, price, quantity)
        {
        }

        public override void applyOffers()
        {

            //substrat the free aubergines from the tomato offer
            if (Free > 0)
            {
                TotalDiscount += Free * Price;
            }

            //Buy 3 Aubergines and pay 2.
            if (Total > 0)
            {
                int freeAubergines = (Quantity-Free) / 3; //count only the aubergines payed not the added by tomato offer 
                TotalDiscount += freeAubergines * Price;
            }
        }
    }
}
