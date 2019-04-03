using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;

            do
            {
                Console.Clear();
                Console.WriteLine("Select an option for testing:");
                Console.WriteLine("\t1) Test deck creation/shuffling");
                Console.WriteLine("\t2) Defense test");
                Console.WriteLine("\t3) Player subclass test");
                Console.WriteLine("\t4) Full game test - AI only");

                Console.Write("Selection: ");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        DisplayDeck();
                        break;
                    case "2":
                        DefenseTest();
                        break;
                    case "3":
                        PlayerTest();
                        break;
                    case "4":
                        FullGameAITest();
                        break;
                    default:
                        userInput = "retry";
                        break;
                }
            } while (userInput == "retry");

            Console.Read();
        }

        /// <summary>
        /// Create a new (automatically shuffled) deck, draw each card, and display those cards to the console.
        /// </summary>
        static void DisplayDeck()
        {
            Deck testDeck = new Deck();
            int deckSize = testDeck.Size;
            Console.WriteLine("testDeck created with " + testDeck.Size + " cards.");
            
            for (int cardsToDraw = deckSize; cardsToDraw > 0; cardsToDraw--)
            {
                Console.Write("[{0}/{1}] ", cardsToDraw, deckSize);
                Console.WriteLine(testDeck.Draw());
            }
        }

        /// <summary>
        /// Test defense logic by having the player defend against an entire talon.
        /// </summary>
        static void DefenseTest()
        {
            List<Card> hand = new List<Card>(); // Player's hand
            Talon talon = new Talon();  // Talon to play against

            Console.WriteLine("----------");
            Console.WriteLine("Populating player's hand...");
            for (int i = 1; i <= 6; i++)
            {
                hand.Add(talon.Draw());
            }

            // Main game loop
            do
            {
                Card attack = talon.Draw();
                Card defense;

                Console.WriteLine("\n===\nDefending against: " + attack);
                Console.WriteLine("Trump suit is " + talon.Trump.Suit);

                Console.WriteLine("Your hand: ");
                for (int index = 0; index < hand.Count; index++)
                {
                    Console.WriteLine("\t" + index + ": " + hand[index]);
                }

                Console.Write("Select a card to defend with: ");

                // Player selects a card
                defense = hand[int.Parse(Console.ReadLine())];

                if (defense.CanDefendAgainst(attack, talon.Trump.Suit))
                {
                    Console.WriteLine("+ Defended successfully. Defense card has been shed from your hand.");
                    hand.Remove(defense);
                }
                else
                {
                    Console.WriteLine("- Defense failed. You must take the attack card.");
                    hand.Add(attack);
                }

                Console.WriteLine("Cards remaining in talon: " + talon.Size);
            } while (talon.Size > 0 && hand.Count > 0);

            Console.WriteLine("-----------");
            if (hand.Count == 0)
            {
                Console.WriteLine("\n...Oh, you won! Good job!");
            }
            else
            {
                Console.WriteLine("\nTalon empty; test concluded.");
            }
        }

        static void PlayerTest()
        {
            Console.WriteLine("Creating players...");
            List<Player> players = new List<Player>
            {
                new HumanPlayer("John"),
                new SimpleAIPlayer("Computron 386")
            };

            Console.WriteLine("Creating talon...");
            Talon talon = new Talon();

            const int CARDS_TO_DRAW = 6;

            Console.WriteLine("Populating hands...");
            for (int i = 0; i < CARDS_TO_DRAW; i++)
            {
                foreach (Player player in players)
                {
                    player.TakeCard(talon.Draw());
                }
            }

            // Display contents of hands
            foreach (Player player in players)
            {
                Console.WriteLine("\n------\n{0} is holding:", player.Name);
                foreach (Card card in player.Hand)
                {
                    Console.WriteLine("\t{0}", card.ToString());
                }
            }
        }

        /// <summary>
        /// Simple output method to use in place of a game log for the next two tests
        /// </summary>
        static void _SpitToOutput(object sender, GameLogEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        static void FullGameAITest()
        {
            // Create new game with two simple AI players
            Game theGame = new Game(new Deck(), new List<Player> { new SimpleAIPlayer("AI Player 1"), new SimpleAIPlayer("AI Player 2") });

            //bool gameOver = false;  // Flag for determining when the game has ended.
            /* 
             * Subscribe event handlers to all relevant events
             */
            // NOTE: these delegates are very ugly, and in actual code we should always be using actual declared methods.
            //theGame.End += delegate (object sender, GameLogEventArgs e) { gameOver = true; };
            theGame.End += _SpitToOutput;
            theGame.NewBout += _SpitToOutput;

            theGame.Talon.Empty += _SpitToOutput;

            theGame.CurrentBout.Report += _SpitToOutput;
            theGame.CurrentBout.End += _SpitToOutput;
            theGame.CurrentBout.End += delegate (object sender, GameLogEventArgs e)
            {
                // Unsubscribe event handlers when a bout ends.
                theGame.CurrentBout.Report -= _SpitToOutput;
                theGame.CurrentBout.End -= _SpitToOutput;
            };
            theGame.NewBout += delegate (object sender, GameLogEventArgs e)
            {
                // Subscribe event handlers to new bout when it starts.
                theGame.CurrentBout.Report += _SpitToOutput;
                theGame.CurrentBout.End += _SpitToOutput;
            };

            foreach (Player player in theGame.Players)
            {
                player.AttackLog += _SpitToOutput;
                player.DefendLog += _SpitToOutput;
                player.Concede += _SpitToOutput;
                player.HandEmpty += _SpitToOutput;
                player.PickUp += _SpitToOutput;
                // Note: Attack, Defend and Prompt events use GameActionEventArgs, and so aren't meant for the game log.
                // They're mostly handled internally, but can be subscribed to if we wanted to, say, make a card spin around when you attack with it or something.
                // The one exception is that HumanPlayers must have their functionality defined in a method that subscribes to the Prompt event.
            }

            /*
             * Begin the core game loop.
             */


            do
            {
                theGame.Continue();

                // Display current game state
                foreach (Player player in theGame.Players)
                {
                    Console.WriteLine("\n-----\n{0}'s hand:", player);
                    foreach (Card card in player.Hand)
                    {
                        Console.WriteLine("\t{0}", card);
                    }
                }
                Console.WriteLine("\n============\nCards on the table:");
                foreach (Card attackCard in theGame.CurrentBout.AttackCardsPlayed)
                {
                    Console.Write(attackCard);
                    // If the card has been defended against...
                    if (theGame.CurrentBout.DefenseCardsPlayed.Count > 0 && theGame.CurrentBout.AttackCardsPlayed.IndexOf(attackCard) <= theGame.CurrentBout.DefenseCardsPlayed.Count - 1)
                    {
                        // (in retrospect it might have been much cleaner to keep a reference variable pointing to these two lists, haha)
                        Console.WriteLine(" (blocked with {0})", theGame.CurrentBout.DefenseCardsPlayed[theGame.CurrentBout.AttackCardsPlayed.IndexOf(attackCard)]);
                    }
                    else
                    {
                        Console.Write(" (unblocked)");
                    }
                }
                Console.WriteLine("\n\nCards remaining in the talon: {0}", theGame.Talon.Size);
                Console.WriteLine("Trump suit: {0}", theGame.TrumpSuit);

                Console.WriteLine("\n\n*********************\n");
                Console.Read();
            } while (!theGame.IsOver);
        }
    }
}
