using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YApi;

namespace YConsole.Model
{
    public class ServerInteractor
    {
        public YApiInteractor ApiInteractor { get; private set; }

        public ServerInteractor()
        {
            ApiInteractor = new(ConfigInteractor.GetToken());
        }


    }
}
