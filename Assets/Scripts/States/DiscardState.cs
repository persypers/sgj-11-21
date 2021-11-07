using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class DiscardState : GameState
{
	public string text = "Сбрось карты: ";
	public TMPro.TMP_Text label;
	public int toDiscard;
	protected override void OnEnable()
	{
		label.gameObject.Show();
		toDiscard = Global.Instance.resolveCache.discard;
		UpdateLabel();
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		label.gameObject.Hide();
		base.OnDisable();
	}

	public void UpdateLabel()
	{
		label.text = text + toDiscard.ToString();
	}

	public void CardTap(GameObject go)
	{
		int index = go.GetComponent<CardView>().cardIndex;
		Debug.Log("Card tap: " + index + " : " + go.name );
		if(index >= 0 && index < Global.Instance.hand.Count)
		{
			Global.Instance.hand.RemoveCard(index);
		}
		toDiscard --;
		UpdateLabel();

		if(toDiscard <= 0)
		{
			GameState.SwitchState<PostBark>();
		}
	}

	protected override void Update()
	{
		base.Update();
		//GameState.SwitchState<MenuState>();
	}
}
