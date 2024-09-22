namespace VegetableShop.Factory
{
    public abstract class VegetableProduct
    {
        public string ProductName = "";
        public float Price = 0;
        public int Quantity = 0;
        public float Total = 0;
        public float TotalDiscount = 0;
        public int Free = 0;

        public VegetableProduct(string name, float price, int quantity = 0)
    {
        ProductName = name;
        Price = price;
        Quantity = quantity;
    }
        public void SetTotal() {
            Total = Quantity * Price;
        }

        public abstract void applyOffers();
    }
}
