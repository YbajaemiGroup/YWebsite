using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApiModel;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    public class UnknownInnerExceptionHandler : IHandler
    {
        public Response ProcessRequest()
        {
            return new Response()
            {
                Exception = new UnknownInnerException()
            };
        }
    }
}
