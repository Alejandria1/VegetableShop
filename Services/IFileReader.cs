namespace VegetableShop.Services
{
    public interface IFileReader
    {

        public List<string> ProcessFile(MemoryStream productsPricesList);
        public Dictionary<string, float> ProcessProductsFile(List<string> lines);

        public Dictionary<string, int> ProcessShoppingListFile(List<string> lines);
    }
}
