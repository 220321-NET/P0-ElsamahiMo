using Models;

namespace DL;

public interface IRepository
{
    Customer CreateCustomer(Customer newCustomer);
    int LoginCheck (Customer login);
    Customer GetCustomer(Customer cust);
    Product CreateProduct(Product newPro);
}