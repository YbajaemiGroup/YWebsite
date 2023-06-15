using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCore.API
{
    internal interface IHandler
    {
        Response ProcessRequest();
    }
}
