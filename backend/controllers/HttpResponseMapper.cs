using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.services;

namespace backend.controllers
{
    
    public static class HttpResponseMapper
    {
        /// <summary>
        /// Converts the ServiceResult property of a ServiceResponse object to a sensicle IResult controller response object.
        /// It is meant to handle the most common uses. You can use ServiceResponse object data to create different logic
        /// instead of using this extension method if necessary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceResponse"></param>
        /// <returns></returns>
        public static IResult DefaultHttpResponse<T>(this ServiceResponse<T> serviceResponse)
        where T : class
        {
            return serviceResponse.ServiceResult switch
            {
                ServiceResult.Success => serviceResponse.Entity == null ? Results.NoContent() : Results.Ok(serviceResponse.Entity),
                ServiceResult.NotFound => Results.NotFound("No entity with the given ID was found. No action taken"),
                ServiceResult.ValidationFailed => Results.BadRequest("The validation has failed. No action taken"),
                ServiceResult.Failed => Results.InternalServerError(),
                _ => Results.InternalServerError("Unknown error")
            };
        }
    }
}