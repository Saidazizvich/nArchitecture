using Core.CrossCuttingConcerns.Exceptions.Extensions;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers
{
    public class HttpExceptionHandler : ExceptionHandler
    {
        private HttpResponse? _response; // sonuc donduryor dikkat

        public HttpResponse Response
        {
            get => _response?? throw new ArgumentNullException(nameof(_response));
            set => _response = value;
        }


        protected override Task HandleException(BusinessException businessException)
        {
             Response.StatusCode = StatusCodes.Status400BadRequest;
            string detail = new BusinessProblemDetails(businessException.Message).AsJson();
            return Response.WriteAsync(detail);  // business exception
        }

        protected override Task HandleException(Exception exception)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string detail = new InternalServerProblemDetails(exception.Message).AsJson();
            return Response.WriteAsync(detail);  // global exception 
        }

        protected override Task HandleException(ValidationException validationException)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string detail = new InternalServerProblemDetails(validationException.Message).AsJson();
            return Response.WriteAsync(detail);  // global exception 
        }
    }
}
