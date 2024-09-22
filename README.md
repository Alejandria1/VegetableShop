The solution is a web application that has 3 main functions in the controller VegetableShopController:

- create-products: receives a .txt file as parameter, the response can be either a BadRequest in case the file was not uploaded or there is something wrong with the structure and it could not be processed has to follow the strucuture 
PRODUCT,PRICE
Tomato,0.75
Aubergine,0.9
Carrot,1

- buy-products: receives a .txt file as parameter, the response can be either a BadRequest in case the file was not uploaded or there is something wrong with the structure and it could not be processed has to follow the strucuture 
PRODUCT,QUANTITY
Tomato,3
Aubergine,25
Carrot,12

- GetTicket: Get method, no parameters received, it generates the ticket and the response is a .txt file download with the ticket information.


FileReader is used by create products and buy products to process the txt received and get the list of products as a data structure.

create products and buy products, use VegetableShop, which is a singleton class, to manage the list of products in memory allowing to Access same instances of vegetable products from both controllers to modify its data, in the firts controller it sets them the name and Price and with the second it sets the quantity, for the same objects.

The Factory method pattern is used to generate the specific classes for each type of product, it uses an abstract class of vegetableProduct defining a method ApplyOffer that later each product implements as needed.

VegetableShop is also in charge of generating the ticket, it contains functions to get the total, the total of discounts and the total to be payed, it creates the ticketVegetableShop.txt that later the user can downloa as response from GetTicket.
