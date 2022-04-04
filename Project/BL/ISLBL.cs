using Models;
namespace BL;

public interface ISLBL 
{
    Customer CreateCustomer(Customer newCustomer);
    int LoginCheck(Customer login);
    Customer GetCustomer(Customer cust);
}