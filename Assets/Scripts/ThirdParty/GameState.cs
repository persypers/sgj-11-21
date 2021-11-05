using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class GameState : MonoSingleton<GameState>
{
	public Hand hand;
	public List<Card> deck;

	public void Start()
	{
		deck = Card.GenerateDeck();
		Card.Shuffle(deck);
		hand.cards = deck.GetRange(0, 6);
		deck.RemoveRange(0, 6);
		HandView.Instance.Reset();
	}

	public void Draw()
	{
		if(HandView.Instance.cardPlaces.Count > hand.Count)
		{
			if(deck.Count == 0)
			{
				Debug.Log("Deck is empty, shuffling a new one...");
				deck = Card.GenerateDeck();
				Card.Shuffle(deck);
			}
			hand.AddCard(deck[0]);
			deck.RemoveAt(0);
		}
	}
}
