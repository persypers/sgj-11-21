using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fancy;

public class Hand : MonoSingleton<Hand>
{
	public List<Card> cards;

	public Card this[int key]
	{
		get => cards[key];
	}

	public int Count => cards.Count;

	public int GetCardIndex(Card card)
	{
		for(int i = 0; i < cards.Count; i++)
		{
			if(cards[i] == card) return i;
		}
		return -1;
	}

	[System.Serializable]
	public class CardEvent : UnityEvent<int> {};

	public CardEvent cardRemoved;
	public CardEvent cardInserted;
	public CardEvent cardHeld;

	public void AddCard(Card c)
	{
		cards.Add(c);
		cardInserted.Invoke(cards.Count - 1);
	}

	public void RemoveCard(int i)
	{
		Debug.Assert(i >= 0 && i < cards.Count);
		cards.RemoveAt(i);
		for(int j = i; j < cards.Count; j++)
		{
			held[j] = held[j + 1];
		}
		cardRemoved.Invoke(i);
	}

	bool[] held = new bool[Card.TOTAL];
	public bool IsHeld(int i)
	{
		return held[i];
	}
	public bool Hold(int i)
	{
		Debug.Assert(i >= 0 && i < cards.Count);
		cardHeld.Invoke(i);
		held[i] = !held[i];
		return held[i];
	}
}