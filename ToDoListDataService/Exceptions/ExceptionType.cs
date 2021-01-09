namespace ToDoListDataService.Exceptions
{
    public class ExceptionType
    {
        public ExceptionType(int exceptionCode, string message)
        {
            ExceptionCode = exceptionCode;
            Message = message;
        }

        public int ExceptionCode { get; }
        public string Message { get; }
    }
}