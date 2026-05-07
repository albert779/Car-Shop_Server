
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarsShop.Dto.Responses.API
{
    public class APIResponse : APIResult
    {

        public object Data { get; }

        private APIResponse(bool success) : this (null, success)
        {
        }

        private APIResponse(object data, bool success) : base(success)
        {
            Data = data;
        }

        // ✅ Generic factory method
        public static APIResponse CreateOK()
        {
            return new APIResponse(true);
        }
        // ✅ Generic factory method
        public static APIResponse CreateOKWithData<T>(T data)
        {
            return new APIResponse(data, true);
        }

        public static APIResponse CreateBad()
        {
            return new APIResponse(false);
        }
        public static APIResponse CreateBadWithMessage<T>(T message)
        {
            return new APIResponse(message, false);
        }
    }
}



