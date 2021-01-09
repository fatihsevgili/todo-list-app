using System;
using System.Runtime.Serialization;

namespace ToDoListDataService.Exceptions
{
    public class TodoException : Exception, ISerializable
    {
        public ExceptionType ExceptionType { get; }
        
        public TodoException(ExceptionType exceptionType)
        {
            ExceptionType = exceptionType;
        }
    }
}