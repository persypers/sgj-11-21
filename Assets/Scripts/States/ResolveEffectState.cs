using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class ResolveEffectState : GameState
{
	public float drawPause = 0.1f;
	Encounter.Resolve resolve;
	protected override void OnEnable()
	{
		base.OnEnable();
		resolve = Global.Instance.resolveCache;
		StartCoroutine(StateRoutine());
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	bool counterReady;
	public IEnumerator StateRoutine()
	{
		counterReady = resolve.sanityEffect == 0 && resolve.innocenceEffect == 0;
		if(!counterReady)
		{
			Global.Instance.statusBar.AnimateFloater(resolve.sanityEffect, resolve.innocenceEffect);
		}

		if(resolve.draw > 0)
		{
			for(int i = 0; i < resolve.draw; i++)
			{
				yield return new WaitForSeconds(drawPause);
				Global.Instance.Draw();
			}
		}

		while(!counterReady)
		{
			yield return null;
		}
		Global.Instance.Innocence += resolve.innocenceEffect;
		Global.Instance.Sanity += resolve.sanityEffect;
		Global.Instance.statusBar.AnimateIcons(resolve.sanityEffect, resolve.innocenceEffect);
		
		// check win/lose condition
		if(resolve.discard > 0)
		{
			GameState.SwitchState<DiscardState>();
		}
		else {
			GameState.SwitchState<WalkState>();
		}

	}

	public void CounterReady(GameObject go)
	{
		counterReady = true;
	}

	protected override void Update()
	{
		base.Update();
	}
}
