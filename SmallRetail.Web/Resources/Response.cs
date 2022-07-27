using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallRetail.Web.Resources
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public string Message { get; set; }
        public Dictionary<LinkedResourceType, LinkedResource> Links { get; init; }
        public Response()
        {
            Message = string.Empty;
            Links = new Dictionary<LinkedResourceType, LinkedResource>();
        }
        public Response(T data)
        {
            Data = data;
            Succeeded = true;
            Errors = null;
            Message = string.Empty;
            Links = new Dictionary<LinkedResourceType, LinkedResource>();
        }
    }
}
