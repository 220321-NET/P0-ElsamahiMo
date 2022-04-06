using System.ComponentModel.DataAnnotations;
using Models;

public class Order : Default
{
    private double total = 0.00; 
    private List<Product> cartItems = new List<Product>(); 
    private int custID = 0;
    private int storeID = 0;

//------------------------------------------------------------------

    public DateTime DateCreated { get; set; }

    public void AddTotal (double addPrice)
    {
        total += addPrice;
    } 

    public double Total()
    {
        return total;
    }

    public int CustID {
        get => custID; 
        set
        {
            if(value <= 0)
                throw new ValidationException("Something went wrong with custID");
            
            custID = value;
        }
    }

    public int StoreID {
        get => storeID; 
        set
        {
            if(value <= 0)
                throw new ValidationException("Something went wrong with storeID");
            
            storeID = value;
        }
    }

    

    public void AddCartItems (Product adding)
    {
        cartItems.Add(adding);
        AddTotal(adding.Price);
    }

    public List<Product> CartItem ()
    {
        return cartItems;
    }
    
}