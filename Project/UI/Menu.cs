using Models;
using BL;
using DL;
using System.ComponentModel.DataAnnotations;

namespace UI;

public class Menu
{

    private readonly ISLBL _b1;

    public Menu(ISLBL b1)
    {
        _b1 = b1;
    }

    public void Start()
    {
        bool exit = false;
        do
        {
            Transition();
            Console.WriteLine("Welcome to the Games Palace");
            Console.WriteLine("[0]: Login");
            Console.WriteLine("[1]: Create Account");
            Console.WriteLine("[x]: Exit");

            Input:
            string? response = Console.ReadLine();

            switch(response.Trim().ToUpper())
            {
                case "0": 
                    Login();
                    break;
                case "1":
                    Signup();
                    break;
                case "14383421":
                    Admin();
                    break;
                case "X":
                    Console.WriteLine("Goodbye.");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Input not acceppted, Please try again.");
                    goto Input;
            }

        }while (!exit);
    }

    public void Transition()
    {
        Console.WriteLine("\n-------------------------------------------\n");
    }

    public void Login()
    {
        Transition();

        EnterLogin:
        Console.WriteLine("Please enter your name");
        string? name = Console.ReadLine();
        Console.WriteLine("Please enter the password for your account");
        string? password = Console.ReadLine();

        Customer login = new Customer();

        try
        {
            login.Name = name;
            login.Pass = password;
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            goto EnterLogin;
        }

        int results = _b1.LoginCheck(login);
        switch(results)
        {
            case 0:
                InputLogin0:
                Console.WriteLine("Account name does not exsist");
                Console.WriteLine("Try again? (Y/N)");
                string? responseLogin0 = Console.ReadLine();
                if (responseLogin0.Trim().ToUpper()[0] == 'Y')
                    goto EnterLogin;
                else if (responseLogin0.Trim().ToUpper()[0] == 'N')
                    break;
                else 
                {
                    Console.WriteLine("Input not recognized");
                    goto InputLogin0;
                }
            case 1: 
                InputLogin1:
                Console.WriteLine("Password is incorrect");
                Console.WriteLine("Try again? (Y/N)");
                string? responseLogin1 = Console.ReadLine();
                if (responseLogin1.Trim().ToUpper()[0] == 'Y')
                    goto EnterLogin;
                else if (responseLogin1.Trim().ToUpper()[0] == 'N')
                    break;
                else 
                {
                    Console.WriteLine("Input not recognized");
                    goto InputLogin1;
                }
            case 2:
                Console.WriteLine("Login successful!");
                CustomerMenu(login);
                break;


        }
    }

    public void CustomerMenu(Customer current)
    {
        CustomerMenuInput:
        Transition();
        Console.WriteLine($"Welcome {current.Name}.");
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("[0]: Shop games");
        Console.WriteLine("[1]: View cart");
        Console.WriteLine("[2]: View order history");
        Console.WriteLine("[3]: Delete Account");
        Console.WriteLine("[x]: Go back");
        string? cmResponse = Console.ReadLine();

        switch(cmResponse.Trim().ToUpper()[0])
        {
            case '0':
                break;
            case '1':
                break;
            case '2':
                break;
            case '3':
                break;
            case 'X': 
                break;
            default:
                Console.WriteLine("Input not acceppted, Please try again.");
                goto CustomerMenuInput;

        }
    }

    public void Signup()
    {
        Transition();
        Console.WriteLine("Welcome new customer please provide some information before getting started.");
        
        EnterCustomerInfo:
        Console.WriteLine("What is your name? ");
        string? name = Console.ReadLine();

        Console.WriteLine("Please create a password");
        string? password = Console.ReadLine();

        Customer newCustomer = new Customer();

        try
        {
            newCustomer.Name = name;
            newCustomer.Pass = password;
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            goto EnterCustomerInfo;
        }

        Customer createdCustomer = _b1.CreateCustomer(newCustomer);
        if (createdCustomer != null)
            Console.WriteLine("\nAccount created successfully");
    }

    public void Admin()
    {
        Transition();
        Console.WriteLine("Welcome to the Admin Menu");
        Console.WriteLine("[0]: Replenish Stocks");
        Console.WriteLine("[1]: View Inventory");
        Console.WriteLine("[2]: Add Product");
        Console.WriteLine("[x]: Go back");

        AdminInput:
            string? response = Console.ReadLine();

            switch(response.Trim().ToUpper()[0])
            {
                case '0': 

                    break;
                case '1':

                    break;
                case '2':
                    AddProduct();
                    break;
                case 'X':
                    break;
                default:
                    Console.WriteLine("Input not acceppted, Please try again.");
                    goto AdminInput;
            }
    }

    public Inventory GetInventory()
    {
        return null;
    }

    public void AddProduct() 
    {
        Transition();
        
        EnterProductInfo:
        Console.WriteLine("What is the name of the game you would like to add?");
        string? proName = Console.ReadLine();

        Console.WriteLine("What is the price?");
        double? proPrice = Convert.ToDouble(Console.ReadLine());

        Product newPro = new Product();

        try
        {
            newPro.ItemName = proName;
            newPro.Price = proPrice.Value;
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            goto EnterProductInfo;
        }

        Product createdProduct = _b1.CreateProduct(newPro);
        if (createdProduct != null)
            Console.WriteLine("\nProduct created successfully");
    }
}