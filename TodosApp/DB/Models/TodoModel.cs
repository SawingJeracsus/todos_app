namespace TodosApp.DB.Models;

public class TodoModel: BaseModel
{
    public long Author;
    public long Assignee;
    public string Task;
    public string? Description;
    public bool Completed;
}