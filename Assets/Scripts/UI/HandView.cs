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
	public CardPlace[] cardPlaces;
	public UnityEngine.UI.HorizontalLayoutGroup placesContainer;
	public int placesContainerWidth;
	public int placesContainerMaxSpacing = 12;
	int shrinkCount = 9;
	
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
		cardPlaces[i].GetComponent<Animator>().SetBool("Low", hand.IsHeld(i));
		//c.Snap(deck);
		c.Apply(hand[i]);
		c.anchor = cardPlaces[i].anchor;
		c.transform.localScale = Vector3.one;
		c.gameObject.SetActive(true);
		c.cardIndex = i;
		return c;
	}

	public void Reset()
	{
		Clear();
		for(int i = 0; i < hand.Count; i++)
		{
			CardView c = CreateCardView(i);
			c.Snap();
			cardViews.Add(c);
		}
		UpdateCardPlaces();
	}

	public void UpdateViewPositions()
	{
		UpdateCardPlaces();
		for(int j = 0; j < cardViews.Count; j++)
		{
			
			cardViews[j].transform.SetSiblingIndex(j);
			cardViews[j].anchor = cardPlaces[j].anchor;
			cardViews[j].cardIndex = j;
			if(hand.IsHeld(j)) cardViews[j].shirt.Show(); else cardViews[j].shirt.Hide();
			cardPlaces[j].GetComponent<Animator>().SetBool("Low", hand.IsHeld(j));
		}
	}

	public void UpdateCardPlaces()
	{
		int w = 158;
		int c = hand.Count;

		int spacing = c < 9 ? 12 : ((placesContainerWidth - w) / (c - 1) - w);
		spacing = Mathf.Min(spacing, placesContainerMaxSpacing);

		placesContainer.spacing = spacing;

		for(int i = 0; i < cardPlaces.Length; i++)
		{
			//cardPlaces[i].gameObject.SetActive(i < hand.Count || i < shrinkCount);
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
		cardViews[i].cardIndex = -1;
		cardViews[i].shirt.Hide();
		cardViews.RemoveAt(i);
		UpdateViewPositions();
	}

	public void OnCardHeld(int i)
	{
		cardPlaces[i].GetComponent<Animator>().SetBool("Low", hand.IsHeld(i));
		if(hand.IsHeld(i)) cardViews[i].shirt.Show(); else cardViews[i].shirt.Hide();
	}
	public void OnEnable()
	{
		hand = Global.Instance.hand;
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

	public void SetHighlight(List<Card> cards)
	{
		for(int i = 0; i < cardPlaces.Length; i++)
		{
			cardPlaces[i].GetComponent<Animator>().SetBool("High", false);
		}
		if(cards == null) return;
		var indices = hand.GetIndexArray(cards);
		for(int i = 0; i < indices.Count; i++)
		{
			cardPlaces[indices[i]].GetComponent<Animator>().SetBool("High", true);
		}
		
	}
}
