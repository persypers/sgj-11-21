using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fancy;

public class WalkState : GameState
{
	public const float TIME = 1.0f;
	float t;
	protected override void OnEnable()
	{
		base.OnEnable();
		Ben.Instance.isWalking = true;
		Ben.Instance.SetTarget(Ben.Instance.normalAnchor);
		t = 0.0f;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}


	protected override void Update()
	{
		base.Update();
		t += Time.deltaTime;
		if(t >= TIME && Ben.Instance.IsTargetReached())
		{
			GameState.SwitchState<EncounterState>();
		}
	}
}
