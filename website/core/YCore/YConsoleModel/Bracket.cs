using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using YApiModel.Models;
using YConsoleModel;

namespace YConsole.Model
{
    public class Bracket
    {
        private List<Round> rounds;
        private List<Player> players;

        public Bracket(List<Round> rounds, List<Player> players)
        {
            this.rounds = rounds;
            this.players = players;
        }

        public List<Round> Rounds { get => rounds; }

        public Round? GetRound(int roundDescriptor) => rounds.FirstOrDefault(r => r.RoundNumber == roundDescriptor);

        public static Game? GetGame(int playerDescriptor, Round round) => round.Games.FirstOrDefault(g => g.Row == BracketUtils.GetGameRowByPlayerDescriptor(playerDescriptor));

        public static int GetPlayerDescriptor(Game game, Expression<Func<Game, int?>> playerIdSelector)
        {
            var property = (PropertyInfo)((MemberExpression)playerIdSelector.Body).Member;
            var pl1Prop = typeof(Game).GetProperty(nameof(game.Player1Id));
            var pl2Prop = typeof(Game).GetProperty(nameof(game.Player2Id));
            if (property.Name == pl1Prop?.Name)
            {
                return BracketUtils.GetPlayerDescriptorByGameRow(game.Row, PlayerPosition.Player1);
            }
            else if (property.Name == pl2Prop?.Name)
            {
                return BracketUtils.GetPlayerDescriptorByGameRow(game.Row, PlayerPosition.Player2);
            }
            else
            {
                throw new ArgumentException($"Only {pl1Prop?.Name} or {pl2Prop?.Name} are supported.", nameof(playerIdSelector));
            }
        }

        public int GetPlayersCountByRoundDescriptor(int roundDescriptor, bool isUpper)
        {
            if (isUpper && roundDescriptor > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(roundDescriptor));
            }
            if (!isUpper && roundDescriptor > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(roundDescriptor));
            }
            if (isUpper)
            {
                return BracketUtils.GetPlayersCountInUpperByRoundDescriptor(roundDescriptor);
            }
            else
            {
                return BracketUtils.GetPlayersCountInLowerByRoundDescriptor(roundDescriptor);
            }
        }

        public Player? GetPlayer(int playerDescriptor, int roundDescriptor)
        {
            var round = GetRound(roundDescriptor);
            if (round == null)
            {
                throw new ArgumentException($"No round found for roundDescriptor {roundDescriptor}.", nameof(roundDescriptor));
            }
            var game = GetGame(playerDescriptor, round);
            if (game == null)
            {
                throw new ArgumentException($"No game found for playerDescriptor {playerDescriptor}.", nameof(playerDescriptor));
            }
            var playerId = BracketUtils.GetPlayerPosition(playerDescriptor) switch
            {
                PlayerPosition.Player1 => game.Player1Id,
                PlayerPosition.Player2 => game.Player2Id,
                _ => throw new UnreachableException("wtf?! playerDescriptor % 2 gave nor 0 or 1!")
            };
            return players.FirstOrDefault(p => p.Id == playerId);
        }

        public void SetWinners()
        {
            foreach (var round in rounds)
            {
                foreach (var game in round.Games)
                {
                    SetWinner(round.RoundNumber, game);
                }
            }
        }

        public void SetWinner(int roundDescriptor, Game game)
        {
            roundDescriptor++;
            var nextRound = rounds.FirstOrDefault(r => r.RoundNumber == roundDescriptor);
            if (nextRound == null)
            {
                return;
            }
            var nextRoundGame = nextRound.Games
                .FirstOrDefault(g => BracketUtils.Player1Won(game, g) ||
                                     BracketUtils.Player2Won(game, g)); // игра из следующего раунда, в которой играет игрок из этой игры.
            if (nextRoundGame == null)
            {
                return;
            }
            if (BracketUtils.Player1Won(game, nextRoundGame))
            {
                game.WinnerId = game.Player1Id;
            }
            else if (BracketUtils.Player2Won(game, nextRoundGame))
            {
                game.WinnerId = game.Player2Id;
            }
        }

        public Game SetPlayer(int playerDescriptor, int roundDescriptor, bool isUpper, int playerId)
        {
            var round = GetRound(roundDescriptor);
            if (round == null)
            {
                round = new()
                {
                    RoundNumber = roundDescriptor,
                    Games = new()
                };
                rounds.Add(round);
            }
            var game = GetGame(playerDescriptor, round);
            if (game == null)
            {
                game = new()
                {
                    Row = BracketUtils.GetGameRowByPlayerDescriptor(playerDescriptor),
                    IsUpper = isUpper
                };
                round.Games.Add(game);
            }
            switch (BracketUtils.GetPlayerPosition(playerDescriptor))
            {
                case PlayerPosition.Player1:
                    game.Player1Id = playerId;
                    break;
                case PlayerPosition.Player2:
                    game.Player2Id = playerId;
                    break;
            }
            return game;
        }

        public void ForEach(Action<int, int, bool, object> action)
        {
            var games = rounds.Select(r => r.Games.AsEnumerable()).Aggregate((l1, l2) => l1.Concat(l2));
            foreach (var round in rounds)
            {
                foreach (var game in round.Games)
                {
                    action.Invoke(GetPlayerDescriptor(game, g => g.Player1Id), round.RoundNumber, game.IsUpper, players.First(p => p.Id == game.Player1Id));
                    action.Invoke(GetPlayerDescriptor(game, g => g.Player2Id), round.RoundNumber, game.IsUpper, players.First(p => p.Id == game.Player2Id));
                }
            }
        }
    }
}