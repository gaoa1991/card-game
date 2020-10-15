using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();

            string option;
            do
            {
                Console.WriteLine("Options: option [1] Reset deck");
                Console.WriteLine("         option [2] Draw 1 Card");
                Console.WriteLine("         option [3] Draw multiple cards");
                Console.WriteLine("         option [4] Sort deck");
                Console.WriteLine("         option [5] Create new deck");
                Console.WriteLine("         option [6] Print deck");
                Console.WriteLine("         option [E] Exit");

                option = Console.ReadLine();

                deck = ProcessOption(option, deck);

                Console.WriteLine("");

            } while (option != "E");
            Console.WriteLine("Section option [1] Create Deck");
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// Process option on the deck based on option selected
        /// </summary>
        /// <param name="option">option selected</param>
        /// <param name="deck">deck to be processed</param>
        /// <returns>Updated deck based on processed option</returns>
        private static Deck ProcessOption(string option, Deck deck)
        {
            switch (option)
            {
                case "1":
                    deck.ResetDeck();
                    Console.WriteLine("Deck reset!");
                    break;
                case "2":
                    var card = deck.DealOneCard();
                    Console.WriteLine($"Drew {card.Rank} of {card.Suit}s - Deck has {deck.Length} left");
                    break;
                case "3":
                    int numCards = 0;
                    string input = null;
                    do
                    {
                        Console.WriteLine("Enter number of cards to be dealt");
                        input = Console.ReadLine();

                    } while (!int.TryParse(input, out numCards));

                    var cards = deck.DealCards(numCards);

                    cards.ForEach(card => Console.WriteLine($"Drew {card.Rank} of {card.Suit}s"));
                    Console.WriteLine($"Deck has {deck.Length} left");
                    break;
                case "4":
                    input = null;
                    do
                    {
                        Console.WriteLine("Sort by [R] Rank or [S] Suit]:");
                        input = Console.ReadLine();

                    } while (input != "R" && input != "S");

                    if (input == "R")
                    {
                        deck.SortBy(SortOption.Rank);
                        Console.WriteLine("Sorted by rank");
                    }
                    else
                    {
                        deck.SortBy(SortOption.Suit);
                        Console.WriteLine("Sorted by suit");
                    }
                    break;
                case "5":
                    deck = new Deck();
                    Console.WriteLine("new deck created!");
                    break;
                case "6":
                    deck.Print();
                    break;
                case "E":
                    Console.WriteLine("Good bye!");
                    break;
                default:
                    Console.WriteLine("Invalid Option");
                    break;
            }

            return deck;
        }
    }

    public class Deck
    {
        private List<Card> _cards;

        public int Length => _cards.Count;

        public Deck()
        {
            CreateDeck();
            ShuffleDeck();
        }

        /// <summary>
        /// Creates a deck
        /// </summary>
        private void CreateDeck()
        {
            _cards = new List<Card>();

            //Loops through defined ranks and suits to create a new ordered deck
            foreach (var rank in Enum.GetValues(typeof(Rank)).Cast<Rank>())
            {
                foreach(var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
                {
                    _cards.Add(new Card { Rank = rank, Suit = suit });
                }
            }
        }

        /// <summary>
        /// Deals (pops) one card from current deck
        /// </summary>
        /// <returns>card on top of the deck</returns>
        public Card DealOneCard()
        {
            var currentCard = _cards[0];
            _cards.Remove(currentCard);
            return currentCard;
        }

        /// <summary>
        /// deals (pops) [numOfCards] number of Cards of the deck
        /// </summary>
        /// <param name="numOfCards">number of cards to be dealt</param>
        /// <returns>Top [numOfCards] of the deck</returns>
        public List<Card> DealCards(int numOfCards)
        {
            var currentCards = _cards.GetRange(0, numOfCards);
            _cards.RemoveRange(0, numOfCards);

            return currentCards;
        }

        /// <summary>
        /// Sorts the current deck
        /// </summary>
        /// <param name="option">sorting option</param>
        public void SortBy(SortOption option)
        {
            if(option == SortOption.Rank)
            {
                _cards = _cards.OrderBy(card => card.Rank).ThenBy(card => card.Suit).ToList();
            }
            else
            {
                _cards = _cards.OrderBy(card => card.Suit).ThenBy(card => card.Rank).ToList();
            }
        }

        /// <summary>
        /// Prints all cards in the deck from top to bottom
        /// </summary>
        public void Print()
        {
            int index = 1;

            _cards.ForEach(card => Console.WriteLine($"{index++}. {card.Rank} of {card.Suit}s"));
        }

        /// <summary>
        /// Shuffle deck with current number of cards left
        /// </summary>
        public void ShuffleDeck()
        {
            var rnd = new Random();
            _cards = _cards.OrderBy(x => rnd.Next()).ToList();
        }

        /// <summary>
        /// Reset the deck back to its original size and shuffles it
        /// </summary>
        public void ResetDeck()
        {
            _cards = new List<Card>();
            CreateDeck();
            ShuffleDeck();
        }
    }

    /// <summary>
    /// Card definition
    /// </summary>
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
    }

    /// <summary>
    /// Type of card
    /// </summary>
    public enum Suit
    {
        Spade,
        Diamond,
        Club,
        Heart
    }

    /// <summary>
    /// Card sort options
    /// </summary>
    public enum SortOption
    {
        Suit,
        Rank,
    }

    /// <summary>
    /// Card values
    /// </summary>
    public enum Rank
    {
        Ace, 
        Two, 
        Three, 
        Four, 
        Five, 
        Six, 
        Seven, 
        Eight, 
        Nine, 
        Ten, 
        Jack, 
        Queen, 
        King
    }
}
