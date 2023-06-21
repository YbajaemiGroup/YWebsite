﻿using YCore.API.IO;
using YCore.Data;

namespace YCore.API.Handlers
{
    internal class TokenCreateHandler : Handler, IHandler
    {
        private readonly string _key;

        public TokenCreateHandler(string key)
        {
            _key = key;
        }

        public void AddTokenToDb()
        {
            var db = DatabaseInteractor.Instance();
            try
            {
                db.CreateToken(_key);
            }
            catch (Exception e)
            {
                Logger.Log(LogSeverity.Debug, nameof(TokenCreateHandler), $"Создание токена {_key} упало нахрен.", e);
            }
        }

        public Response ProcessRequest()
        {
            return GetResponse(null);
        }
    }
}
