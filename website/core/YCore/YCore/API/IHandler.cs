using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCore.API.IO;

namespace YCore.API
{
    internal interface IHandler
    {
        Response ProcessRequest();
    }
}
