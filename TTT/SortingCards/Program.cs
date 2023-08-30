using System;
using System.Collections.Generic;

namespace SortingCards;

public class Program {
    static void Main()
    {
        /*var card = new Card(2, 3);
        var cardTwo = new Card(13, 3);
        if (card != null) Console.WriteLine(card.CompareTo(cardTwo));*/


        var deck = new List<Card> {
            new Card (2,3),
            new Card (5,1),
            new Card (1,1),
            new Card (10,2),
            new Card (4,2),
            new Card (11,4),
            new Card (7,3),
            new Card (13,1),
            new Card (6,4),
            new Card (9,3)
        };

        deck.Sort();

        var sortedDeck = new List<Card> {
            new Card (1,1),
            new Card (5,1),
            new Card (13,1),
            new Card (4,2),
            new Card (10,2),
            new Card (2,3),
            new Card (7,3),
            new Card (9,3),
            new Card (6,4),
            new Card (11,4)
        };

        for (int i = 0; i < deck.Count; i++) {
            Card card1 = deck[i];
            Card card2 = sortedDeck[i];
            if (card1.rank != card2.rank || card1.suit != card2.suit) {
                Console.WriteLine("Index {0}: Expected ({1}) but got ({2}).", i, card2, card1);
                return;
            }
        }
        Console.WriteLine("Deck sorted successfully!");
    }
}