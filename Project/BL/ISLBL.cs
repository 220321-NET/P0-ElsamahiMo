using Models;
namespace BL;

public interface ISLBL 
{
    Customer CreateCustomer(Customer newCustomer);
    int LoginCheck(Customer login);
    Customer GetCustomer(Customer cust);
    Product CreateProduct(Product newPro);
    Product GetProduct(int id);
    List<Product> GetInventory(Store getInv);
    List<Store> GetStores();
    Order UpdateOrders(Order updateOrder);
}