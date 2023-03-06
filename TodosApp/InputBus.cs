using TodosApp.DB.Models;
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

    public void AddTodo(string title, string description)
    {
        var identifier = new UserIdentifierSessionValue().Get();

        if (identifier == null)
        {
            throw new KeyNotFoundException();
        }

        var userService = new UserService();
        var todoService = new TodoService();
        var user = userService.GetUserByIdentifier(identifier);

        if (user == null)
        {
            throw new KeyNotFoundException();
        }

        var todoModel = new TodoModel();

        todoModel.Assignee = todoModel.Author = user.Id;
        todoModel.Task = title;
        todoModel.Description = description;
        todoModel.Completed = 0;
        
        todoService.Add(todoModel);
    }
}