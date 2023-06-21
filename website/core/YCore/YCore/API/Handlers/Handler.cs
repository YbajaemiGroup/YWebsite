using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCore.API.IO;
using YCore.API.IO.Exceptions;

namespace YCore.API.Handlers
{
    internal abstract class Handler
    {
        protected CoreException? CoreException;

        protected Response GetResponse(object? responseData)
        {
            if (CoreException == null)
            {
                return new() { ResponseData = responseData };
            }
            return new() { Exception = CoreException };
        }
    }
}
