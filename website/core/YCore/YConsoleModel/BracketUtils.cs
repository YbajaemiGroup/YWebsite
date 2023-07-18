using System.Diagnostics;
using YApiModel.Models;

namespace YConsoleModel
{
    public static class BracketUtils
    {
        public static int GetGameRowByPlayerDescriptor(int playerDescriptor) => playerDescriptor / 2 + 1;

        public static PlayerPosition GetPlayerPosition(int playerDescriptor) => (PlayerPosition)(playerDescriptor % 2);

        public static int GetPlayerDescriptorByGameRow(int gameRow, PlayerPosition playerPosition) => playerPosition switch
        {
            PlayerPosition.Player1 => gameRow * 2 - 2,
            PlayerPosition.Player2 => gameRow * 2 - 1,
            _ => throw new UnreachableException(),
        };

        public static bool Player1Won(Game game, Game nextRoundGame) => nextRoundGame.Player1Id == game.Player1Id || nextRoundGame.Player2Id == game.Player1Id;

        public static bool Player2Won(Game game, Game nextRoundGame) => nextRoundGame.Player1Id == game.Player2Id || nextRoundGame.Player2Id == game.Player2Id;

        public static int GetReverseUpperRoundNumber(int roundDescriptor)
        {
            if (roundDescriptor > 5 || roundDescriptor < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(roundDescriptor));
            }
            return 6 - roundDescriptor;
        }

        public static int GetReverseLowerRoundNumber(int roundDescriptor)
        {
            if (roundDescriptor > 8 || roundDescriptor < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(roundDescriptor));
            }
            return 9 - roundDescriptor;
        }

        public static int GetPlayersCountInLowerByRoundNumber(int roundNumber)
        {
            if (roundNumber < 2)
            {
                return 1;
            }
            return GetPlayersCountInLowerByRoundNumber(roundNumber - 2) * 2;
        }

        public static int GetPlayersCountInLowerByRoundDescriptor(int roundDescriptor)
        {
            int roundNumber = GetReverseLowerRoundNumber(roundDescriptor) - 1;
            return GetPlayersCountInLowerByRoundNumber(roundNumber);
        }

        public static int GetPlayersCountInUpperByRoundNumber(int roundNumber)
        {
            if (roundNumber < 3)
            {
                return 1;
            }
            return GetPlayersCountInUpperByRoundNumber(roundNumber - 1) * 2;
        }

        public static int GetPlayersCountInUpperByRoundDescriptor(int roundDescriptor)
        {
            int roundNumber = GetReverseUpperRoundNumber(roundDescriptor);
            return GetPlayersCountInUpperByRoundNumber(roundNumber);
        }
    }
}
