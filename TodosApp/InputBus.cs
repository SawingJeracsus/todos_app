using TodosApp.DB.Models;
using TodosApp.DB.Services;
namespace TodosApp;

public class InputBus
{
    public void Start(string userIdentifier, string nickname)
    {
        var userService = new UserService();

        userService.CreateIfNotExists(userIdentifier, nickname);   
     }
}