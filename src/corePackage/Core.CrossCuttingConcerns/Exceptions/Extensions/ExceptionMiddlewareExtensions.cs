using Core.CrossCuttingConcerns.Exceptions.Types.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
          // middleware dosyasini icinda Exception Middleware calisacak dikkat 
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<ExceptionMiddleware>();
     
    }
}
