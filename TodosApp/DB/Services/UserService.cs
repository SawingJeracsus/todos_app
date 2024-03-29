using TodosApp.DB.Models;
using TodosApp.Session;

namespace TodosApp.DB.Services;

public class UserService: BaseService<UserModel>
{
    public UserService() : base("user")
    { }

    public static UserModel? GetSessionUser()
    {
        var identifierSession = new UserIdentifierSessionValue();
        var identifier = identifierSession.Get();

        if (identifier == null)
        {
            return null;
        }

        var service = new UserService();

        return service.GetUserByIdentifier(identifier);
    }
    
    public void CreateIfNotExists(string userIdentifier, string nickname)
    {
        if (UserWithIdentifierExists(userIdentifier))
        {
            return;
        }

        var userToCreate = new UserModel()
        {
            Identifier = userIdentifier,
            Nickname = nickname
        };
        
        Add(userToCreate);
    }

    public UserModel? GetUserByIdentifier(string userIdentifier)
    {
        var sql = AppendTableName("SELECT * FROM {0} WHERE Identifier = @identifier");
        var command = CreateCommand(sql);

        command.Parameters.AddWithValue("@identifier", userIdentifier);
        
        var usersWithSameIdentifier = Select(command);

        if (usersWithSameIdentifier.Count == 0)
        {
            return null;
        }
        
        return usersWithSameIdentifier[0];
    }

    public bool UserWithIdentifierExists(string userIdentifier)
    {
        return GetUserByIdentifier(userIdentifier) != null;
    }
}