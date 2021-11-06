using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class HandView : MonoSingleton<HandView>
{
	public Hand hand;
	public Transform deck;
	public ObjectPool cardViewPool;
	public List<CardView> cardViews;
	public Transform cardPlaceContainer;
	List<CardPlace> cardPlaces;
	
	public void Clear()
	{
		for(int i = 0; i < cardViews.Count; i++)
		{
			cardViews[i].dieOnEnd = true;
			cardViews[i].Snap();
		}
		cardViews.Clear();
	}

	CardView CreateCardView(int i)
	{
		CardView c = cardViewPool.Get().GetComponent<CardView>();
		c.transform.position = deck.position;
		//c.Snap(deck);
		c.Apply(hand[i]);
		c.anchor = cardPlaces[i].anchor;
		c.transform.localScale = Vector3.one;
		c.gameObject.SetActive(true);
		c.OnRightClick = (cardView, index) => {
			hand.RemoveCard(index);
			cardView.OnRightClick = null;
		};
		c.cardIndex = i;
		return c;
	}

	public void Reset()
	{
		Clear();
		cardPlaces = new List<CardPlace>(cardPlaceContainer.GetComponentsInChildren<CardPlace>(true));
		for(int i = 0; i < hand.Count; i++)
		{
			CardView c = CreateCardView(i);
			c.Snap();
			cardViews.Add(c);
		}
	}

	public void UpdateViewPositions()
	{
		for(int j = 0; j < cardViews.Count; j++)
		{
			cardViews[j].transform.SetSiblingIndex(j);
			cardViews[j].anchor = cardPlaces[j].anchor;
			cardViews[j].cardIndex = j;
		}
	}

	public void OnCardInserted(int i)
	{
		CardView c = CreateCardView(i);
		c.Snap(deck);
		cardViews.Insert(i, c);
		UpdateViewPositions();
	}

	public void OnCardRemoved(int i)
	{
		cardViews[i].dieOnEnd = true;
		cardViews[i].anchor = deck;
		cardViews.RemoveAt(i);
		UpdateViewPositions();
	}

	public void OnCardHeld(int i)
	{

	}
	public void OnEnable()
	{
		hand.cardInserted.AddListener(OnCardInserted);
		hand.cardRemoved.AddListener(OnCardRemoved);
		hand.cardHeld.AddListener(OnCardHeld);
	}

	public void OnDisable()
	{
		hand.cardInserted.RemoveListener(OnCardInserted);
		hand.cardRemoved.RemoveListener(OnCardRemoved);
		hand.cardHeld.RemoveListener(OnCardHeld);
	}

}
