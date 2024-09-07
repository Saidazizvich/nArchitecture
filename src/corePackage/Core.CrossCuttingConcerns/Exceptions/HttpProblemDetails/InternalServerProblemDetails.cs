using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class InternalServerProblemDetails : ProblemDetails
    { // burasi esa exception olarak hta veriyor dikkat !!!
        public InternalServerProblemDetails(string detail)
        {
            Title = "Internel Server";
            Detail = "Internel server Error";
            Status = StatusCodes.Status500InternalServerError;
            Type = "";
        }
    }

}
