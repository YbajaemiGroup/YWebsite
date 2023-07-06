using YApiModel.Models;
using YCore.API.IO.Exceptions;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class GroupGetHandler : Handler, IHandler
    {
        public IResponseSender GetResponseSender()
        {
            var db = DatabaseInteractor.Instance();
            var groups = new List<GroupGetData>();
            var players = db.GetPlayers();
            foreach (var player in players.Where(p => p.GroupNumber != null))
            {
                var group = groups.FirstOrDefault(g => g.Group == player.GroupNumber);
                if (group == null)
                {
                    if (player.GroupNumber == null)
                    {
                        CoreException = new UnknownInnerException();
                        return GetResponseSender(null);
                    }
                    group = new GroupGetData()
                    {
                        Group = player.GroupNumber.Value
                    };
                    groups.Add(group);
                }
                group.PlayerId.Add(player.Id);
            }
            return GetResponseSender(groups);
        }
    }
}
