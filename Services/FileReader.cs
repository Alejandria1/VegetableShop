namespace VegetableShop.Services
{
    public class FileReader : IFileReader
    {
        public List<string> ProcessFile(MemoryStream productsPricesList)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(productsPricesList))
            {
                sr.ReadLine(); //Reads first line assuming is the header of the columns, might do this dinamically in case there are no headers...
                while (sr.Peek() != -1)
                {
                    lines.Add(sr.ReadLine());
                }
            }
            return lines;

        }

        public Dictionary<string, float> ProcessProductsFile(List<string> lines)
        {
            Dictionary<string, float> products = new Dictionary<string, float>();

            foreach (string line in lines)
            {
                string productName = line.Substring(0, line.IndexOf(","));

                string productPrice = line.Substring(line.IndexOf(",") + 1);

                float price = float.Parse(productPrice);
                products.Add(productName.ToLower(), price);
            }
            return products;
        }

        public Dictionary<string, int> ProcessShoppingListFile(List<string> lines)
        {
            Dictionary<string, int> products = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                string productName = line.Substring(0, line.IndexOf(","));

                string productQuantity = line.Substring(line.IndexOf(",") + 1);

                int quantity = int.Parse(productQuantity);
                products.Add(productName.ToLower(), quantity);
            }
            return products;
        }
    }
}
