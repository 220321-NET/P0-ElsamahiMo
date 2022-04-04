using DL;
using Models;
namespace BL;

public class SLBL : ISLBL
{
    private readonly IRepository _repo;

    public SLBL(IRepository repo)
    {
        _repo = repo;
    }

    public Customer CreateCustomer(Customer newCustomer)
    {
        return _repo.CreateCustomer(newCustomer);
    }

    public int LoginCheck(Customer login)
    {
        return _repo.LoginCheck(login);
    }

    public Customer GetCustomer(Customer cust)
    {
        return _repo.GetCustomer(cust);
    }
}