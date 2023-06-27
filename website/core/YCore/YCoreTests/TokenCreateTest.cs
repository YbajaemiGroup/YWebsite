using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCore.API.Handlers;
using YCore.Data;

namespace YCoreTests
{
    public class TokenCreateTest
    {
        [Fact]
        public void CreateToken()
        {
            DatabaseInteractor.LoadConnectionString(Constants.CONNECTION_STRING);
            var handler = new TokenCreateHandler("token");
            handler.GetResponseSender();
        }
    }
}
