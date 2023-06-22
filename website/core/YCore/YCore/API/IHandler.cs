using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApiModel;
using YCore.API.IO;

namespace YCore.API
{
    public interface IHandler
    {
        Response ProcessRequest();
    }
}
