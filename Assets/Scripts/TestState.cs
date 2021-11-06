using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class TestState : GameState
{
	public Hand hand;
	public HandView handView;
	public List<Card> deck;

	public EncounterView encounterView;
	public Encounter encounter;
	public void Start()
	{
		deck = Card.GenerateDeck();
		Card.Shuffle(deck);
		hand.cards = deck.GetRange(0, 6);
		deck.RemoveRange(0, 6);
		handView.Reset();

		encounterView.Apply(encounter);
	}

	public void Draw()
	{
		if(Global.Instance.config.maxCardsOnHand > hand.Count)
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
