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
	}

	protected override void OnDisable()
	{
		view.theButton.Hide();
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
			Global.Instance.hand.Hold(index);
		}
	}

	public void THEBUTTON(GameObject go)
	{
		Debug.Log("THE BUTTON: " + go.name);
		GameState.SwitchState<HandOfFateState>();
	}
}
