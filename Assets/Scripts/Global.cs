using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class Global : MonoSingleton<Global>
{
	public ResourceConfig config;
	public HandView handView;
	public EncounterView encounterView;
	
	public Hand hand;
	public List<Card> deck;


	public Encounter nextEncounter;
	public int MaxHeld
	{
		get {
			return 2;
		}
	}
	public void ResetGame()
	{
		deck = Card.GenerateDeck();
		Card.Shuffle(deck);
		//hand.cards = deck.GetRange(0, config.firstDeal);
		//deck.RemoveRange(0, config.firstDeal);
		//handView.Reset();
	}

	public void Draw()
	{
		if(config.maxCardsOnHand > hand.Count)
		{
			if(deck.Count == 0)
			{
				Debug.Log("Deck is empty, shuffling a new one...");
				//deck = Card.GenerateDeck();
				//Card.Shuffle(deck);
				return;
			}
			hand.AddCard(deck[0]);
			deck.RemoveAt(0);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		GameState.SwitchState<MenuState>();
	}
}
