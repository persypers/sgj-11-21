using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class JudgementState : GameState
{
	public float checkEndPause = 0.5f;
	public float pauseBetweenBlames = 0.5f;
	EncounterView view;
	Encounter enc;
	Encounter.Resolve resolve;

	bool predicateHighlighted;
	protected override void OnEnable()
	{
		base.OnEnable();
		enc = Global.Instance.nextEncounter;
		view = Global.Instance.encounterView;

		if(Global.Instance.config.foolModeEnabled)
		{
			resolve = Global.Instance.resolveCache;
		}
		else
		{
			var activeHand = Global.Instance.hand.GetActiveCards();
			resolve = enc.GetResolve(activeHand);
		}

		StartCoroutine(StateRoutine());
	}

	public IEnumerator StateRoutine()
	{
		for(int i = 0; i < enc.blames.Length; i++)
		{
			Global.Instance.handView.SetHighlight(null);
			var br = resolve.blameResolves[i];
			predicateHighlighted = false;
			Animator anim = view.blameViews[i].GetComponent<Animator>();
			anim.SetTrigger("Check");
			if(br.predicateSucceed && br.hits != null)
			{
				Global.Instance.handView.SetHighlight(br.hits);
			}
			while(!predicateHighlighted)
			{
				yield return null;
			}
			yield return new WaitForSeconds(checkEndPause);
			anim.SetBool("Success", br.predicateSucceed);
			anim.SetTrigger("CheckEnd");
			yield return new WaitForSeconds(pauseBetweenBlames);
		}
		GameState.SwitchState<EncounterState>();
	}

	protected override void OnDisable()
	{
		Global.Instance.handView.SetHighlight(null);
		for(int i = 0; i < view.blameViews.Length; i++)
		{
			view.blameViews[i].GetComponent<Animator>().SetTrigger("Reset");
		}
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
	}

	public void PredicateHighlightReady(GameObject go)
	{
		predicateHighlighted = true;
	}
}
