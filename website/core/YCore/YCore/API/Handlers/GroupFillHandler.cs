using YApiModel.Models;
using YCore.Data;

namespace YCore.API.Handlers
{
    public class GroupFillHandler : Handler, IHandler
    {
        private readonly List<GroupFillData> groupData;

        public GroupFillHandler(List<GroupFillData> groupData)
        {
            this.groupData = groupData;
        }

        public IResponseSender GetResponseSender()
        {
            if (groupData.Count == 0)
            {
                CoreException = new IO.Exceptions.InvalidDataException("No group data specified.");
                return GetResponseSender(null);
            }
            var db = DatabaseInteractor.Instance();
            var players = db.GetPlayers();
            var updatedPlayers = new List<YDatabase.Models.Player>();
            foreach (var gd in groupData)
            {
                var player = players.Find(p => p.Id == gd.PlayerId);
                if (player == null)
                {
                    continue;
                }
                if (gd.Group >= 4)
                {
                    CoreException = new IO.Exceptions.InvalidDataException("group should be from 0 (A group) to 3 (D group).");
                    return GetResponseSender(null);
                }
                player.GroupNumber = gd.Group;
                updatedPlayers.Add(player);
            }
            foreach (var player in updatedPlayers)
            {
                db.UpdatePlayer(player);
            }
            return GetResponseSender(players.Select(p => new Player(
                p.Nickname,
                p.Descr,
                p.Image?.ImageName,
                p.Id,
                p.GroupNumber,
                p.Won,
                p.Lose,
                p.Points)));
        }
    }
}
