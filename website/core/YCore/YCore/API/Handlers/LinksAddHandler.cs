using YApiModel;
using YCore.API.IO;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class LinksAddHandler : Handler, IHandler
    {
        private readonly List<YApiModel.Models.Link> links;

        public LinksAddHandler(List<YApiModel.Models.Link> links)
        {
            this.links = links;
        }

        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            var players = db.GetPlayers();
            var errorLinks = links.Where(l => l.Id != null)
                .Where(l => !players.Any(p => p.Id == l.PlayerId));
            if (errorLinks.Any())
            {
                return new JsonResponseSender(new Response()
                {
                    Exception = new IO.Exceptions.InvalidDataException("Invalid player id. In data will be list of links with ivalid player id."),
                    ResponseData = errorLinks
                });
            }
            var tasks = new List<YDatabase.Models.Link>();
            foreach (var link in links)
            {
                tasks.Add(db.InsertLink(new()
                {
                    Link1 = link.LinkUrl,
                    Player = link.PlayerId,
                    Descr = link.Description
                }).Result);
            }
            //Task.WaitAll(tasks.ToArray());
            //var results = tasks.Select(t => t.Result);
            return GetResponseSender(tasks.Select(link => new YApiModel.Models.Link(
                    link.Link1,
                    link.Descr,
                    link.Id,
                    link.Player)));
        }
    }
}
