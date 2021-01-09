using Newtonsoft.Json;

namespace ToDoListDataService.Utils
{
    public class ErrorResult
    {
        public ErrorResult() {
        }

        public string Message { get; set; }
        public int Code { get; set; }
        
        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}