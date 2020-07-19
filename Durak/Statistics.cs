using System;
using System.IO;

namespace Durak
{
    class Statistics
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string PlayerName = "Player";
        /// <summary>
        /// The number of games that the player has played since the last reset.
        /// </summary>
        public int NumberOfGames;
        /// <summary>
        /// The number of non-losses the player has.
        /// </summary>
        public int Wins;
        /// <summary>
        /// The number of times the player has been the fool.
        /// </summary>
        public int Losses;

        private const string STATS_FILE_PATH = "./assets/statistics.txt";

        /// <summary>
        /// Save the current stats to the statistics file within the assets folder.
        /// </summary>
        public void Save()
        {
            using (var file = new StreamWriter(STATS_FILE_PATH))
            {
                file.WriteLine($"Player: {PlayerName}");
                file.WriteLine($"Number Of Games: {NumberOfGames}");
                file.WriteLine($"Wins: {Wins}");
                file.WriteLine($"Losses: {Losses}");
            }
        }

        /// <summary>
        /// Read the stats from the file within the assets folder.
        /// </summary>
        public static Statistics Read()
        {
            Statistics stats = new Statistics();

            if (File.Exists(STATS_FILE_PATH))
            {
                using (var file = new StreamReader(STATS_FILE_PATH))
                {
                    stats.PlayerName = file.ReadLine()
                                           .Split(':')[1]
                                           .Trim();

                    stats.NumberOfGames = Convert.ToInt32(file.ReadLine()
                                                              .Split(':')[1]
                                                              .Trim());

                    stats.Wins = Convert.ToInt32(file.ReadLine()
                                                     .Split(':')[1]
                                                     .Trim());

                    stats.Losses = Convert.ToInt32(file.ReadLine()
                                                       .Split(':')[1]
                                                       .Trim());
                }
            }

            return stats;
        }
    }
}
