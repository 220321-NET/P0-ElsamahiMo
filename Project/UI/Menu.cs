using Models;
using BL;

namespace UI;

public class Menu
{


    public void Start()
    {
        bool exit = false;
        do
        {
            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine("Welcome to the store(placeholder)");
            Console.WriteLine("[0]: Login");
            Console.WriteLine("[1]: Create Account");
            Console.WriteLine("[x]: Exit");

            Input:
            string? response = Console.ReadLine();

            switch(response)
            {
                case "0": 

                    break;
                case "1":

                    break;
                case "14383421":
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
}