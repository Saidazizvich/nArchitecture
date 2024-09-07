﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails
{
    public class BusinessProblemDetails: ProblemDetails 
    {
         // burasi sadece application yani business olarak calisiyor dikkat !!!! ve detail olarak hata veriyor 
        public BusinessProblemDetails(string detail)
        {
            Title = "Rule Violation";
            Detail = detail;
            Status = StatusCodes.Status400BadRequest;
            Type = "";
        }
    }

}