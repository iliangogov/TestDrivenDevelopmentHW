using System;
using NUnit.Framework;
using System.Collections.Generic;
using Mono.Collections.Generic;

namespace Poker.Tests
{
    [TestFixture]
    public class CardClassTests
    {
        [Test]
        public void ToString_MustReturnExpectedStringContainingCardFaceAndCardSuit()
        {
            string expected = "Five of Spades";
            var card = new Card(CardFace.Five, CardSuit.Spades);
            string actualToString = card.ToString();

            Assert.AreEqual(expected, actualToString);
        }
    }

    [TestFixture]
    public class HandClassTests
    {
        [Test]
        public void ToString_MustReturnExpectedStringContainingListedCardFaceAndCardSuitSeparatedByComma()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.King, CardSuit.Diamonds);
            var card3 = new Card(CardFace.Queen, CardSuit.Hearts);
            var card4 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card5 = new Card(CardFace.Queen, CardSuit.Spades);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            string expected = "Ace of Clubs, King of Diamonds, Queen of Hearts, Queen of Clubs, Queen of Spades";

            var hand = new Hand(listOfCards);
            string actualHandToString = hand.ToString();

            Assert.AreEqual(expected, actualHandToString);
        }
    }

    [TestFixture]
    public class PokerHandsCheckerTests
    {
        [Test]
        public void IsValidHand_MustReturnTrue_IfAllCardsInHandAreDifferent()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Diamonds);
            var card3 = new Card(CardFace.Queen, CardSuit.Hearts);
            var card4 = new Card(CardFace.Queen, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Jack, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsValidHand(hand));
        }

        [Test]
        public void IsValidHand_MustReturnFalse_IfHandContains2CardsWithSameSuitAndSameFace()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card3 = new Card(CardFace.Queen, CardSuit.Hearts);
            var card4 = new Card(CardFace.Queen, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Jack, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsValidHand(hand));
        }
        [Test]
        public void IsValidHand_MustReturnFalse_IfHandContainsMoreOrLessThan5Cards()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Diamonds);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsValidHand(hand));
        }

        [Test]
        public void IsFlush_MustReturnTrue_IfAllCardsInHandAreWithSameCardSuite()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.King, CardSuit.Clubs);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.Jack, CardSuit.Clubs);
            var card5 = new Card(CardFace.Nine, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsFlush(hand));
        }

        [Test]
        public void IsFlush_MustReturnFalse_IfAllCardsInHandAreNotWithSameCardSuite()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Diamonds);
            var card2 = new Card(CardFace.King, CardSuit.Clubs);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.Jack, CardSuit.Clubs);
            var card5 = new Card(CardFace.Nine, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsFlush(hand));
        }

        [Test]
        public void IsFlush_MustReturnFalse_IfHandIsStraightFlush()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.King, CardSuit.Clubs);
            var card2 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card3 = new Card(CardFace.Jack, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ten, CardSuit.Clubs);
            var card5 = new Card(CardFace.Nine, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsFlush(hand));
        }

        [Test]
        public void IsFourOfAKind_ShouldReturnTrue_IfThereAre4cardsWithSameCardFaceInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ace, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsFourOfAKind(hand));
        }

        [Test]
        public void IsFourOfAKind_ShouldReturnFalse_IfThereAreNot4cardsWithSameCardFaceInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ten, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsFourOfAKind(hand));
        }

        [Test]
        public void IsFullHouse_ShouldReturnTrue_IfThereAre3cardsWithSameCardFaceAndAnother2CardsWithSameCardFaceInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.Queen, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsFullHouse(hand));
        }

        [Test]
        public void IsFullHouse_ShouldReturnFalse_IfThereAre3cardsWithSameCardFaceAndAnother2CardsWithDifferentCardFaceInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsFullHouse(hand));
        }

        [Test]
        public void IsThreeOfAKind_ShouldReturnTrue_IfThereAre3cardsWithSameCardFaceAndAnother2CardsWithDifferentCardFaceInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsThreeOfAKind(hand));
        }

        [Test]
        public void IsThreeOfAKind_ShouldReturnFalse_IfThereAreNot3cardsWithSameCardFaceInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ten, CardSuit.Spades);
            var card3 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsThreeOfAKind(hand));
        }

        [Test]
        public void IsThreeOfAKind_ShouldReturnFalse_IfFullHouseHandIsPassedEvenThereAreThreeOfAKindInsideThisHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Ace, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsThreeOfAKind(hand));
        }

        [Test]
        public void IsTwoPair_ShouldReturnTrue_IfThereAreTwoPairsAndDifferrentFifthCardInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Queen, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsTwoPair(hand));
        }

        [Test]
        public void IsTwoPair_ShouldReturnFalse_IfThereAreNotTwoPairsInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.Ten, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Queen, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsTwoPair(hand));
        }

        [Test]
        public void IsOnePair_ShouldReturnTrue_IfThereAreOnePairAndThreeOtherDifferrentCardsInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ten, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Queen, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsOnePair(hand));
        }

        [Test]
        public void IsOnePair_ShouldReturnFalse_IfThereIsNoPairInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Nine, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ten, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Queen, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsOnePair(hand));
        }

        [Test]
        public void IsOnePair_ShouldReturnFalse_IfThereAreTwoPairsInTheHand()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.King, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Queen, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsOnePair(hand));
        }

        [Test]
        public void IsHighCard_ShouldReturnTrue_IfAllCardsInTheHandHaveDifferentFacesAndMixedSuitsAndAreNotSequential()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Nine, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Jack, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Queen, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsHighCard(hand));
        }

        [Test]
        public void IsHighCard_ShouldReturnFalse_IfAllCardsInTheHandHaveDifferentFacesAndEqualSuit()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Nine, CardSuit.Clubs);
            var card2 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Jack, CardSuit.Clubs);
            var card5 = new Card(CardFace.Queen, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsHighCard(hand));
        }

        [Test]
        public void IsHighCard_ShouldReturnFalse_IfAllCardsInTheHandHaveDifferentFacesAndAreSeqential()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ten, CardSuit.Diamonds);
            var card2 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Jack, CardSuit.Clubs);
            var card5 = new Card(CardFace.Queen, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsHighCard(hand));
        }

        [Test]
        public void IsHighCard_ShouldReturnFalse_IfOnePairIsPassed()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Diamonds);
            var card2 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Jack, CardSuit.Clubs);
            var card5 = new Card(CardFace.Queen, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsHighCard(hand));
        }
        [Test]
        public void IsStraight_ShouldReturnTrue_IfCardsInHandAreSequential()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Queen, CardSuit.Spades);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ten, CardSuit.Diamonds);
            var card5 = new Card(CardFace.Jack, CardSuit.Hearts);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsStraight(hand));
        }

        [Test]
        public void IsStraight_ShouldReturnFalse_IfCardsInHandAreSequentialButOneColored()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ten, CardSuit.Clubs);
            var card5 = new Card(CardFace.Jack, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsStraight(hand));
        }

        [Test]
        public void IsStraight_ShouldReturnFalse_IfCardsInHandAreNotSequential()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card2 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Nine, CardSuit.Clubs);
            var card5 = new Card(CardFace.Jack, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsStraight(hand));
        }

        [Test]
        public void IsStraightFlush_ShouldReturnTrue_IfHandWithSequentialCardsWithOneColorIsPassed()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ten, CardSuit.Clubs);
            var card2 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Nine, CardSuit.Clubs);
            var card5 = new Card(CardFace.Jack, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsTrue(cheker.IsStraightFlush(hand));
        }

        [Test]
        public void IsStraightFlush_ShouldReturnFalse_IfRoyalFlushHandIsPassed()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ten, CardSuit.Clubs);
            var card2 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Ace, CardSuit.Clubs);
            var card5 = new Card(CardFace.Jack, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsStraightFlush(hand));
        }

        [Test]
        public void IsStraightFlush_ShouldReturnFalse_IfHandWithSequentialCardsWithMultyColorIsPassed()
        {
            var listOfCards = new List<ICard>();
            var card1 = new Card(CardFace.Ten, CardSuit.Spades);
            var card2 = new Card(CardFace.Queen, CardSuit.Clubs);
            var card3 = new Card(CardFace.King, CardSuit.Clubs);
            var card4 = new Card(CardFace.Nine, CardSuit.Clubs);
            var card5 = new Card(CardFace.Jack, CardSuit.Clubs);
            listOfCards.Add(card1);
            listOfCards.Add(card2);
            listOfCards.Add(card3);
            listOfCards.Add(card4);
            listOfCards.Add(card5);
            var hand = new Hand(listOfCards);
            var cheker = new PokerHandsChecker();

            Assert.IsFalse(cheker.IsStraightFlush(hand));
        }


    }

}