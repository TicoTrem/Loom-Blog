using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.services;

namespace backend.controllers
{
    public static class HttpResponseMapper
    {
        public static IResult DefaultHttpResponse<T>(this ServiceResponse<T> response)
        where T : class
        {
            return response.ServiceResult switch
            {
                ServiceResult.Success => Results.Ok(response.Entity),
                ServiceResult.NotFound => Results.NotFound("No entity with the given ID was found. No action taken"),
                ServiceResult.ValidationFailed => Results.BadRequest("The validation has failed. No action taken"),
                ServiceResult.Failed => Results.InternalServerError(),
                _ => Results.InternalServerError("Unknown error")
            };
        }
    }
}