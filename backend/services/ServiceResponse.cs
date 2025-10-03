namespace backend.services
{

    public enum ServiceResult
    {
        Success,
        NotFound,
        ValidationFailed,
        Failed
    }
    public class ServiceResponse<T> where T : class
    {
        public ServiceResult ServiceResult { get; set; } = ServiceResult.Success;
        public T? Entity { get; set; } = null;
        public string? Message { get; set; }

        public static ServiceResponse<T> Success(T? ent = null)
        {
            return new ServiceResponse<T> { ServiceResult = ServiceResult.Success, Entity = ent, Message = null };
        }
        public static ServiceResponse<T> NotFound(string? message = null)
        {
            return new ServiceResponse<T> { ServiceResult = ServiceResult.NotFound, Entity = null, Message = message };
        }

        public static ServiceResponse<T> ValidationFailed(string? message = null)
        {
            return new ServiceResponse<T> { ServiceResult = ServiceResult.ValidationFailed, Entity = null, Message = message };
        }

        public static ServiceResponse<T> Failed(string? message = null)
        {
            return new ServiceResponse<T> { ServiceResult = ServiceResult.Failed, Entity = null, Message = message };
        }



    }
}