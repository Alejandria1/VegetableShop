namespace VegetableShop.Factory
{
    public class VegetableFactory
    {
        /* Applying Factory Method, this class creates the different types of products
         */
        public static VegetableProduct CreateProduct(string productType, float price, int qty = 0)
        {
            switch (productType)
            {
                case "tomato":
                    return new Tomato("Tomato", price, qty);
                case "aubergine":
                    return new Aubergine("Aubergine", price, qty);
                case "carrot":
                    return new Carrot("Carrot", price, qty);
                //Add the case for any new products 
                default:
                    throw new ArgumentException("Product {0} is not in our inventory yet :(", productType);
            }
        }
    }
}