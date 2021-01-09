using ToDoListDataService.Exceptions;

namespace ToDoListDataService.Utils
{
    public static class ExceptionTypes
    {
        public static ExceptionType RECORD_NOT_FOUND = new ExceptionType(1000, "Record not found");
        public static ExceptionType DB_ERROR = new ExceptionType(1001, "Db error.");
    }
}