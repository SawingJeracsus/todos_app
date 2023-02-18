using TodosApp.DB.Services;
using TodosApp.Session;

namespace TodosApp;

public class InputBus
{
    public void CreateUser(string userIdentifier, string nickname)
    {
        var userService = new UserService();

        userService.CreateIfNotExists(userIdentifier, nickname);
        new UserIdentifierSessionValue().Set(userIdentifier);
    }

    public bool ShouldRegisterUser()
    {
        var identifier = new UserIdentifierSessionValue().Get();
        
        return identifier == null;
    }
}