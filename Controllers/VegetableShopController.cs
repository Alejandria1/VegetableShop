using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection.PortableExecutable;
using VegetableShop.Factory;
using VegetableShop.Services;

namespace VegetableShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VegetableShopController : ControllerBase
    {

        private readonly ILogger<VegetableShopController> _logger;

        private readonly IFileReader _reader;

        public VegetableShopController(ILogger<VegetableShopController> logger, IFileReader reader)
        {
            _logger = logger;
            _reader = reader;
        }
        /*
         * This controller receives a .txt file with the products and its prices
         * It creates the products as instances of class vegetableProduct
        */

        [HttpPost("create-products", Name = "CreateProducts")]
        public async Task<IActionResult> CreateProducts(IFormFile productsPricesList)
        {
            List<string> products;
            Dictionary<string, float> productsPrices;
            if (productsPricesList == null) //Ideally the file should be evaluated in a specific class to validate the extension, size, etc for security reasons.
            {
                return BadRequest("error: No input file");
            }

            using (var memorystream = new MemoryStream()) // code taken from https://stackoverflow.com/questions/66552288/unable-to-cast-object-of-type-microsoft-asp-netcore-http-formfile-to-type-sys
            {
                await productsPricesList.CopyToAsync(memorystream);
                memorystream.Position = 0;
                products = _reader.ProcessFile(memorystream);
             }
            productsPrices = _reader.ProcessProductsFile(products);

            VegetableShop vegetableShop = VegetableShop.Instance;

            foreach (var product in productsPrices)
            {
                try
                {
                    VegetableProduct vp = VegetableFactory.CreateProduct(product.Key, product.Value);
                    vegetableShop.AddProduct(vp);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return Ok(string.Format("{0} Products processed correctly", products.Count));
        }


        /*
            * This controller receives a .txt file with the products and the quantity
            * 
        */

        [HttpPost("buy-products", Name = "shoppingList")]
        public async Task<IActionResult> BuyProducts(IFormFile shoppingList)
        {
            List<string> products;
            Dictionary<string, int> productQty;
            if (shoppingList == null)
            {
                return BadRequest("error: No input file");
            }

            using (var memorystream = new MemoryStream())
            {
                await shoppingList.CopyToAsync(memorystream);
                memorystream.Position = 0;
                products = _reader.ProcessFile(memorystream);
            }

            productQty = _reader.ProcessShoppingListFile(products);
            VegetableShop vegetableShop = VegetableShop.Instance;

            foreach (var product in productQty)
            {
                try
                {
                    VegetableFactory.CreateProduct(product.Key, product.Value);
                    VegetableProduct vp = vegetableShop.GetProductByType(product.Key);
                    vp.Quantity = product.Value;

                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return Ok(string.Format("{0} Products processed correctly", products.Count));
        }



        /*
            * This controller generates the ticket for the purchase and download it
            * 
        */
        [HttpGet(Name = "GetTicket")]
        public IActionResult GetTicket()
        {
           
            VegetableShop vegetableShop = VegetableShop.Instance;
            vegetableShop.PrintTicket();

            // path to the txt created in printTicket()
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fileName = @"\ticketVegetableShop.txt";
            string fullPath = folder + fileName;

            // Verify the path exists
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("Sorry there was an error");
            }

            var bytes = System.IO.File.ReadAllBytes(fullPath);

            return File(bytes, "text/plain", fileName);  // based on https://code-maze.com/aspnetcore-web-api-return-file/
        }
    }
}