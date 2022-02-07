using System.Net;
using Newtonsoft.Json;

namespace AuthServerApp.Exceptions;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;    
    
    public ExceptionHandlerMiddleware(RequestDelegate next)    
    {    
        _next = next;    
    }    
    
    public async Task Invoke(HttpContext context)    
    {    
        try    
        {    
            await _next.Invoke(context);    
        }    
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
        }    
    }
    
    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)  
    {  
        context.Response.ContentType = "application/json";  
        int statusCode = (int)HttpStatusCode.InternalServerError;  
        var result = JsonConvert.SerializeObject(new  
        {
            Message = exception.Message  
        });  
        context.Response.ContentType = "application/json";  
        context.Response.StatusCode = statusCode;  
        return context.Response.WriteAsync(result);  
    } 
}