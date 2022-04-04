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
                    Console.WriteLine("Secret, Congrats.");
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

        Console.WriteLine("Please enter your name");
        string? name = Console.ReadLine();

        Console.WriteLine("Please enter the password for your account");
        string? password = Console.ReadLine();

        

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
        Console.WriteLine("[x]: Go back");

        Input:
            string? response = Console.ReadLine();

            switch(response.Trim().ToUpper())
            {
                case "0": 

                    break;
                case "1 ":

                    break;
                case "X":
                    break;
                default:
                    Console.WriteLine("Input not acceppted, Please try again.");
                    goto Input;
            }
    }
}