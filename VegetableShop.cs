using System;
using System.IO;
using VegetableShop.Factory;

namespace VegetableShop
{
    public class VegetableShop
    {
        private static VegetableShop _instance = null; //unique instance of vegetableShop, singleton shop manager
        private static readonly object _lock = new object();

        private List<VegetableProduct> _products;
        

        private VegetableShop()
        {
            _products = new List<VegetableProduct>();
        }

        public static VegetableShop Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new VegetableShop();
                    }
                    return _instance;
                }
            }
        }

        public void AddProduct(VegetableProduct product)
        {
            _products.Add(product);
        }

        public VegetableProduct GetProductByType(string productType)
        {
            return _products.Find(p => p.GetType().Name.ToLower() == productType.ToLower());
        }

        public void PrintTicket()
        {
            ApplyOffersAccrossMultipleProducts();
            float total, totalDiscount, grandTotal;
            total = getTotal();
            totalDiscount = getTotalDiscounts();
            grandTotal = totalToPay(total, totalDiscount);
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string fileName = @"\ticketVegetableShop.txt";

            string fullPath = folder + fileName;
            File.WriteAllText(fullPath, String.Empty);
            File.AppendAllText(fullPath, "\n***********************************************");
            File.AppendAllText(fullPath, "\nProduct        | Quantity  | Price     | Total");
            File.AppendAllText(fullPath, "\n-----------------------------------------------");

            foreach (var product in _products)
            {
                if (product.Quantity > 0)
                {
                    // from https://stackoverflow.com/questions/2978311/format-a-string-into-columns
                    File.AppendAllText(fullPath, string.Format("\n{0,-15} | {1,-10} | {2,-10:C} | {3,-10:C}", product.ProductName, product.Quantity, product.Price, product.Total));

                    if (product.TotalDiscount > 0)
                    {
                        File.AppendAllText(fullPath, string.Format("\n                                             -{0,-10:C}", product.TotalDiscount));
                    }
                }
                
            }

 
            File.AppendAllText(fullPath, string.Format("\nTotal                                       {0,-10:C} ", total));
            File.AppendAllText(fullPath, string.Format("\nYou pay                                     {0,-10:C} ", grandTotal));
            File.AppendAllText(fullPath, string.Format("\nYou have saved                              {0,-10:C} ", totalDiscount));
            clearLastPurchase();
        }

        public float getTotalDiscounts()
        {
            float totalDiscount = 0;
            foreach (var product in _products)
            {
                product.applyOffers();
                totalDiscount += product.TotalDiscount;

            }

            return totalDiscount;
        }
        public float getTotal() { 
            float total = 0;
            foreach (var product in _products)
            {
                product.SetTotal();
                total += product.Total;

            }
            return total; 
        }

        public float totalToPay(float total, float totalDiscount)
        {
            float totalToPay = total - totalDiscount;
            return totalToPay;
        }

        public void ApplyOffersAccrossMultipleProducts()
        {
            //Get a free Aubergine for every 2 Tomatoes you buy.
            VegetableProduct tomato = GetProductByType("Tomato");
            int freeAubergines = tomato.Quantity / 2;

            if (freeAubergines > 0) {
                VegetableProduct aubergine = GetProductByType("Aubergine");
                aubergine.Quantity += freeAubergines;
                aubergine.Free = freeAubergines;
            }
        }
        public void clearLastPurchase()
        {
            foreach (var product in _products)
            {
                product.Free = 0;
                product.Quantity = 0;
                product.TotalDiscount = 0;
                product.SetTotal();
            }

        }

    }
}
