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

	public void Reset()
	{
		cards.Clear();
		UnholdAll();
	}
	public void AddCard(Card c)
	{
		cards.Add(c);
		cardInserted.Invoke(cards.Count - 1);
	}

	public void RemoveCard(int i)
	{
		Debug.Assert(i >= 0 && i < cards.Count);
		cards.RemoveAt(i);
		for(int j = i; j < held.Length - 1; j++)
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
		if(!held[i] && GetHeldCount() >= Global.Instance.MaxHeld)
		{
			UnholdRandom();
		}
		held[i] = !held[i];
		cardHeld.Invoke(i);
		return held[i];
	}

	public int GetHeldCount()
	{
		int c = 0;
		for(int i = 0; i < cards.Count; i++)
		{
			if(held[i]) c++;
		}
		return c;
	}

	public void UnholdAll()
	{
		for(int i = 0; i < held.Length; i++)
		{
			if(held[i]) Hold(i);
		}
	}

	public void UnholdRandom()
	{
		int hc = GetHeldCount();
		if(hc < 1) return;
		int c = Random.Range(0, hc);
		for(int i = 0; i < cards.Count; i++)
		{
			if(held[i])
			{
				if(c == 0) 
				{
					Hold(i);
					return;
				}
				c--;
			}
		}
	}

	public List<Card> GetActiveCards()
	{
		List<Card> result = new List<Card>();
		for(int i = 0; i < cards.Count; i++)
		{
			if(held[i]) continue;
			result.Add(cards[i]);
		}
		return result;
	}

	public List<int> GetIndexArray(List<Card> cards)
	{
		List<int> result = new List<int>();
		for(int i = 0; i < cards.Count; i++)
		{
			int id = -1;
			for(int j = 0; j < this.cards.Count; j++)
			{
				if(this.cards[j] == cards[i] && !result.Contains(j)) id = j;
			}
			Debug.Assert(id >= 0);
			result.Add(id);
		}
		return result;
	}

	public List<int> ActiveToHandIndices(List<int> active)
	{
		List<int> result = new List<int>();
		for(int i = 0; i < active.Count; i++)
		{
			int id = active[i];
			for(int j = 0; j < held.Length; j++)
			{
				if(held[j] && j < id) id ++;
			}
			result.Add(id);
		}
		return result;
	}
}
