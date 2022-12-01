namespace TodosApp.DB.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public static T AssignBaseFields<T>(T model, BaseModel baseModel) where T: BaseModel
    {
        model.Id = baseModel.Id;
        model.CreatedAt = baseModel.CreatedAt;
        model.UpdatedAt = baseModel.UpdatedAt;

        return model;
    }
}
