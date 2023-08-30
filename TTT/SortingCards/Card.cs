using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace SortingCards;

public class Card : IComparable<Card> {
    public int rank;
    public int suit;

    public Card(int rank, int suit) {
        this.rank = rank;
        this.suit = suit;
    }
    /// <summary>
    /// Given an object Card, CompareTo sorts cards
    /// first by their suit property and then by rank property.
    /// </summary>
    /// <param name="cardToCompare"> The object Card with which we
    /// compare our Card </param>
    /// <returns> A positive int if this.suit/this.rank index is "greater than" c.
    /// A negative int if this.suit/rank index is "less than" c.
    /// Zero if they are equal.</returns>
    public int CompareTo(Card? cardToCompare)
    {
        if (suit > cardToCompare!.suit) return 1;
        if (suit < cardToCompare!.suit) return -1;
        if (rank > cardToCompare!.rank) return 1;
        if (rank < cardToCompare!.rank) return -1;
        return 0;
    }

    public override string ToString() {
        return $"{rank}, {suit}";
    }

}