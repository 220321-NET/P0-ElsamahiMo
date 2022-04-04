using Models;

namespace DL;

public interface IRepository
{
    Customer CreateCustomer(Customer newCustomer);
}