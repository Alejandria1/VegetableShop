namespace VegetableShop.Factory
{
    public abstract class VegetableProduct
    {
        public string ProductName = "";
        public float Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public float Total { get; set; } = 0;
        public float TotalDiscount  { get; set; } = 0;
        public int Free { get; set; } = 0;

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
