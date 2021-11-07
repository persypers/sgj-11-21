using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class Global : MonoSingleton<Global>
{
	public ResourceConfig config;
	public GameObject gameUi;
	public HandView handView;
	public EncounterView encounterView;
	public StatusBar statusBar;
	public GameObject barkLabel;
	
	public Hand hand;
	public List<Card> deck;

	public Para para;
	public GameObject character;
	public Transform groundLayer;
	public Transform characterAppearRoot;
	public Transform characterStopRoot;

	int sanity;
	public int Sanity
	{
		get => sanity;
		set {
			sanity = value;
			statusBar.sanityLabel.text = sanity.ToString();
		}
	}
	int innocence;
	public int Innocence
	{
		get => innocence;
		set {
			innocence = value;
			statusBar.innocenceLabel.text = innocence.ToString() + "/" + config.maxInnocence.ToString();
		}
	}

	public EncounterSource game;
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
		Innocence = 0;
		Sanity = config.baseSanity;
		game.Reset();
		//hand.cards = deck.GetRange(0, config.firstDeal);
		//deck.RemoveRange(0, config.firstDeal);
		//handView.Reset();
	}

	public Encounter PopNextEncounter()
	{
		return nextEncounter = game.Get();
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

	public Encounter.Resolve resolveCache;
	public Encounter.Resolve UpdateResolveCache()
	{
		List<Card> activeHand = hand.GetActiveCards();
		resolveCache = nextEncounter.GetResolve(activeHand);
		return resolveCache;
	}

	protected override void Awake()
	{
		base.Awake();
		GameState.SwitchState<IntroState>();
	}
}
