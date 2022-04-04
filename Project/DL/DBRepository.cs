using Microsoft.Data.SqlClient;
using System.Data;
using Models;
namespace DL;

public class DBRepository : IRepository
{
    private readonly string _connectionString;

    public DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Customer CreateCustomer(Customer newCustomer)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Customers(customerName, customerPass) OUTPUT INSERTED.Id VALUES (@customerName, @customerPass)", connection);

        cmd.Parameters.AddWithValue("@customerName", newCustomer.Name);
        cmd.Parameters.AddWithValue("@customerPass", newCustomer.Pass);

        cmd.ExecuteScalar();
        connection.Close();

        return newCustomer;
    }

}