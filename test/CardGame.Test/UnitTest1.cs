using NUnit.Framework;

namespace CardGame.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NewDeck()
        {
            var deck = new Deck();

            Assert.AreEqual(52, deck.Length);
        }

        [Test]
        public void DealOne()
        {
            var deck = new Deck();

            deck.DealOneCard();

            Assert.AreEqual(51, deck.Length);
        }
    }
}