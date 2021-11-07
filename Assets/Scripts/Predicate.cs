using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Predicates
{
	public delegate List<Card> Predicate(List<Card> hand);
	public enum Enum
	{
		None,
		_2S,
		_2S_or_2S,
		_4Red,
		KQRed,
		_Qh,
		_Ks,
	}

	public static Dictionary<Enum, Predicate> Table = new Dictionary<Enum, Predicate>()
	{
		{Enum._Qh, Both(Value(Card.Value.Queen), Mast(Card.Mast.Hearts))},	/// Дама червей
		{Enum._Ks, Both(Value(Card.Value.King), Mast(Card.Mast.Spades))},	/// Король пик
		{Enum._2S, p_2S},
		{Enum._2S_or_2S, Or(p_2S, p_2S)},
		{Enum._4Red, X(Color(Card.Color.Red), 4)},
		{Enum.KQRed, And(Both(Color(Card.Color.Red), Value(Card.Value.King)), Both(Color(Card.Color.Red), Value(Card.Value.Queen)))},
	};
	
	public static List<Card> p_2S(List<Card> hand)
	{
		List<Card> hits = new List<Card>();
		for(int i = 0; i < hand.Count; i++)
		{
			if(hand[i].mast == Card.Mast.Spades)
			{
				hits.Add(hand[i]);
			}
		}
		return hits.Count >= 2 ? hits : null;
	}

	public static Predicate Value(Card.Value value)
	{
		return (hand) => {
			List<Card> hits = new List<Card>();
			for(int i = 0; i < hand.Count; i++)
			{
				if(hand[i].value == value)
				{
					hits.Add(hand[i]);
					return hits;
				}
			}
			return null;
		};
	}
	public static Predicate ValueRange(Card.Value minValue, Card.Value maxValue)
	{
		return (hand) => {
			List<Card> hits = new List<Card>();
			int min = (int)minValue;
			int max = (int)maxValue;
			for(int i = 0; i < hand.Count; i++)
			{
				int value = (int) hand[i].value;
				if(value >= min && value <= max)
				{
					hits.Add(hand[i]);
					return hits;
				}
			}
			return null;
		};
	}
	public static Predicate Color(Card.Color color)
	{
		return (hand) => {
			List<Card> hits = new List<Card>();
			for(int i = 0; i < hand.Count; i++)
			{
				if(
					(color == Card.Color.Red && (hand[i].mast == Card.Mast.Diamonds || hand[i].mast == Card.Mast.Hearts))
					|| (color == Card.Color.Black && (hand[i].mast == Card.Mast.Spades || hand[i].mast == Card.Mast.Clubs))
				) {
					hits.Add(hand[i]);
					return hits;
				}
			}
			return null;
		};
	}
	public static Predicate Mast(Card.Mast mast)
	{
		return (hand) => {
			List<Card> hits = new List<Card>();
			for(int i = 0; i < hand.Count; i++)
			{
				if(hand[i].mast == mast)
				{
					hits.Add(hand[i]);
					return hits;
				}
			}
			return null;
		};
	}
	public static Predicate Both(Predicate p1, Predicate p2)
	{
		return (hand) => {
			List<Card> copy = new List<Card>(hand);
			List<Card> hits = new List<Card>();
			List<Card> hits1 = p1(copy);
			while(hits1 != null)
			{
				hits.AddRange(hits1);
				for(int i = 0; i < hits1.Count; i++)
				{
					copy.Remove(hits1[i]);
				}
				hits1 = p1(copy);
			}
			hits = p2(hits);
			return hits;
		};
	}
	public static Predicate MastValue(Card.Mast mast, Card.Value value)
	{
		return (hand) => {
			List<Card> c = new List<Card>();
			for(int i = 0; i < hand.Count; i++)
			{
				if(hand[i].value == value && hand[i].mast == mast)
				{
					c.Add(hand[i]);
					return c;
				}
			}
			return null;
		};
	}
	public static Predicate Or(Predicate p1, Predicate p2)
	{
		return (hand) => {
			List<Card> hits = p1(hand);
			return hits == null ? p2(hand) : hits;
		};
	}

	public static Predicate And(Predicate p1, Predicate p2)
	{
		return (hand) => {
			List<Card> hits1 = p1(hand);
			if(hits1 == null) return null;
			List<Card> hits2 = p2(hand);
			if(hits2 == null) return null;
			for(int i = 0; i < hits2.Count; i++)
			{
				if(!hits1.Contains(hits2[i])) hits1.Add(hits2[i]);
			}
			return hits1;
		};
	}

	public static Predicate X(Predicate p, int times)
	{
		return (hand) => {
			List<Card> copy = new List<Card>(hand);
			List<Card> hits = new List<Card>();
			for(int i = 0; i < times; i++)
			{
				var newHits = p(copy);
				if(newHits == null) return null;
				for(int j = 0; j < newHits.Count; j++)
				{
					hits.Add(newHits[j]);
					copy.Remove(newHits[j]);
				}
			}
			return hits;
		};
	}
}
