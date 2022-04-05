using Models;
using BL;
using DL;
using System.ComponentModel.DataAnnotations;

namespace UI;

public class Menu
{

    private readonly ISLBL _bl;

    public Menu(ISLBL bl)
    {
        _bl = bl;
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

        int results = _bl.LoginCheck(login);
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
        bool customerExit = false;
        do
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
                    ShopGames();
                    break;
                case '1':
                    break;
                case '2':
                    break;
                case '3':
                    break;
                case 'X':
                    customerExit = true;
                    break;
                default:
                    Console.WriteLine("Input not acceppted, Please try again.");
                    goto CustomerMenuInput;
            }
        }while(!customerExit);
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

        Customer createdCustomer = _bl.CreateCustomer(newCustomer);
        if (createdCustomer != null)
            Console.WriteLine("\nAccount created successfully");
    }

    public void ShopGames()
    {
        Transition();
        Console.WriteLine("Which store would you like to shop at?");
        Store shopAt = SelectStore();

        Console.WriteLine("Select the game you would like to add to your cart.");
        Product shopPro = SelectInventory(shopAt);

        shopConfirm:
        Console.WriteLine($"Are you should you would like to add \n{shopPro.ItemName} at ${shopPro.Price} to your cart (Y/N)");
        string shopInput = Console.ReadLine();

        switch(shopInput.Trim().ToUpper()[0])
        {
            case 'Y':
                AddToCart(shopPro);
                break;
            case 'N':
                Console.WriteLine("Item not added to cart");
                break;
            default:
                Console.WriteLine("Invalid input, Try again");
                goto shopConfirm;
                break;
        }

    }

    public void AddToCart(Product shopCart)
    {

    }
    public void Admin()
    {
        bool adminExit = false;
        do
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
                        ReplenishStock();
                        break;
                    case '1':
                        ViewInventory();
                        break;
                    case '2':
                        AddProduct();
                        break;
                    case 'X':
                        adminExit = true;
                        break;
                    default:
                        Console.WriteLine("Input not acceppted, Please try again.");
                        goto AdminInput;
                }
        }while(!adminExit);
    }

    public Store? SelectStore()
    {
        Console.WriteLine("Here are all the stores by state: ");
        List<Store> allStores = _bl.GetStores();

        if(allStores.Count == 0)
            return null;

        SelectInput:
        for (int i = 0; i < allStores.Count; i++)
            Console.WriteLine(allStores[i].ToString());

        int select;

        if(Int32.TryParse(Console.ReadLine(), out select) && ((select-1) >= 0 && (select-1) < allStores.Count))
            return allStores[select-1];
        else
        {
            Console.WriteLine("Invalid input, Try again");
            goto SelectInput;
        }
    } 

    public Product SelectInventory(Store getInv)
    {
        Transition();
        Console.WriteLine($"Here is the Inventory for the {getInv.StoreLocation} store:");
        List<Product> inventory = _bl.GetInventory(getInv);

        if (inventory.Count == 0)
            return null;
        
        InvInput:
        for (int i = 0; i < inventory.Count; i++)
            Console.WriteLine(inventory[i].ToString());
        
        int proSelect;

        if(Int32.TryParse(Console.ReadLine(), out proSelect) && ((proSelect-1) >= 0 && (proSelect-1) < inventory.Count))
            return inventory[proSelect-1];
        else
        {
            Console.WriteLine("Invalid input, Try again");
            goto InvInput;
        }
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

        Product createdProduct = _bl.CreateProduct(newPro);
        if (createdProduct != null)
            Console.WriteLine("\nProduct created successfully");
    }

    public void ReplenishStock()
    {
        Transition();
        Console.WriteLine("Please choose a location to replenish stocks at");
        Store? replenishStore = SelectStore();

        Product? replenishPro = SelectInventory(replenishStore);

        Console.WriteLine($"Please enter the new quantity of {replenishPro.ItemName}");
        int newQuan = Convert.ToInt32(Console.ReadLine());

        //pausing this to work on neccessary implementation 
        //_bl.UpdateQuantity(newQuan, replenishPro);





        
    }

    public void ViewInventory()
    {
        Transition();
        Console.WriteLine("Which store would you like to view the inventory for?");
        Store? viewStore = SelectStore();

        Console.WriteLine($"Here is the Inventory for the {viewStore.StoreLocation} store:");
        List<Product> inventory = _bl.GetInventory(viewStore);

        if (inventory.Count == 0)
        {
            Console.WriteLine("This store has no inventory");
            return;
        }
        for (int i = 0; i < inventory.Count; i++)
            Console.WriteLine(inventory[i].ToString());

        Console.WriteLine("Press any key to continue");
        string temp = Console.ReadLine();
        
    }

}
