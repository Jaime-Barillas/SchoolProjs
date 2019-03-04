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
                Console.WriteLine("Trump suit is " + talon.TrumpSuit);

                Console.WriteLine("Your hand: ");
                for (int index = 0; index < hand.Count; index++)
                {
                    Console.WriteLine("\t" + index + ": " + hand[index]);
                }

                Console.Write("Select a card to defend with: ");

                // Player selects a card
                defense = hand[int.Parse(Console.ReadLine())];

                if (defense.CanDefendAgainst(attack, talon.TrumpSuit))
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
    }
}
