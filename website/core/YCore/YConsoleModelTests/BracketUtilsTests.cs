using YConsole.Model;
using YConsoleModel;

namespace YConsoleModelTests
{
    public class BracketUtilsTests
    {
        [Fact]
        public void GetGameRowByPlayerDescriptorTest()
        {
            var playersDescriptors = new int[5]
            {
                0, 1, 2, 3, 4
            };
            var expected = new int[5]
            {
                1, 1, 2, 2, 3
            };
            var real = new int[5];
            for (int i = 0; i < 5; i++)
            {
                real[i] = BracketUtils.GetGameRowByPlayerDescriptor(playersDescriptors[i]);
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetPlayerPositionTest()
        {
            var playersDescriptors = new int[5]
            {
                0, 1, 2, 3, 4
            };
            var expected = new PlayerPosition[5]
            {
                PlayerPosition.Player1,
                PlayerPosition.Player2,
                PlayerPosition.Player1,
                PlayerPosition.Player2,
                PlayerPosition.Player1
            };
            var real = new PlayerPosition[5];
            for (int i = 0; i < 5; i++)
            {
                real[i] = BracketUtils.GetPlayerPosition(playersDescriptors[i]);
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetPlayerDescriptorByGameRowTest()
        {
            var gameRows = new int[3]
            {
                1, 2, 3
            };
            var expected = new int[6]
            {
                0, 1, 2, 3, 4, 5
            };
            var real = new int[6];
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                real[j] = BracketUtils.GetPlayerDescriptorByGameRow(gameRows[i], PlayerPosition.Player1);
                j++;
                real[j] = BracketUtils.GetPlayerDescriptorByGameRow(gameRows[i], PlayerPosition.Player2);
                j++;
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetReverseRoundNumberTest()
        {
            var expected = new int[8]
            {
                8, 7, 6, 5, 4, 3, 2, 1
            };
            var real = new int[8];
            for (int i = 0; i < 8; i++)
            {
                real[i] = BracketUtils.GetReverseLowerRoundNumber(i + 1);
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetPlayersCountInLowerByRoundNumberTest()
        {
            var expected = new int[8]
            {
                1, 1, 2, 2, 4, 4, 8, 8
            };
            var real = new int[8];
            for (int i = 0; i < 8; i++)
            {
                real[i] = BracketUtils.GetPlayersCountInLowerByRoundNumber(i);
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetPlayersCountInLowerByRoundDescriptorTest()
        {
            var expected = new int[8]
            {
                8, 8, 4, 4, 2, 2, 1, 1
            };
            var real = new int[8];
            for (int i = 0; i < 8; i++)
            {
                real[i] = BracketUtils.GetPlayersCountInLowerByRoundDescriptor(i + 1);
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetPlayersCountInUpperByRoundDescriptorTest()
        {
            var expected = new int[5]
            {
                8, 4, 2, 1, 1
            };
            var real = new int[5];
            for (int i = 0; i < 5; i++)
            {
                real[i] = BracketUtils.GetPlayersCountInUpperByRoundDescriptor(i + 1);
            }
            Assert.Equal(expected, real);
        }

        [Fact]
        public void GetPlayersCountInUpperByRoundNumberTest()
        {
            var expected = new int[5]
            {
                1, 1, 2, 4, 8
            };
            var real = new int[5];
            for (int i = 0; i < 5; i++)
            {
                real[i] = BracketUtils.GetPlayersCountInUpperByRoundNumber(i + 1);
            }
            Assert.Equal(expected, real);
        }
    }
}