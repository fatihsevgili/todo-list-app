namespace ToDoListDataService.Data.Model.Entity
{
    public class TodoItem : BaseEntity
    {
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}