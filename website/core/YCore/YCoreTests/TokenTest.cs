using YCore.API.Handlers;
using YCore.Data;

namespace YCoreTests
{
    public class TokenTest
    {
        public TokenTest()
        {
            DatabaseInteractor.LoadConnectionString(Constants.CONNECTION_STRING);
        }

        [Fact]
        public void ValidateTokenTest()
        {
            string token = "CheckTokenTest";
            var db = DatabaseInteractor.Instance();
            db.CreateToken(token);

            Assert.True(db.ValidateToken(token));
        }

        [Fact]
        public void CreateTokenTest()
        {
            string token = "CreateTokenTest";
            var handler = new TokenCreateHandler(token);
            var response = handler.ProcessRequest();
            Assert.Null(response.Exception);
        }

        [Fact]
        public void DeleteTokenTest()
        {
            string token = "DeleteTokenTest";
            var db = DatabaseInteractor.Instance();
            db.CreateToken(token);

            Assert.True(db.ValidateToken(token));

            var handler = new TokenDeleteHandler(token);
            var response = handler.ProcessRequest();

            Assert.Null(response.Exception);
            Assert.True((bool?)response.ResponseData);
            Assert.False(db.ValidateToken(token));
        }
    }
}
