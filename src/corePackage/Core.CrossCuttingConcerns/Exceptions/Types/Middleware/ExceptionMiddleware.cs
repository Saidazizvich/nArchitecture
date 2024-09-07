using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Types.Middleware
{
    public class ExceptionMiddleware
    {
        // middleware nedir bize istek yani request yapdi zaman uni karsiya bilecek ve icina girib ve sonuc yani response 
        // gonderdimiz zaman calisacak bir ara islemidir ve onemli islemdir dikkat 

        private readonly RequestDelegate _next;  // delegate nedir bu bizim butun metodlarmizni toplab bir class icinda tutar 
        private readonly HttpExceptionHandler _httpExceptionHandler;

        public ExceptionMiddleware(RequestDelegate next, HttpExceptionHandler httpExceptionHandler)
        {
            _next = next;
            _httpExceptionHandler = httpExceptionHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                await HandleExceptionAsync(context.Response, exception);

            }


        }
        // business exception             // global exception
        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }

    }
}
