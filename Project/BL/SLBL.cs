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
}