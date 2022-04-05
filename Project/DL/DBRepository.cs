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

    public int LoginCheck (Customer login)
    {
        bool found = false;
        bool correct = false;

        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE customerName = @customerName", connection);

        cmd.Parameters.AddWithValue("@customerName", login.Name);

        SqlDataReader read = cmd.ExecuteReader();
        if(read.HasRows)
            found = true;
        read.Close();

        using SqlCommand cmd2 = new SqlCommand("SELECT * FROM Customers WHERE customerName = @customerName AND customerPass = @customerPass", connection);

        cmd2.Parameters.AddWithValue("@customerName", login.Name);
        cmd2.Parameters.AddWithValue("@customerPass", login.Pass);

        SqlDataReader read2 = cmd2.ExecuteReader();
        if(read2.HasRows)
            correct = true;
        read2.Close();

        // if the user name is found and the pass word is correct
        if(correct)
            return 2;

        // if the user name is found but the pass word was incorrect
        if(found)
            return 1;

        // if the user name wasn't found at all
        return 0;

    }

    public Customer GetCustomer(Customer cust)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE customerName = @customerName", connection);

        cmd.Parameters.AddWithValue("@customerName", cust.Name);

        cmd.ExecuteScalar();
        connection.Close();

        return cust;
    }

    public Product CreateProduct(Product newPro)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        using SqlCommand cmd = new SqlCommand("INSERT INTO Products(productName, price) OUTPUT INSERTED.Id VALUES (@productName, @price)", connection);

        cmd.Parameters.AddWithValue("@productName", newPro.ItemName);
        cmd.Parameters.AddWithValue("@price", newPro.Price);

        cmd.ExecuteScalar();
        connection.Close();

        return newPro;
    }

}