/* Assets.cs - Manages the various assets for the Durak game project.
 * 
 * Created By: Jaime Barillas
 * Created: 2019-03-07
 * 
 * Last Modified By: Jaime Barillas
 * Last Modified: 2019-04-01
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DurakGame;

namespace Durak
{
    /// <summary>
    /// Manages the various assets for the game. Call the Load() method to load
    /// all the assets.
    /// </summary>
    static class Assets
    {
        #region Variables

        private const string CARD_SKINS_DIR = "./assets/textures/cardskins/";

        /// <summary>
        /// [Get] The set of card images. Retrieve them as such: Assets.Cards[Suit.Heart][Rank.Three].
        /// </summary>
        public static Dictionary<Suit, Dictionary<Rank, Image>> Cards { get; private set; } =
            new Dictionary<Suit, Dictionary<Rank, Image>>();

        public static Image CardBackside;

        // TODO: autogen from Suits and Ranks enums.
        // NOTE: The image files use a singular suit name instead of plural >_<
        public static Dictionary<string, Suit> validSuits = new Dictionary<string, Suit>
        {
            { "club", Suit.Clubs },
            { "spade", Suit.Spades },
            { "heart", Suit.Hearts },
            { "diamond", Suit.Diamonds }
        };

        public static Dictionary<string, Rank> validRanks = new Dictionary<string, Rank>
        {
            { "2", Rank.Two },
            { "3", Rank.Three },
            { "4", Rank.Four },
            { "5", Rank.Five },
            { "6", Rank.Six },
            { "7", Rank.Seven },
            { "8", Rank.Eight },
            { "9", Rank.Nine },
            { "10", Rank.Ten },
            { "ace", Rank.Ace },
            { "jack", Rank.Jack },
            { "queen", Rank.Queen },
            { "king", Rank.King },
        };

        #endregion

        #region Methods

        /// <summary>
        /// Return an array of card skins available to Durak.
        /// Note that this method does not guarantee that the texture files
        /// exist on disk.
        /// </summary>
        /// <returns>An array of card skins.</returns>
        public static string[] AvailableSkins()
        {
            return Directory.EnumerateDirectories(CARD_SKINS_DIR)
                            .Select(dir => new DirectoryInfo(dir).Name.Replace('_', ' '))
                            .ToArray();
        }

        /// <summary>
        /// Initialize the cards dictionary by clearing it and
        /// ensuring that all the necessar keys are present.
        /// </summary>
        private static void InitializeCardsDictionary()
        {
            Suit[] suits = (Suit[])Enum.GetValues(typeof(Suit));
            Rank[] ranks = (Rank[])Enum.GetValues(typeof(Rank));

            Cards.Clear();

            foreach (Suit suit in suits)
            {
                // First we add the suit.
                Cards.Add(suit, null);

                foreach (Rank rank in ranks)
                {
                    // Then we add the rank. The Image will be initialized later.
                    Cards[suit] = new Dictionary<Rank, Image> { { rank, null } };
                }
            }
        }

        /// <summary>
        /// Load all assets. Loads the default cardskin.
        /// </summary>
        public static void Load()
        {
            LoadCardSkin("Big Numbers");
        }

        /// <summary>
        /// Load a specific card skin. Replaces the currently loaded images for cards.
        /// </summary>
        /// <param name="name">The name of card skin pack.</param>
        public static void LoadCardSkin(string name)
        {
            List<string> files = new List<string>();
            string cardSkinFolder = Path.Combine(CARD_SKINS_DIR, name.Replace(' ', '_'));

            InitializeCardsDictionary();

            // Accept both jpegs(only the .jpg extension not .jpeg?) and pngs.
            files.AddRange(Directory.EnumerateFiles(cardSkinFolder, "*.jpg"));
            files.AddRange(Directory.EnumerateFiles(cardSkinFolder, "*.png"));

            foreach (string file in files)
            {
                string[] suitAndRank = Path.GetFileNameWithoutExtension(file).Split('_');

                // The texture name should split on _ into two strings.
                // If it doesn't then it is likely in an incorrect format.
                if (suitAndRank.Length == 2 &&
                    validSuits.ContainsKey(suitAndRank[0]) &&
                    validRanks.ContainsKey(suitAndRank[1]))
                {
                    Suit suit = validSuits[suitAndRank[0]];
                    Rank rank = validRanks[suitAndRank[1]];

                    Cards[suit][rank] = Image.FromFile(file);
                }
            }

            // And now load the backside image.
            if (File.Exists(cardSkinFolder + "/backside.png"))
            {
                CardBackside = Image.FromFile(cardSkinFolder + "/backside.png");
            }
            else if (File.Exists(cardSkinFolder + "/backside.jpg"))
            {
                CardBackside = Image.FromFile(cardSkinFolder + "/backside.jpg");
            }
        }

        #endregion
    }
}
