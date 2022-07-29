using System.Globalization;

namespace List_e_rest.Helpers.Models;

public class ApiException : Exception
{
    public ApiException() :base()
    {
            
    }

    public ApiException(string message)
    {
            
    }

    public ApiException(string message, params object[]args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
            
    }
}