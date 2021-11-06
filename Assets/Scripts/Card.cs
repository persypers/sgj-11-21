using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Card : System.IEquatable<Card>
{
	public enum Value
	{
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Jack,
		Queen,
		King,
		Ace
	}

	public enum Mast
	{
		Clubs,
		Spades,
		Diamonds,
		Hearts
	}

	public enum Color
	{
		Default,
		Black,
		Red
	}

	public static int TOTAL = 36;
	public Mast mast;
	public Value value;

	bool System.IEquatable<Card>.Equals(Card other)
	{
		return value == other.value && mast == other.mast;
	}

	public static bool operator ==(Card c1, Card c2)
	{
		return c1.Equals(c2);
	}

	public static bool operator !=(Card c1, Card c2)
	{
	return !c1.Equals(c2);
	}

	public Card(Mast mast, Value value)
	{
		this.mast = mast;
		this.value = value;
	}

	public override string ToString()
	{
		return System.Enum.GetName(typeof(Value), value) + " of " + System.Enum.GetName(typeof(Mast), mast);
	}

	public override int GetHashCode()
	{
		return mast.GetHashCode() ^ value.GetHashCode();
	}

	[System.Serializable]
	public class Deck : List<Card> {};

	public static List<Card> GenerateDeck()
	{
		List<Card> deck = new List<Card>();

		string[] masts = System.Enum.GetNames(typeof(Mast));
		string[] values = System.Enum.GetNames(typeof(Value));

		for(int i = 0; i < masts.Length; i++)
		{
			for(int j = 0; j < values.Length; j++)
			{
				Mast m;
				System.Enum.TryParse<Mast>(masts[i], out m);
				Value v;
				System.Enum.TryParse<Value>(values[j], out v);
				deck.Add(new Card(m, v));
			}
		}
		return deck;
	}

	public static void Shuffle(List<Card> deck)
	{
		for(int i = 0; i < deck.Count; i++)
		{
			int j = Random.Range(i, deck.Count);
			Card swap = deck[j];
			deck[j] = deck[i];
			deck[i] = swap;
		}
	}
}
