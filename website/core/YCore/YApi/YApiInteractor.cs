using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YApi
{
    public class YApiInteractor
    {
        private readonly YClient _client;

        public YApiInteractor(string token)
        {
            _client = new YClient(token);
        }
    }
}
