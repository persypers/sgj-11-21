using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class EncounterState : GameState
{
	Encounter enc;
	EncounterView view;
	protected override void OnEnable()
	{
		base.OnEnable();

		enc = Global.Instance.nextEncounter;
		view = Global.Instance.encounterView;
		view.Apply(enc);
		view.gameObject.Show();
		view.theButton.Show();
		
		var hand = Global.Instance.hand;
		hand.cardHeld.AddListener(OnHandChanged);
		hand.cardInserted.AddListener(OnHandChanged);
		hand.cardRemoved.AddListener(OnHandChanged);

		if(!Global.Instance.handDealt)
		{
			Global.Instance.handDealt = true;
			for(int i = 0; i < Global.Instance.config.firstDeal; i++)
			{
				Global.Instance.Draw();
			}
		}

		OnHandChanged(0);

		Global.Instance.barkLabel.Hide();
		Ben.Instance.isWalking = false;
	}

	protected override void OnDisable()
	{
		view.theButton.Hide();

		var hand = Global.Instance.hand;
		hand.cardHeld.RemoveListener(OnHandChanged);
		hand.cardInserted.RemoveListener(OnHandChanged);
		hand.cardRemoved.RemoveListener(OnHandChanged);

		if(Global.Instance.config.foolModeEnabled)
		{
			for(int i = 0; i < enc.blames.Length; i++)
			{
				view.blameViews[i].GetComponent<Animator>().SetBool("PreviewSuccess", false);
			}
		}

		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
	}

	public void CardTap(GameObject go)
	{
		int index = go.GetComponent<CardView>().cardIndex;
		Debug.Log("Card tap: " + index + " : " + go.name );
		if(index >= 0 && index < Global.Instance.hand.Count)
		{
			Global.Instance.hand.Hold(index);
		}
	}

	public void CardTapRight(GameObject go)
	{
		if(!Global.Instance.config.rightClickDiscards) return;
		int index = go.GetComponent<CardView>().cardIndex;
		Debug.Log("Card tap right: " + index + " : " + go.name );
		if(index >= 0 && index < Global.Instance.hand.Count)
		{
			Global.Instance.hand.RemoveCard(index);
		}
	}

	public void Draw(GameObject go)
	{
		if(Global.Instance.config.allowDraw)
		{
			Global.Instance.Draw();
		}
	}


	public void THEBUTTON(GameObject go)
	{
		Debug.Log("THE BUTTON: " + go.name);
		GameState.SwitchState<HandOfFateState>();
	}

	public void OnHandChanged(int index)
	{
		if(Global.Instance.config.foolModeEnabled)
		{
			Global.Instance.handView.SetHighlight(null);
			var resolve = Global.Instance.UpdateResolveCache();
			for(int i = 0; i < resolve.blameResolves.Length; i++)
			{
				view.blameViews[i].GetComponent<Animator>().SetBool("PreviewSuccess", resolve.blameResolves[i].predicateSucceed);
			}
		}
	}
}
