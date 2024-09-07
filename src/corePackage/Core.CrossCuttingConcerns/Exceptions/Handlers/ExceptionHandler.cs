using Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.Handlers
{
    public abstract class ExceptionHandler // miras sinf ama soyut sinf
    {
        public Task HandleExceptionAsync(Exception exception) =>
            exception switch
            {
                BusinessException businessException => HandleException(businessException),
                 ValidationException validationException => HandleException(validationException),
                // burda sadece business ile ilgili olarak hata verir dikkat 
                _ => HandleException(exception) // bu esa global olarak verir 
            };

        protected abstract Task HandleException(BusinessException businessException); // business exception

        protected abstract Task HandleException(ValidationException validationException); // Validation exception

        protected abstract Task HandleException(Exception exception); // global exception

          // bu soyut sinf oldu icin burda uni icini doldurmuyoruz dikkat uni miras verdimiz class da method icini dolduryoruz 

    }
}
