using UI;
using BL;
using DL;

string connectionString = File.ReadAllText("./connectionString.txt");

//Dependency Injection, Whatever that is 
IRepository repo = new DBRepository(connectionString);
ISLBL b1 = new SLBL(repo);
new Menu(b1).Start();