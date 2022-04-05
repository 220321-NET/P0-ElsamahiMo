using System.ComponentModel.DataAnnotations;
using Models;

public class store
{
    private string location = "";
    public string StoreLocation
    {
        get => location;
        set
        {
            if(String.IsNullOrWhiteSpace(value))
                throw new ValidationException("Name cannot be empty");
            
            location = value.Trim();
        }
    }
}